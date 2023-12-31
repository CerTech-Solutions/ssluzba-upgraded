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

        private ObservableCollection<ProfessorDTO> _professors;
        private ObservableCollection<StudentDTO> _students;
        private ObservableCollection<SubjectDTO> _subjects;
        private ObservableCollection<DepartmentDTO> _departments;

        public MainWindow()
        {
            InitializeComponent();
            SetWindowLocationAndSize();

            _controller = new Controller();
            _controller.publisher.Subscribe(this);

            // Initializing timer for statusbar
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimeTicker;
            _timer.Start();

            _professors = new ObservableCollection<ProfessorDTO>();
            _students = new ObservableCollection<StudentDTO>();
            _subjects = new ObservableCollection<SubjectDTO>();
            _departments = new ObservableCollection<DepartmentDTO>();

            dataGridProfessor.ItemsSource = _professors;
            dataGridStudents.ItemsSource = _students;
            dataGridSubjects.ItemsSource = _subjects;
            dataGridDepartments.ItemsSource = _departments;

            Update();
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

        private void TimeTicker(object sender, EventArgs e)
        {
            statusBarItemTime.Content = DateTime.Now.ToString("HH:mm:ss dd-MMM-yyyy");
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
                        AddChiefToDepartment addChiefToDepartment = new AddChiefToDepartment(_controller, dataGridDepartments.SelectedItem as DepartmentDTO);
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
            MessageBox.Show("Version 0.1.7 - Made by passionate developers from CerTech-Solutions®\n\nNemanja Zekanovic \n\t Young developer who has a lot more to learn,\n\t also known as telepnemanja\nNikola Kuslakovic \n\t Legends tell that he is \n\t the greateast programmer of all time");
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
            ICollectionView collectionView;

            if (tabControl.SelectedItem == null)
                return;

            if(tabControl.SelectedItem == tabItemStudents)
            {
                collectionView = CollectionViewSource.GetDefaultView(_students);
                collectionView.Filter = FilterStudent;
            }
            else if (tabControl.SelectedItem == tabItemProfessors)
            {
                collectionView = CollectionViewSource.GetDefaultView(_professors);
                collectionView.Filter = FilterProfessor;
            }
            else if (tabControl.SelectedItem == tabItemSubjects)
            {
                collectionView = CollectionViewSource.GetDefaultView(_subjects);
                collectionView.Filter = FilterSubject;
            }
            else
            {
                return;
            }

            collectionView.Refresh();
        }

        private void TextBoxSearchKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                ApplySearch(sender, e);
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
            // TODO - Implement
            return true;
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

                    }
                    else
                    {
                        MessageBox.Show("Please select a student for more information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case "Professors":
                    if (dataGridProfessor.SelectedItem != null)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Please select a professor for more information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case "Subjects":
                    if (dataGridSubjects.SelectedItem != null)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Please select a subject for more information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case "Departments":
                    if (dataGridDepartments.SelectedItem != null)
                    {

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
            if(tabControl.SelectedItem == tabItemDepartments)
            {
                buttonAdd.IsEnabled = false;
                buttonAdd.Opacity = 0.5;

                buttonDelete.IsEnabled = false;
                buttonDelete.Opacity = 0.5;
            }
            else
            {
                buttonAdd.IsEnabled = true;
                buttonAdd.Opacity = 1.0;

                buttonDelete.IsEnabled = true;
                buttonDelete.Opacity = 1.0;
            }
            
        }
    }
}
