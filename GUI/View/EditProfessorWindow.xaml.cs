using CLI.DAO;
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
        private HeadDAO _headDAO;
        public ProfessorDTO professorDTO;

        private Brush _defaultBrushBorder;

        public EditProfessorWindow(HeadDAO headDAO, ProfessorDTO professorOld)
        {   
            InitializeComponent();
            _headDAO = headDAO;
            labelError.Content = string.Empty;

            _defaultBrushBorder = textBoxName.BorderBrush.Clone();

            professorDTO = new ProfessorDTO(professorOld);
            DataContext = professorDTO;
        }


        private void ApplyEdit(object sender, RoutedEventArgs e)
        {
            if (InputCheck())
            {
                Professor prof = professorDTO.ToProfessor();

                _headDAO.daoProfessor.UpdateObject(prof);

                Close();
            }
            else
            {
                labelError.Content = "Invalid input!";
                labelError.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private bool InputCheck()
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

            if (int.TryParse(textBoxServiceYears.Text, out _))
            {
                int serviceYears = int.Parse(textBoxServiceYears.Text);
                if (serviceYears < 0)
                {
                    textBoxServiceYears.BorderBrush = Brushes.Red;
                    textBoxServiceYears.BorderThickness = new Thickness(2);
                    validInput = false;
                }
                else
                {
                    textBoxServiceYears.BorderBrush = _defaultBrushBorder;
                    textBoxServiceYears.BorderThickness = new Thickness(1);
                }
            }

            return validInput;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

    
}
