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
    /// Interaction logic for EditSubjectWindow.xaml
    /// </summary>
    public partial class EditSubjectWindow : Window
    {
        private HeadDAO _headDAO;
        private SubjectDTO _subjectDTO;

        private Brush _defaultBrushBorder;

        public EditSubjectWindow(HeadDAO headDAO, SubjectDTO subjectOld)
        {
            InitializeComponent();
            _headDAO = headDAO;
            labelError.Content = string.Empty;

            _defaultBrushBorder = textBoxName.BorderBrush.Clone();

            _subjectDTO = new SubjectDTO(subjectOld);
            DataContext = _subjectDTO;
        }

        private void ApplyEdit(object sender, RoutedEventArgs e)
        {
            if (InputCheck())
            {
                Subject subj = _subjectDTO.ToSubject();

                _headDAO.daoSubject.UpdateObject(subj);

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

            return validInput;
        }



        public void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
