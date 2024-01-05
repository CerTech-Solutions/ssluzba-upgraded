using CLI.Controller;
using CLI.Model;
using CLI.Observer;
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
    /// Interaction logic for EditProfessorWindow.xaml
    /// </summary>
    public partial class EditProfessorWindow : Window, IObserver
    {
        private Controller _controller;
        private ProfessorDTO _professor;

        private Brush _defaultBrushBorder;

        public EditProfessorWindow(Controller controller, ProfessorDTO professorOld)
        {   
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _controller = controller;

            _defaultBrushBorder = textBoxName.BorderBrush.Clone();

            _professor = new ProfessorDTO(professorOld);
            DataContext = _professor;

            dataGridSubjects.ItemsSource = _professor.Subjects;

            controller.publisher.Subscribe(this);
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            _controller.UpdateProfessor(_professor.ToProfessor());

            Close();
        }

        private bool EmptyTextBoxCheck()
        {
            bool validInput = true;

            foreach (var grid in stackPanel.Children.OfType<Grid>())
            {
                foreach (var control in grid.Children)
                {
                    if (control is not TextBox)
                        continue;

                    TextBox textBox = (TextBox)control;
                    if (textBox.Text == string.Empty)
                    {
                        BorderBrushToRed(textBox);
                        validInput = false;
                    }
                    else
                    {
                        BorderBrushToDefault(textBox);
                    }
                }
            }

            return validInput;
        }

        private void BorderBrushToRed(TextBox textBox)
        {
            textBox.BorderBrush = Brushes.Red;
            textBox.BorderThickness = new Thickness(1.5);
        }

        private void BorderBrushToDefault(TextBox textBox)
        {
            textBox.BorderBrush = _defaultBrushBorder;
            textBox.BorderThickness = new Thickness(1);
        }

        private bool InputCheck()
        {
            bool validInput = EmptyTextBoxCheck();

            if (!int.TryParse(textBoxServiceYears.Text, out _))
            {
                BorderBrushToRed(textBoxServiceYears);
                validInput = false;
            }
            else
            {
                if(int.Parse(textBoxServiceYears.Text) < 0)
                {
                    BorderBrushToRed(textBoxServiceYears);
                    validInput = false;
                }
                else
                    BorderBrushToDefault(textBoxServiceYears);
            }

            return validInput;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (InputCheck())
                buttonUpdate.IsEnabled = true;
            else
                buttonUpdate.IsEnabled = false;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddSubject(object sender, RoutedEventArgs e)
        {
            AddSubjectToProfessor addSubjectToProfessor = new AddSubjectToProfessor(_controller, _professor);
            addSubjectToProfessor.ShowDialog();
        }

        private void DeleteSubject(object sender, RoutedEventArgs e)
        {
            if(dataGridSubjects.SelectedItem == null)
            {
                MessageBox.Show("Please select subject to delete!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SubjectDTO subjectDTO = (SubjectDTO) dataGridSubjects.SelectedItem;
            _controller.DeleteSubjectFromProfessorList(subjectDTO.Id, _professor.Id);
        }

        public void Update()
        {
            Professor prof = _controller.GetAllProfessors().Find(p => p.Id == _professor.Id);

            _professor.Subjects.Clear();
            foreach (Subject subject in prof.Subjects)
                    _professor.Subjects.Add(new SubjectDTO(subject, _professor));
        }
    }
}
