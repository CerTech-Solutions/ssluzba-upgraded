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
    /// Interaction logic for AddSubjectToStudentWindow.xaml
    /// </summary>
    public partial class AddSubjectToStudentWindow : Window
    {
        private Controller _controller;
        private StudentDTO _student;
        private List<SubjectDTO> _subjects;

        public AddSubjectToStudentWindow(Controller controller, StudentDTO studentDTO)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _controller = controller;
            _student = studentDTO;

            _subjects = new List<SubjectDTO>(
                _controller.GetSubjectsForStudent(_student.Id)
                .Select(s => new SubjectDTO(s))
                );

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

            _controller.AddSubjectToStudent(selectedSubject.Id, _student.Id);

            Close();
        }
    }
}
