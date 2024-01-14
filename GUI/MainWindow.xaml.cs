using CLI.Controller;
using CLI.Model;
using GUI;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using CLI.Observer;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.ComponentModel;
using GUI.View;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private DispatcherTimer _timer;
        private Controller _controller;
        private int _currentPageNumber = 1;
        private int _maxItemsPerPage = 3;
        private int _totalNumberOfPages = 1;

        private ObservableCollection<ProfessorDTO> _professors = new ObservableCollection<ProfessorDTO>();
        private ObservableCollection<StudentDTO> _students = new ObservableCollection<StudentDTO>();
        private ObservableCollection<SubjectDTO> _subjects = new ObservableCollection<SubjectDTO>();
        private ObservableCollection<DepartmentDTO> _departments = new ObservableCollection<DepartmentDTO>();

        private ObservableCollection<ProfessorDTO> _filteredProfessors;
        private ObservableCollection<StudentDTO> _filteredStudents;
        private ObservableCollection<SubjectDTO> _filteredSubjects;

        public MainWindow()
        {
            InitializeComponent();
            SetWindowLocationAndSize();
            InitTimer();

            _controller = new Controller();
            _controller.publisher.Subscribe(this);

            dataGridProfessor.ItemsSource = _filteredProfessors;
            dataGridStudents.ItemsSource = _filteredStudents;
            dataGridSubjects.ItemsSource = _filteredSubjects;
            dataGridDepartments.ItemsSource = _departments;

            labelCurrentPage.DataContext = this;

            Update();

            _filteredProfessors = new ObservableCollection<ProfessorDTO>(_professors);
            _filteredStudents = new ObservableCollection<StudentDTO>(_students);
            _filteredSubjects = new ObservableCollection<SubjectDTO>(_subjects);
        }

        public int CurrentPageNumber
        {
            get { return _currentPageNumber; }
            set
            {
                _currentPageNumber = value;
                labelCurrentPage.Content = _currentPageNumber.ToString();
            }
        }

        public void SetWindowLocationAndSize()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            this.Width = (int) (3/4 * screenWidth);
            this.Height = (int) (3/4 * screenHeight);
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void InitTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += 
                (object sender, EventArgs e) => statusBarItemTime.Content = DateTime.Now.ToString("HH:mm dd-MMM-yyyy");

            _timer.Start();
        }

        private void AddNewEntity(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = tabControl.SelectedItem as TabItem;

            if (selectedTab != null)
                switch (selectedTab.Header)
                {
                    case "Students":
                        AddStudentWindow addStudentWindow = new AddStudentWindow(_controller);
                        addStudentWindow.ShowDialog();
                        break;
                    case "Professors": 
                        AddProfessorWindow addProfessorWindow = new AddProfessorWindow(_controller);
                        addProfessorWindow.ShowDialog();
                        break;
                    case "Subjects": 
                        AddSubjectWindow addSubjectWindow = new AddSubjectWindow(_controller, _professors);
                        addSubjectWindow.ShowDialog();
                        break;
                }
        }

        private void EditEntity(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = tabControl.SelectedItem as TabItem;

            if (selectedTab == null)
                return;

            switch (selectedTab.Header)
            {
                case "Students":
                    if (dataGridStudents.SelectedItem != null)
                    {
                        EditStudentWindow editStudentWindow = new EditStudentWindow(_controller, dataGridStudents.SelectedItem as StudentDTO);
                        editStudentWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please select a student to edit!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case "Professors":  
                    if (dataGridProfessor.SelectedItem != null)
                    {
                        EditProfessorWindow editProfessorWindow = new EditProfessorWindow(_controller, dataGridProfessor.SelectedItem as ProfessorDTO);
                        editProfessorWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please select a professor to edit!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case "Subjects":
                    if (dataGridSubjects.SelectedItem != null)
                    {
                        EditSubjectWindow editSubjectWindow = new EditSubjectWindow(_controller, dataGridSubjects.SelectedItem as SubjectDTO, _professors);
                        editSubjectWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please select a subject to edit!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case "Departments":
                    if (dataGridDepartments.SelectedItem != null)
                    {
                        AddChiefToDepartmentWindow addChiefToDepartment = new AddChiefToDepartmentWindow(_controller, dataGridDepartments.SelectedItem as DepartmentDTO);
                        addChiefToDepartment.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please select a department to edit!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
            }

        }

        private void DeleteEntity(object sender, RoutedEventArgs e) 
        {
            TabItem selectedTab = tabControl.SelectedItem as TabItem;

            if (selectedTab != null)
                switch (selectedTab.Header)
                {
                    case "Students":
                        deleteStudent();
                        break;
                    case "Professors":
                        deleteProfessor();
                        break;
                    case "Subjects":
                        deleteSubject();
                        break;
                }

        }

        private void deleteProfessor()
        {
            if (dataGridProfessor.SelectedItem != null)
            {
                MessageBoxResult dr = MessageBox.Show("Are you sure you want to delete this professor?", "Delete professor", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dr == MessageBoxResult.Yes)
                {
                    try
                    {
                        _controller.DeleteProfesor((dataGridProfessor.SelectedItem as ProfessorDTO).Id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select professor to delete!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void deleteStudent()
        {
            if (dataGridStudents.SelectedItem != null)
            {
                MessageBoxResult dr = MessageBox.Show("Are you sure you want to delete this student?", "Delete student", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dr == MessageBoxResult.Yes)
                {
                    try
                    {
                        _controller.DeleteStudent((dataGridStudents.SelectedItem as StudentDTO).Id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select student to delete!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void deleteSubject()
        {
            if (dataGridSubjects.SelectedItem != null)
            {
                MessageBoxResult dr = MessageBox.Show("Are you sure you want to delete this subject?", "Delete subject", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dr == MessageBoxResult.Yes)
                {
                    try
                    {
                        _controller.DeleteSubject((dataGridSubjects.SelectedItem as SubjectDTO).Id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select subject to delete!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void Update()
        {
            fillStudentDTOList();
            fillProfessorDTOList();
            fillSubjectsDTOList();
            fillDepartmeDTOList();

            ApplySearch(this, new RoutedEventArgs());
            RestartTotalNumberOfPages();
            ApplyPaging(this, new RoutedEventArgs());
        }

        private void fillProfessorDTOList()
        {
            _professors.Clear();
            foreach (Professor p in _controller.GetAllProfessors())
            {
                _professors.Add(new ProfessorDTO(p));
            }
        }

        private void fillStudentDTOList()
        {
            _students.Clear();
            foreach (Student s in _controller.GetAllStudents())
            {
                _students.Add(new StudentDTO(s));
            }
        }

        private void fillSubjectsDTOList()
        {
            _subjects.Clear();
            foreach (Subject s in _controller.GetAllSubjects())
            {
                _subjects.Add(new SubjectDTO(s));
            }
        }

        private void fillDepartmeDTOList()
        {
            _departments.Clear();
            foreach (Department d in _controller.GetAllDepartments())
            {
                _departments.Add(new DepartmentDTO(d));
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            _controller.SaveAllToStorage();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            _controller.SaveAllToStorage();
            Close();
        }

        private void OpenAbout(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Version 0.1.7 - Made by passionate developers from CerTech-Solutions®\n\nNemanja Zekanovic \n\t Young developer who has a lot more to learn,\n\t also known as telepnemanja\nNikola Kuslakovic \n\t Legends tell that he is \n\t the greateast programmer of all time");
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void OpenStudents(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabItemStudents;
        }

        private void OpenProfessors(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabItemProfessors;
        }

        private void OpenSubjects(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabItemSubjects;
        }

        private void ApplySearch(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedItem == null)
                return;

            if(tabControl.SelectedItem == tabItemStudents)
            {
                _filteredStudents = new ObservableCollection<StudentDTO>(_students.Where(x => FilterStudent(x)));
            }
            else if (tabControl.SelectedItem == tabItemProfessors)
            {
                _filteredProfessors = new ObservableCollection<ProfessorDTO>(_professors.Where(x => FilterProfessor(x)));
            }
            else if (tabControl.SelectedItem == tabItemSubjects)
            {
                _filteredSubjects =  new ObservableCollection<SubjectDTO>(_subjects.Where(x => FilterSubject(x)));
            }
        }

        private void RestartTotalNumberOfPages()
        {
            int totalNumberOfItems = 1;

            if (tabControl.SelectedItem == null)
                return;

            if (tabControl.SelectedItem == tabItemStudents)
            {
                totalNumberOfItems = _filteredStudents.Count;
            }
            else if (tabControl.SelectedItem == tabItemProfessors)
            {
                totalNumberOfItems = _filteredProfessors.Count;
            }
            else if (tabControl.SelectedItem == tabItemSubjects)
            {
                totalNumberOfItems = _filteredSubjects.Count;
            }

            _totalNumberOfPages = (int)Math.Ceiling((double)totalNumberOfItems / _maxItemsPerPage);
        }

        private void ApplyPaging(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedItem == null)
                return;

            if (tabControl.SelectedItem == tabItemStudents)
            {
                dataGridStudents.ItemsSource = _filteredStudents.Skip((CurrentPageNumber - 1) * _maxItemsPerPage).Take(_maxItemsPerPage);
            }
            else if (tabControl.SelectedItem == tabItemProfessors)
            {
                dataGridProfessor.ItemsSource = _filteredProfessors.Skip((CurrentPageNumber - 1) * _maxItemsPerPage).Take(_maxItemsPerPage);
            }
            else if (tabControl.SelectedItem == tabItemSubjects)
            {
                dataGridSubjects.ItemsSource = _filteredSubjects.Skip((CurrentPageNumber - 1) * _maxItemsPerPage).Take(_maxItemsPerPage);
            }
        }

        private void MoveToLeftPage(object sender, RoutedEventArgs e)
        {
            CurrentPageNumber = Math.Max(1, CurrentPageNumber - 1);

            buttonRightPage.IsEnabled = true;
            buttonRightPage.Opacity = 1.0;

            if(CurrentPageNumber == 1)
            {
                buttonLeftPage.IsEnabled = false;
                buttonLeftPage.Opacity = 0.5;
            }
            else
            {
                buttonRightPage.IsEnabled = true;
                buttonRightPage.Opacity = 1.0;
            }

            ApplyPaging(sender, e);
        }

        private void MoveToRightPage(object sender, RoutedEventArgs e)
        {
            CurrentPageNumber = Math.Min(_totalNumberOfPages, CurrentPageNumber + 1);

            buttonLeftPage.IsEnabled = true;
            buttonLeftPage.Opacity = 1.0;

            if(CurrentPageNumber == _totalNumberOfPages)
            {
                buttonRightPage.IsEnabled = false;
                buttonRightPage.Opacity = 0.5;
            }
            else
            {
                buttonAdd.IsEnabled = true;
                buttonAdd.Opacity = 1.0;
            }

            ApplyPaging(sender, e);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ApplySearch(sender, e);
            CurrentPageNumber = 1;
            RestartTotalNumberOfPages();
            ApplyPaging(sender, e);
        }

        private void TextBoxSearchKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Search(sender, e);
            }
        }

        private bool FilterProfessor(object item)
        {
            ProfessorDTO p = (ProfessorDTO)item;
            string[] words = textBoxSearch.Text.Split(", ");

            words = words.Select(w => w.ToLower().Replace(" ", "")).ToArray();

            if (words.Length == 1)
            {
                if (p.Surname.ToLower().Contains(words[0]))
                    return true;
            }
            else if (words.Length == 2)
            {
                if (p.Surname.ToLower().Contains(words[0]) 
                        && p.Name.ToLower().Contains(words[1]))
                    return true;
            }
            return false;
        }

        private bool FilterStudent(object item)
        {
            StudentDTO p = (StudentDTO)item;
            string[] words = textBoxSearch.Text.Split(", ");

            words = words.Select(w => w.ToLower().Replace(" ", "")).ToArray();

            if (words.Length == 1)
            {
                if (p.Surname.ToLower().Contains(words[0]))
                    return true;
            }
            else if (words.Length == 2)
            {
                if (p.Surname.ToLower().Contains(words[0]) 
                        && p.Name.ToLower().Contains(words[1]))
                    return true;
            }
            else if (words.Length == 3)
            {
                if (p.Index.ToLower().Replace(" ", "").Contains(words[0])
                        && p.Name.ToLower().Contains(words[1])
                        && p.Surname.ToLower().Contains(words[2]))
                    return true;
            }
            return false;
        }

        private bool FilterSubject(object item)
        {
            SubjectDTO p = (SubjectDTO)item;
            string[] words = textBoxSearch.Text.Split(", ");

            words = words.Select(w => w.ToLower().Replace(" ", "")).ToArray();

            if (words.Length == 1)
            {
                if (p.Name.ToLower().Contains(words[0]))
                    return true;
            }
            else if (words.Length == 2)
            {
                if (p.Name.ToLower().Contains(words[0])
                        && p.Code.ToLower().Contains(words[1]))
                    return true;
            }
            return false;
        }

        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = tabControl.SelectedItem as TabItem;

            if (selectedTab == null)
                return;

            switch (selectedTab.Header)
            {
                case "Students":
                    if (dataGridStudents.SelectedItem != null)
                    {
                        InfoStudentWindow infoSubjectWindow = new InfoStudentWindow(dataGridStudents.SelectedItem as StudentDTO);
                        infoSubjectWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please select a student for more information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case "Professors":
                    if (dataGridProfessor.SelectedItem != null)
                    {
                        InfoProfessorWindow infoProfessorWindow = new InfoProfessorWindow(dataGridProfessor.SelectedItem as ProfessorDTO, _students);
                        infoProfessorWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please select a professor for more information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case "Subjects":
                    if (dataGridSubjects.SelectedItem != null)
                    {
                        InfoSubjectWindow infoSubjectWindow = new InfoSubjectWindow(_controller, dataGridSubjects.SelectedItem as SubjectDTO);
                        infoSubjectWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please select a subject for more information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case "Departments":
                    if (dataGridDepartments.SelectedItem != null)
                    {
                        InfoDepartmentWindow infoDepartmentWindow = new InfoDepartmentWindow(dataGridDepartments.SelectedItem as DepartmentDTO);
                        infoDepartmentWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please select a department for more information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
            }
        }

        private void TabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is not TabControl)
                return;

            CurrentPageNumber = 1;
            RestartTotalNumberOfPages();
            ApplyPaging(sender, e);

            if(tabControl.SelectedItem == tabItemDepartments)
            {
                buttonAdd.IsEnabled = false;
                buttonAdd.Opacity = 0.5;

                buttonDelete.IsEnabled = false;
                buttonDelete.Opacity = 0.5;

                buttonSearch.IsEnabled = false;
                buttonSearch.Opacity = 0.5;

                textBoxSearch.IsEnabled = false;
                textBoxSearch.Opacity = 0.5;
            }
            else
            {
                buttonAdd.IsEnabled = true;
                buttonAdd.Opacity = 1.0;

                buttonDelete.IsEnabled = true;
                buttonDelete.Opacity = 1.0;

                buttonSearch.IsEnabled = true;
                buttonSearch.Opacity = 1.0;

                textBoxSearch.IsEnabled = true;
                textBoxSearch.Opacity = 1.0;

                buttonLeftPage.IsEnabled = false;
                buttonLeftPage.Opacity = 0.5;

                buttonRightPage.IsEnabled = true;
                buttonRightPage.Opacity = 1.0;

                labelCurrentPage.IsEnabled = true;
                labelCurrentPage.Opacity = 1.0;
            }

            if (_totalNumberOfPages == 1)
            {
                buttonLeftPage.IsEnabled = false;
                buttonLeftPage.Opacity = 0.5;

                buttonRightPage.IsEnabled = false;
                buttonRightPage.Opacity = 0.5;

                labelCurrentPage.IsEnabled = false;
                labelCurrentPage.Opacity = 0.5;
            }
        }
    }
}
