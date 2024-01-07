using CLI.Controller;
using GUI.DTO;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for InfoSubjectWindow.xaml
    /// </summary>
    public partial class InfoSubjectWindow : Window
    {
        SubjectDTO _firstSubject;
        Controller _controller;
        List<SubjectDTO> _subjects;
        List<StudentDTO> _students;

        public InfoSubjectWindow(Controller controller, SubjectDTO firstSubject)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _controller = controller;
            _firstSubject = firstSubject;

            textBoxFirstSubject.Text = _firstSubject.CodeAndName;

            _subjects = _controller.GetAllSubjects()
                .FindAll(s => s.Id != _firstSubject.Id)
                .Select(s => new SubjectDTO(s))
                .ToList();

            _students = _controller.GetAllStudents().Select(s => new StudentDTO(s)).ToList();

            listViewSubjects.ItemsSource = _subjects;
        }

        private void ListViewSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubjectDTO secondSubject = (SubjectDTO) listViewSubjects.SelectedItem;

            List<StudentDTO> takeBothSubjects = _students.FindAll(s => 
                s.NotPassedSubjects.Any(sub => sub.Id == _firstSubject.Id) 
                && s.NotPassedSubjects.Any(sub => sub.Id == secondSubject.Id)
                );

            List<StudentDTO> passedFirstOnly = _students.FindAll(s =>
                s.PassedSubjects.Any(sub => sub.Subject.Id == _firstSubject.Id)
                && s.NotPassedSubjects.Any(sub => sub.Id == secondSubject.Id)
                && !s.PassedSubjects.Any(sub => sub.Subject.Id == secondSubject.Id)
                );

            List<StudentDTO> passedSecondOnly = _students.FindAll(s =>
                s.PassedSubjects.Any(sub => sub.Subject.Id == secondSubject.Id)
                && s.NotPassedSubjects.Any(sub => sub.Id == _firstSubject.Id)
                && !s.PassedSubjects.Any(sub => sub.Subject.Id == _firstSubject.Id)
                );

            listViewStudents.ItemsSource = takeBothSubjects;
            listViewPassedFirstOnly.ItemsSource = passedFirstOnly;
            listViewPassedSecondOnly.ItemsSource = passedSecondOnly;
        }
    }
}
