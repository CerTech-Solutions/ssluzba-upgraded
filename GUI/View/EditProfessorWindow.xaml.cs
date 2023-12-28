using CLI.Controller;
using CLI.Model;
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
    /// Interaction logic for EditProfessorWindow.xaml
    /// </summary>
    public partial class EditProfessorWindow : Window
    {
        private Controller _controller;
        public ProfessorDTO professorDTO;

        private Brush _defaultBrushBorder;

        public EditProfessorWindow(Controller controller, ProfessorDTO professorOld)
        {   
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _controller = controller;

            _defaultBrushBorder = textBoxName.BorderBrush.Clone();

            professorDTO = new ProfessorDTO(professorOld);
            DataContext = professorDTO;
        }


        private void Update(object sender, RoutedEventArgs e)
        {
            if (InputCheck())
            {
                _controller.UpdateProfessor(professorDTO.ToProfessor());

                Close();
            }
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

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
