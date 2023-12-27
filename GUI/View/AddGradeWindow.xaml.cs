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
    /// Interaction logic for AddGradeWindow.xaml
    /// </summary>
    public partial class AddGradeWindow : Window
    {
        Controller _controller;
        StudentDTO _studentDTO;
        SubjectDTO _subjectDTO;
        GradeDTO _gradeDTO;

        private Brush _defaultBrushBorder;

        public AddGradeWindow(Controller controller, StudentDTO student, SubjectDTO subject)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _defaultBrushBorder = textBoxName.BorderBrush.Clone();

            _controller = controller;
            _studentDTO = student;
            _subjectDTO = subject;
            _gradeDTO = new GradeDTO(student, subject);

            DataContext = _gradeDTO;
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
            int gradeNum = 0;

            if (!int.TryParse(textBoxGradeValue.Text, out _))
            { 
                BorderBrushToRed(textBoxGradeValue);
                validInput = false;
            }
            else
            {
                gradeNum = int.Parse(textBoxGradeValue.Text);
                if (gradeNum <= 5 || gradeNum > 10)
                {
                    BorderBrushToRed(textBoxGradeValue);
                    return validInput = false;
                }
                BorderBrushToDefault(textBoxGradeValue);
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

        private void Add(object sender, RoutedEventArgs e)
        {
            if(InputCheck())
            {
                _studentDTO.PassedSubjects.Add(_gradeDTO);
                _studentDTO.NotPassedSubjects.Remove(_subjectDTO);
                Close();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
