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
    /// Interaction logic for AddSubjectToProfessor.xaml
    /// </summary>
    public partial class AddSubjectToProfessor : Window
    {
        private Controller _controller;
        private ProfessorDTO _professor;
        private ObservableCollection<SubjectDTO> _subjects;

        public AddSubjectToProfessor(Controller controller, ProfessorDTO professorDTO)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _controller = controller;
            _professor = professorDTO;

            // This should be moved to the controller

            Professor prof = _controller.GetAllProfessors().Find(p => p.Id == _professor.Id);

            _subjects = new ObservableCollection<SubjectDTO>(
                _controller.GetAllSubjects()
                               
                .FindAll(s => s.Professor == null &&
                    !prof.Subjects.Exists(sub => sub.Id == s.Id))
                                              
                .Select(s => new SubjectDTO(s))
                                                             
                .ToList());

            // -----------------------------

            listViewSubjects.ItemsSource = _subjects;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddSubject(object sender, RoutedEventArgs e)
        {
            if (listViewSubjects.SelectedItem == null)
            {
                MessageBox.Show("Please select a subject to add!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SubjectDTO selectedSubject = (SubjectDTO)listViewSubjects.SelectedItem;

            _controller.AddSubjectToProfessor(selectedSubject.Id, _professor.Id);

            Close();
        }
    }
}
