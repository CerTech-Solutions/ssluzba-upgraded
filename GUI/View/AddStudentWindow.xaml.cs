using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for AddStudentWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        private HeadDAO _headDAO;
        private Brush _defaultBrushBorder;

        public StudentDTO _studentDTO { get; set; }

        public AddStudentWindow(HeadDAO headDAO)
        {
            InitializeComponent();
            DataContext = this;

            _headDAO = headDAO;
            _defaultBrushBorder = textBoxName.BorderBrush.Clone();

            labelError.Content = string.Empty;
            comboBoxStatus.SelectedItem = comboBoxItemB;

            // Initializing DTO objects
            _studentDTO = new StudentDTO();
            DataContext = _studentDTO;
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
                        if(textBox.Text == string.Empty)
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
            textBoxCurrentYear.Text = string.Empty;

            textBoxStreet.Text = string.Empty;
            textBoxNumber.Text = string.Empty;
            textBoxCity.Text = string.Empty;
            textBoxCountry.Text = string.Empty;

            textBoxCourseLabel.Text = string.Empty;
            textBoxRegNumber.Text = string.Empty;
            textBoxEnrollmentYear.Text = string.Empty;
        }

        private void AddStudent(object sender, RoutedEventArgs e)
        {
            if(InputCheck())
            {
                _headDAO.daoStudent.AddObject(_studentDTO.ToStudent());

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
