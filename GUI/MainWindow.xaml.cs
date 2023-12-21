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

        private int Id;

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
                        break;
                }

            fillProfessorDTOList();
        }

        private void EditEntity(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = tabControl.SelectedItem as TabItem;

            if (selectedTab != null)
                switch (selectedTab.Header)
                {
                    case "Students": break;
                    case "Professors":  
                        if (dataGridProfessor.SelectedItem != null)
                        {
                            EditProfessorWindow editProfessorWindow = new EditProfessorWindow(_headDAO, dataGridProfessor.SelectedItem as ProfessorDTO);
                            editProfessorWindow.ShowDialog();
                        }
                        break;
                    case "Subjects": break;
                }

            fillProfessorDTOList();
        }

        private void DeleteEntity(object sender, RoutedEventArgs e) 
        {
            TabItem selectedTab = tabControl.SelectedItem as TabItem;

            if (selectedTab != null)
                switch (selectedTab.Header)
                {
                    case "Students": break;
                    case "Professors":
                        if (dataGridProfessor.SelectedItem != null)
                        {
                            MessageBoxResult dr =  MessageBox.Show("Are you sure you want to delete this professor?", "Delete professor", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                            if(dr == MessageBoxResult.Yes)
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
                        break;
                    case "Subjects": break;
                }

            fillProfessorDTOList();
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
    }
}
