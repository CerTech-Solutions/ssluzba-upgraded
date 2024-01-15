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
using System.Windows.Automation.Provider;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private App app;
        private const string SRB = "sr-RS";
        private const string ENG = "en-US";

        private DispatcherTimer _timer;
        private Controller _controller;

        private int _currentPageNumber = 1;
        private int _maxItemsPerPage = 4;
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

            labelCurrentPage.DataContext = this;

            Update();

            _filteredProfessors = new ObservableCollection<ProfessorDTO>(_professors);
            _filteredStudents = new ObservableCollection<StudentDTO>(_students);
            _filteredSubjects = new ObservableCollection<SubjectDTO>(_subjects);

            dataGridProfessor.ItemsSource = _filteredProfessors;
            dataGridStudents.ItemsSource = _filteredStudents;
            dataGridSubjects.ItemsSource = _filteredSubjects;
            dataGridDepartments.ItemsSource = _departments;

            app = (App) Application.Current;
            app.ChangeLanguage(ENG);
        }

        private void ChangeLangToSerbian(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(SRB);
        }

        private void ChangeLangToEnglish(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(ENG);
        }

        public int CurrentPageNumber
        {
            get { return _currentPageNumber; }
            set
            {
                _currentPageNumber = value;
                labelCurrentPage.Content = $"{_currentPageNumber} / {_totalNumberOfPages}";
            }
        }

        public int TotalNumberOfPages
        {
            get { return _totalNumberOfPages; }
            set
            {
                _totalNumberOfPages = value;
                labelCurrentPage.Content = $"{_currentPageNumber} / {_totalNumberOfPages}";
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

            if (selectedTab == null)
                return;

            if (selectedTab == tabItemStudents)
            {
                AddStudentWindow addStudentWindow = new AddStudentWindow(_controller);
                addStudentWindow.ShowDialog();
            }
            else if (selectedTab == tabItemProfessors)
            {
                AddProfessorWindow addProfessorWindow = new AddProfessorWindow(_controller);
                addProfessorWindow.ShowDialog();
            }
            else if (selectedTab == tabItemSubjects)
            {
                AddSubjectWindow addSubjectWindow = new AddSubjectWindow(_controller, _professors);
                addSubjectWindow.ShowDialog();
            }
        }

        private void EditEntity(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = tabControl.SelectedItem as TabItem;

            if (selectedTab == null)
                return;

            if (selectedTab == tabItemStudents)
            {
                if (dataGridStudents.SelectedItem != null)
                {
                    EditStudentWindow editStudentWindow = new EditStudentWindow(_controller, dataGridStudents.SelectedItem as StudentDTO);
                    editStudentWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select a student to edit!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if (selectedTab == tabItemProfessors)
            {
                if (dataGridProfessor.SelectedItem != null)
                {
                    EditProfessorWindow editProfessorWindow = new EditProfessorWindow(_controller, dataGridProfessor.SelectedItem as ProfessorDTO);
                    editProfessorWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select a professor to edit!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if (selectedTab == tabItemSubjects)
            {
                if (dataGridSubjects.SelectedItem != null)
                {
                    EditSubjectWindow editSubjectWindow = new EditSubjectWindow(_controller, dataGridSubjects.SelectedItem as SubjectDTO, _professors);
                    editSubjectWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select a subject to edit!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if (selectedTab == tabItemDepartments)
            {
                if (dataGridDepartments.SelectedItem != null)
                {
                    AddChiefToDepartmentWindow addChiefToDepartment = new AddChiefToDepartmentWindow(_controller, dataGridDepartments.SelectedItem as DepartmentDTO);
                    addChiefToDepartment.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select a department to edit!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void DeleteEntity(object sender, RoutedEventArgs e) 
        {
            TabItem selectedTab = tabControl.SelectedItem as TabItem;

            if (selectedTab == null)
                return;

            if (selectedTab == tabItemStudents)
            {
                DeleteStudent();
            }
            else if (selectedTab == tabItemProfessors)
            {
                DeleteProfessor();
            }
            else if (selectedTab == tabItemSubjects)
            {
                DeleteSubject();
            }
        }

        private void DeleteProfessor()
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

        private void DeleteStudent()
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

        private void DeleteSubject()
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
            _students.Clear();
            _controller.GetAllStudents().ForEach(s => _students.Add(new StudentDTO(s)));

            _professors.Clear();
            _controller.GetAllProfessors().ForEach(p => _professors.Add(new ProfessorDTO(p)));

            _subjects.Clear();
            _controller.GetAllSubjects().ForEach(s => _subjects.Add(new SubjectDTO(s)));

            _departments.Clear();
            _controller.GetAllDepartments().ForEach(d => _departments.Add(new DepartmentDTO(d)));

            ApplySearch(this, new RoutedEventArgs());
            
            RestartTotalNumberOfPages();
            ChangeMovePageButtonsVisibility();

            ApplyPaging(this, new RoutedEventArgs());
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

            TotalNumberOfPages = (int) Math.Ceiling((double) totalNumberOfItems / _maxItemsPerPage);
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

            ChangeMovePageButtonsVisibility();

            ApplyPaging(sender, e);
        }

        private void MoveToRightPage(object sender, RoutedEventArgs e)
        {
            CurrentPageNumber = Math.Min(TotalNumberOfPages, CurrentPageNumber + 1);

            ChangeMovePageButtonsVisibility();

            ApplyPaging(sender, e);
        }

        private void ChangeMovePageButtonsVisibility()
        {
            if(TotalNumberOfPages == 1)
            {
                buttonLeftPage.IsEnabled = false;
                buttonLeftPage.Opacity = 0.5;

                labelCurrentPage.IsEnabled = false;
                labelCurrentPage.Opacity = 0.5;

                buttonRightPage.IsEnabled = false;
                buttonRightPage.Opacity = 0.5;
            }
            else if(CurrentPageNumber == TotalNumberOfPages)
            {
                buttonLeftPage.IsEnabled = true;
                buttonLeftPage.Opacity = 1.0;

                labelCurrentPage.IsEnabled = true;
                labelCurrentPage.Opacity = 1.0;

                buttonRightPage.IsEnabled = false;
                buttonRightPage.Opacity = 0.5;
            }
            else if (CurrentPageNumber == 1 && TotalNumberOfPages > 1)
            {
                buttonLeftPage.IsEnabled = false;
                buttonLeftPage.Opacity = 0.5;

                labelCurrentPage.IsEnabled = true;
                labelCurrentPage.Opacity = 1.0;

                buttonRightPage.IsEnabled = true;
                buttonRightPage.Opacity = 1.0;
            }
            else
            {
                buttonLeftPage.IsEnabled = true;
                buttonLeftPage.Opacity = 1.0;

                labelCurrentPage.IsEnabled = true;
                labelCurrentPage.Opacity = 1.0;

                buttonRightPage.IsEnabled = true;
                buttonRightPage.Opacity = 1.0;
            }
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ApplySearch(sender, e);
            CurrentPageNumber = 1;
            RestartTotalNumberOfPages();
            ApplyPaging(sender, e);
            ChangeMovePageButtonsVisibility();
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

            if (selectedTab == tabItemStudents)
            {
                if (dataGridStudents.SelectedItem != null)
                {
                    InfoStudentWindow infoSubjectWindow = new InfoStudentWindow(dataGridStudents.SelectedItem as StudentDTO);
                    infoSubjectWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select a student for more information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if (selectedTab == tabItemProfessors)
            {
                if (dataGridProfessor.SelectedItem != null)
                {
                    InfoProfessorWindow infoProfessorWindow = new InfoProfessorWindow(dataGridProfessor.SelectedItem as ProfessorDTO, _students);
                    infoProfessorWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select a professor for more information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if (selectedTab == tabItemSubjects)
            {
                if (dataGridSubjects.SelectedItem != null)
                {
                    InfoSubjectWindow infoSubjectWindow = new InfoSubjectWindow(_controller, dataGridSubjects.SelectedItem as SubjectDTO);
                    infoSubjectWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select a subject for more information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if (selectedTab == tabItemDepartments)
            {
                if (dataGridDepartments.SelectedItem != null)
                {
                    InfoDepartmentWindow infoDepartmentWindow = new InfoDepartmentWindow(dataGridDepartments.SelectedItem as DepartmentDTO);
                    infoDepartmentWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select a department for more information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
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
            }

            ChangeMovePageButtonsVisibility();
        }
    }
}
