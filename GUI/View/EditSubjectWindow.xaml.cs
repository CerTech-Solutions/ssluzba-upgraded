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
using System.Xml.Linq;

namespace GUI
{
    /// <summary>
    /// Interaction logic for EditSubjectWindow.xaml
    /// </summary>
    public partial class EditSubjectWindow : Window
    {
        private Controller _controller;
        private SubjectDTO _subjectDTO;

        private ObservableCollection<ProfessorDTO> _professors;

        private Brush _defaultBrushBorder;

        public EditSubjectWindow(Controller controller, SubjectDTO subjectOld, ObservableCollection<ProfessorDTO> professors)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _controller = controller;

            _defaultBrushBorder = textBoxName.BorderBrush.Clone();

            _subjectDTO = new SubjectDTO(subjectOld);
            DataContext = _subjectDTO;

            //comboBoxProfessor.ItemsSource = _professors;
            _professors = new ObservableCollection<ProfessorDTO>(professors);

            if (subjectOld.Semester == SemesterEnum.winter)
                comboBoxSemester.SelectedItem = comboBoxItemWinter;
            else
                comboBoxSemester.SelectedItem = comboBoxItemSummer;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            String prof = textBoxProfessor.Text;
            prof = prof.Replace(" ", "");
            if (prof == String.Empty)
            {
                buttonAddProfessor.IsEnabled = true;
                buttonDeleteProfessor.IsEnabled = false;
            }
            else
            {
                buttonAddProfessor.IsEnabled = false;
                buttonDeleteProfessor.IsEnabled = true;
            }

            if(InputCheck())
                buttonUpdate.IsEnabled = true;
            else
                buttonUpdate.IsEnabled = false;
        }

        private void addProfessorToSubject(object sender, RoutedEventArgs e)
        {
            AddProfessorToSubjectWindow addProfessorToSubjectWindow = new AddProfessorToSubjectWindow(_controller, _subjectDTO, _professors);
            addProfessorToSubjectWindow.ShowDialog();
        }

        private void deleteProfessorFromSubject(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dr = MessageBox.Show("Are you sure you want to delete professor from subject?", "Delete professor", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dr == MessageBoxResult.Yes)
            {
                try
                {
                    _subjectDTO.Professor = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool EmptyTextBoxCheck()
        {
            bool validInput = true;

            foreach (var grid in stackPanel.Children.OfType<Grid>())
            {
                foreach (var control in grid.Children)
                {
                    if (control is TextBox)
                    {
                        TextBox textBox = (TextBox)control;
                        if (textBox.Text == string.Empty)
                        {
                            textBox.BorderBrush = Brushes.Red;
                            textBox.BorderThickness = new Thickness(2);
                            validInput = false;
                        }
                        else
                        {
                            textBox.BorderBrush = _defaultBrushBorder;
                            textBox.BorderThickness = new Thickness(1);
                        }
                    }
                }
            }

            return validInput;
        }

        private bool InputCheck()
        {
            bool validInput = EmptyTextBoxCheck();

            if (!int.TryParse(textBoxYearOfStudy.Text, out _))
            {
                BorderBrushToRed(textBoxYearOfStudy);
                validInput = false;
            }
            else
            {
                if(int.Parse(textBoxYearOfStudy.Text) <= 0)
                {
                    BorderBrushToRed(textBoxYearOfStudy);
                    validInput = false;
                }
                else
                    BorderBrushToDefault(textBoxYearOfStudy);
            }

            if (!int.TryParse(textBoxEcts.Text, out _))
            {
                BorderBrushToRed(textBoxEcts);
                validInput = false;
            }
            else
            {
                if (int.Parse(textBoxEcts.Text) <= 0)
                {
                    BorderBrushToRed(textBoxEcts);
                    validInput = false;
                }
                else
                    BorderBrushToDefault(textBoxEcts);
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

        private void Update(object sender, RoutedEventArgs e)
        {
            if(comboBoxSemester.SelectedItem == comboBoxItemWinter)
                _subjectDTO.Semester = SemesterEnum.winter;
            else
                _subjectDTO.Semester = SemesterEnum.summer;

            _controller.UpdateSubject(_subjectDTO.ToSubject());
            Close();
        }

        public void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
