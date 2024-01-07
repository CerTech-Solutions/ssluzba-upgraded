using CLI.Controller;
using CLI.Model;
using CLI.Observer;
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
    /// Interaction logic for EditStudentWindow.xaml
    /// </summary>
    public partial class EditStudentWindow : Window, IObserver
    {
        private Controller _controller;
        private StudentDTO _studentDTO;

        private Brush _defaultBrushBorder;

        public EditStudentWindow(Controller controller, StudentDTO studentOld)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _controller = controller;
            _defaultBrushBorder = textBoxName.BorderBrush.Clone();

            if(studentOld.Status == StatusEnum.B)
                comboBoxStatus.SelectedItem = comboBoxItemB;
            else
                comboBoxStatus.SelectedItem = comboBoxItemS;

            _studentDTO = new StudentDTO(studentOld);
            DataContext = _studentDTO;
            dataGridPassedSubjects.ItemsSource = _studentDTO.PassedSubjects;
            dataGridNotPassedSubjects.ItemsSource = _studentDTO.NotPassedSubjects;

            controller.publisher.Subscribe(this);
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

            if (!int.TryParse(textBoxCurrentYear.Text, out _))
            {
                BorderBrushToRed(textBoxCurrentYear);
                validInput = false;
            }
            else
            {
                if (int.Parse(textBoxCurrentYear.Text) <= 0)
                {
                    BorderBrushToRed(textBoxCurrentYear);
                    validInput = false;
                }
                else
                    BorderBrushToDefault(textBoxCurrentYear);
            }

            if (!int.TryParse(textBoxRegNumber.Text, out _))
            {
                BorderBrushToRed(textBoxRegNumber);
                validInput = false;
            }
            else
            {
                if (int.Parse(textBoxRegNumber.Text) <= 0)
                {
                    BorderBrushToRed(textBoxRegNumber);
                    validInput = false;
                }
                else
                    BorderBrushToDefault(textBoxRegNumber);
            }

            if (!int.TryParse(textBoxEnrollmentYear.Text, out _))
            {
                BorderBrushToRed(textBoxEnrollmentYear);
                validInput = false;
            }
            else
            {
                if (int.Parse(textBoxEnrollmentYear.Text) <= 0 || int.Parse(textBoxEnrollmentYear.Text) > DateTime.Now.Year)
                {
                    BorderBrushToRed(textBoxEnrollmentYear);
                    validInput = false;
                }
                else
                    BorderBrushToDefault(textBoxEnrollmentYear);
            }

            return validInput;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (InputCheck())
                buttonUpdate.IsEnabled = true;
            else
                buttonUpdate.IsEnabled = false;
        }

        private void UpdateStudent(object sender, RoutedEventArgs e)
        {
            if (comboBoxStatus.SelectedItem == comboBoxItemB)
                _studentDTO.Status = StatusEnum.B;
            else
                 _studentDTO.Status = StatusEnum.S;

            _controller.UpdateStudent(_studentDTO.ToStudent());
            Close();
        }

        private void AddSubject(object sender, RoutedEventArgs e)
        {
            AddSubjectToStudentWindow addSubjectToStudent = new AddSubjectToStudentWindow(_controller, _studentDTO);
            addSubjectToStudent.ShowDialog();
        }

        private void PassSubject(object sender, RoutedEventArgs e)
        {
            if (dataGridNotPassedSubjects.SelectedItem != null)
            {
                SubjectDTO _subjectDTO = dataGridNotPassedSubjects.SelectedItem as SubjectDTO;
                GradeDTO _gradeDTO = new GradeDTO(_studentDTO, _subjectDTO);

                AddGradeWindow addGradeWindow = new AddGradeWindow(_controller, _gradeDTO);
                addGradeWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select subject to add grade!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteSubject(object sender, RoutedEventArgs e)
        {
            if (dataGridNotPassedSubjects.SelectedItem != null)
            {
                MessageBoxResult dr = MessageBox.Show("Are you sure you want to delete this subject from student list?", "Delete professor", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dr == MessageBoxResult.Yes)
                {
                    try
                    {
                        _controller.DeleteSubjectFromStudentList((dataGridNotPassedSubjects.SelectedItem as SubjectDTO).Id, _studentDTO.Id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select subject to delete!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            _controller.publisher.Unsubscribe(this);
            Close();
        }

        private void CancelGrade(object sender, RoutedEventArgs e)
        {
            if (dataGridPassedSubjects.SelectedItem == null)
            {
                MessageBox.Show("Please select grade to cancel!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel this grade?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                GradeDTO selectedGrade = (GradeDTO)dataGridPassedSubjects.SelectedItem;
                _controller.DeleteGrade(selectedGrade.ToGrade());
            }
        }

        public void Update()
        {
            Student student = _controller.GetAllStudents().Find(s => s.Id == _studentDTO.Id);

            _studentDTO.PassedSubjects.Clear();
            foreach (Grade grade in student.PassedSubjects)
                _studentDTO.PassedSubjects.Add(new GradeDTO(grade, _studentDTO));

            _studentDTO.NotPassedSubjects.Clear();
            foreach (Subject subject in student.NotPassedSubjects)
                _studentDTO.NotPassedSubjects.Add(new SubjectDTO(subject));

            _studentDTO.Gpa = student.GPA;
            _studentDTO.CalculateTotalEcts();
        }
    }
}
