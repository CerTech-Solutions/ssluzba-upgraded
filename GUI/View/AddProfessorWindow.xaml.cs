using CLI.Controller;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for AddProfessorWindow.xaml
    /// </summary>
    public partial class AddProfessorWindow : Window
    {
        private Controller _controller;
        private Brush _defaultBrushBorder;

        public ProfessorDTO professorDTO { get; set; }
        public AddressDTO addressDTO { get; set; }

        public AddProfessorWindow(Controller controller)
        {
            InitializeComponent();
            DataContext = this;

            _controller = controller;
            _defaultBrushBorder = textBoxName.BorderBrush.Clone();

            labelError.Content = string.Empty;

            // Initializing DTO objects
            addressDTO = new AddressDTO();
            professorDTO = new ProfessorDTO(addressDTO);
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

            return validInput;
        }

        private void ClearAllInput()
        {
            textBoxName.Text = string.Empty;
            textBoxSurname.Text = string.Empty;
            datePickerBirthDate.SelectedDate = null;
            textBoxPhoneNumber.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
            textBoxIdNumber.Text = string.Empty;
            textBoxTitle.Text = string.Empty;
            textBoxServiceYears.Text = string.Empty;

            textBoxStreet.Text = string.Empty;
            textBoxNumber.Text = string.Empty;
            textBoxCity.Text = string.Empty;
            textBoxCountry.Text = string.Empty;
        }

        private void AddProfessor(object sender, RoutedEventArgs e)
        {
            if (InputCheck())
            {
                _controller.daoProfessor.AddObject(professorDTO.ToProfessor());

                labelError.Content = "Student successuflly added!";
                labelError.Foreground = new SolidColorBrush(Colors.Green);

                ClearAllInput();
            }
            else
            {
                labelError.Content = "Invalid input!";
                labelError.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
