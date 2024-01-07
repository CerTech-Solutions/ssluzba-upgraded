using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            _students = createNewStudentList();

            listViewStudents.ItemsSource = _students;
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

        private void ApplySearch(object sender, RoutedEventArgs e)
        {
            ICollectionView collectionView;

            collectionView = CollectionViewSource.GetDefaultView(_students);
            collectionView.Filter = FilterStudent;

            collectionView.Refresh();
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

        private void TextBoxSearchKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ApplySearch(sender, e);
            }
        }
    }
}
