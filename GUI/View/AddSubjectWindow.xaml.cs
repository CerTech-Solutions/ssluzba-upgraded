using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for AddSubjectWindow.xaml
    /// </summary>
    public partial class AddSubjectWindow : Window
    {
        private HeadDAO _headDAO;
        private Brush _defaultBrushBorder;

        public SubjectDTO subjectDTO { get; set; }
        public ProfessorDTO professorDTO { get; set; }

        public AddSubjectWindow(HeadDAO headDAO)
        {
            InitializeComponent();
            _headDAO = headDAO;
            DataContext = this;
            _defaultBrushBorder = textBoxName.BorderBrush.Clone();

            comboBoxProfessor.ItemsSource = headDAO.daoProfessor.GetAllObjects();

            comboBoxSemester.SelectedItem = comboBoxItemWinter;
            comboBoxProfessor.SelectedIndex = 0;

            professorDTO = new ProfessorDTO();
            subjectDTO = new SubjectDTO(professorDTO);
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

        public void AddSubject(object sender, RoutedEventArgs e)
        {
            if (InputCheck())
            {
                _headDAO.daoSubject.AddObject(subjectDTO.ToSubject());

                if (comboBoxSemester.SelectedItem == comboBoxItemWinter)
                    subjectDTO.Semester = SemesterEnum.winter;
                else
                    subjectDTO.Semester = SemesterEnum.summer;
            }
        }

        public void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
