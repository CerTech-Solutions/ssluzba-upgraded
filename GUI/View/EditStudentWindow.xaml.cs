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
    /// Interaction logic for EditStudentWindow.xaml
    /// </summary>
    public partial class EditStudentWindow : Window
    {
        private HeadDAO _headDAO;
        public StudentDTO studentDTO;

        private Brush _defaultBrushBorder;

        public EditStudentWindow(HeadDAO headDAO, StudentDTO studentOld)
        {
            InitializeComponent();
            _headDAO = headDAO;
            labelError.Content = string.Empty;

            _defaultBrushBorder = textBoxName.BorderBrush.Clone();

            studentDTO = new StudentDTO(studentOld);
            DataContext = studentDTO;
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

        private void ApplyEdit(object sender, RoutedEventArgs e)
        {
            if (InputCheck())
            {
                Student s = studentDTO.ToStudent();

                _headDAO.daoStudent.UpdateObject(s);

                Close();
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
