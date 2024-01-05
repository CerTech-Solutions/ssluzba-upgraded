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
    /// Interaction logic for InfoProfessorWindow.xaml
    /// </summary>
    public partial class InfoProfessorWindow : Window
    {
        private ProfessorDTO _professorDTO;
        private ObservableCollection<StudentDTO> _students;

        public InfoProfessorWindow(ProfessorDTO professorDTO, ObservableCollection<StudentDTO> students)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _professorDTO = professorDTO;
            _students = new ObservableCollection<StudentDTO>(students);

            ObservableCollection<StudentDTO> studentsForProfessor = createNewStudentList();
            listViewStudents.ItemsSource = studentsForProfessor;
        }

        private ObservableCollection<StudentDTO> createNewStudentList()
        {
            ObservableCollection<StudentDTO> studentsForProfessor = new ObservableCollection<StudentDTO>();

            foreach (StudentDTO student in _students)
            {
                ObservableCollection<SubjectDTO> subjects = student.NotPassedSubjects;

                foreach(SubjectDTO subj in subjects)
                {
                    if(subj.Professor.Id == _professorDTO.Id && !studentsForProfessor.Any(std => std.Id == student.Id))
                    {
                        studentsForProfessor.Add(student);
                    }
                }
            }

            return studentsForProfessor;
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
