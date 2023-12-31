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
    /// Interaction logic for EditSubjectWindow.xaml
    /// </summary>
    public partial class EditSubjectWindow : Window
    {
        private Controller _controller;
        private SubjectDTO _subjectDTO;
        private int oldProfessorId;

        private Brush _defaultBrushBorder;

        public EditSubjectWindow(Controller controller, SubjectDTO subjectOld, ObservableCollection<ProfessorDTO> _professors)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _controller = controller;

            _defaultBrushBorder = textBoxName.BorderBrush.Clone();

            _subjectDTO = new SubjectDTO(subjectOld);
            DataContext = _subjectDTO;
            oldProfessorId = subjectOld.Professor.Id;

            comboBoxProfessor.ItemsSource = _professors;
            comboBoxProfessor.SelectedIndex = subjectOld.Professor.Id;

            if (subjectOld.Semester == SemesterEnum.winter)
                comboBoxSemester.SelectedItem = comboBoxItemWinter;
            else
                comboBoxSemester.SelectedItem = comboBoxItemSummer;
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
            if (InputCheck())
            {
                _subjectDTO.Professor = (ProfessorDTO)comboBoxProfessor.SelectedItem;
                
                if(comboBoxSemester.SelectedItem == comboBoxItemWinter)
                    _subjectDTO.Semester = SemesterEnum.winter;
                else
                    _subjectDTO.Semester = SemesterEnum.summer;

                _controller.UpdateSubject(_subjectDTO.ToSubject(), oldProfessorId);
                Close();
            }
        }

        public void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
