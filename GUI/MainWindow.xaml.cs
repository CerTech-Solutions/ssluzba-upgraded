using CLI.DAO;
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

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private HeadDAO _headDAO;

        private List<ProfessorDTO> _professors;
        private List<StudentDTO> _students;
        private List<SubjectDTO> _subjects;

        public MainWindow()
        {
            InitializeComponent();

            _headDAO = new HeadDAO();

            // Initializing timer for statusbar
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimeTicker;
            _timer.Start();

            fillProfessorDTOList();
            fillStudentDTOList();
            fillSubjectsDTOList();
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
                        AddStudentWindow addStudentWindow = new AddStudentWindow(_headDAO);
                        addStudentWindow.ShowDialog();
                        break;
                    case "Professors": 
                        AddProfessorWindow addProfessorWindow = new AddProfessorWindow(_headDAO);
                        addProfessorWindow.ShowDialog();
                        break;
                    case "Subjects": 
                        AddSubjectWindow addSubjectWindow = new AddSubjectWindow(_headDAO, _professors);
                        addSubjectWindow.ShowDialog();
                        break;
                }

            fillProfessorDTOList();
            fillStudentDTOList();
            fillSubjectsDTOList();
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
                        EditStudentWindow editStudentWindow = new EditStudentWindow(_headDAO, dataGridStudents.SelectedItem as StudentDTO);
                        editStudentWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please select an student to edit!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case "Professors":  
                    if (dataGridProfessor.SelectedItem != null)
                    {
                        EditProfessorWindow editProfessorWindow = new EditProfessorWindow(_headDAO, dataGridProfessor.SelectedItem as ProfessorDTO);
                        editProfessorWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please select an professor to edit!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case "Subjects":
                    if (dataGridSubjects.SelectedItem != null)
                    {
                        EditSubjectWindow editSubjectWindow = new EditSubjectWindow(_headDAO, dataGridSubjects.SelectedItem as SubjectDTO, _professors);
                        editSubjectWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please select an subject to edit!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
            }

            fillProfessorDTOList();
            fillStudentDTOList();
            fillSubjectsDTOList();
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

            fillProfessorDTOList();
            fillStudentDTOList();
            fillSubjectsDTOList();
        }

        private void deleteProfessor()
        {
            if (dataGridProfessor.SelectedItem != null)
            {
                MessageBoxResult dr = MessageBox.Show("Are you sure you want to delete this professor?", "Delete professor", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (dr == MessageBoxResult.Yes)
                {
                    try
                    {
                        _headDAO.DeleteProfesor((dataGridProfessor.SelectedItem as ProfessorDTO).Id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void deleteStudent()
        {
            if (dataGridStudents.SelectedItem != null)
            {
                MessageBoxResult dr = MessageBox.Show("Are you sure you want to delete this student?", "Delete student", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (dr == MessageBoxResult.Yes)
                {
                    try
                    {
                        _headDAO.DeleteStudent((dataGridStudents.SelectedItem as StudentDTO).Id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void deleteSubject()
        {
            if (dataGridSubjects.SelectedItem != null)
            {
                MessageBoxResult dr = MessageBox.Show("Are you sure you want to delete this subject?", "Delete subject", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (dr == MessageBoxResult.Yes)
                {
                    try
                    {
                        _headDAO.DeleteSubject((dataGridSubjects.SelectedItem as SubjectDTO).Id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void fillProfessorDTOList()
        {
            _professors = new List<ProfessorDTO>();
            foreach (Professor p in _headDAO.daoProfessor.GetAllObjects())
            {
                _professors.Add(new ProfessorDTO(p));
            }
            dataGridProfessor.ItemsSource = _professors;
        }

        private void fillStudentDTOList()
        {
            _students = new List<StudentDTO>();
            foreach (Student s in _headDAO.daoStudent.GetAllObjects())
            {
                _students.Add(new StudentDTO(s));
            }
            dataGridStudents.ItemsSource = _students;
        }

        private void fillSubjectsDTOList()
        {
            _subjects = new List<SubjectDTO>();
            foreach (Subject s in _headDAO.daoSubject.GetAllObjects())
            {
                _subjects.Add(new SubjectDTO(s));
            }
            dataGridSubjects.ItemsSource = _subjects;
        }
    }
}
