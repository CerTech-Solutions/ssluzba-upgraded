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
    /// Interaction logic for InfoStudentWindow.xaml
    /// </summary>
    public partial class InfoStudentWindow : Window
    {
        private StudentDTO _studentDTO;

        public InfoStudentWindow(StudentDTO studentDTO)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _studentDTO = studentDTO;

            List<ProfessorDTO> professors = createProfessorListForStudent();

            listViewProfessors.ItemsSource = professors;
        }

        private List<ProfessorDTO> createProfessorListForStudent()
        {
            List<ProfessorDTO> professors = new List<ProfessorDTO>();

            foreach (SubjectDTO subject in _studentDTO.NotPassedSubjects)
            {
                if (subject.Professor != null && !professors.Any(prof => prof.Id == subject.Professor.Id))
                    professors.Add(subject.Professor);
            }

            return professors;
        }
    }
}
