using CLI.Controller;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for AddSubjectToStudent.xaml
    /// </summary>
    public partial class AddSubjectToStudent : Window
    {
        private Controller _controller;
        private StudentDTO _student;
        private ObservableCollection<SubjectDTO> _subjects;

        public AddSubjectToStudent(Controller controller, StudentDTO studentDTO)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _controller = controller;
            _student = studentDTO;

            Student stud = _controller.GetAllStudents().Find(s => s.Id == _student.Id);

            _subjects = new ObservableCollection<SubjectDTO>(
                _controller.GetAllSubjects()
                .FindAll(s => s.YearOfStudy == stud.CurrentYear && !stud.NotPassedSubjects.Contains(s))
                .Select(s => new SubjectDTO(s))
                .ToList());

            listViewSubjects.ItemsSource = _subjects;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddSubject(object sender, RoutedEventArgs e)
        {
            if(listViewSubjects.SelectedItem == null)
            {
                MessageBox.Show("Please select a subject to add!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SubjectDTO selectedSubject = (SubjectDTO) listViewSubjects.SelectedItem;

            _controller.AddSubjectToStudent(_student.Id, selectedSubject.Id);

            Close();
        }
    }
}
