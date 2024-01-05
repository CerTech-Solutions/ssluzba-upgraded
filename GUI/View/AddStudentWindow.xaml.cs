﻿using CLI.Controller;
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
        private Controller _controller;
        private StudentDTO _studentDTO;
        private Brush _defaultBrushBorder;

        public AddStudentWindow(Controller controller)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _controller = controller;
            _defaultBrushBorder = textBoxName.BorderBrush.Clone();

            comboBoxStatus.SelectedItem = comboBoxItemB;

            _studentDTO = new StudentDTO();
            DataContext = _studentDTO;
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
                    if(textBox.Text == string.Empty)
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

            if(!int.TryParse(textBoxRegNumber.Text, out _))
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
                buttonAdd.IsEnabled = true;
            else
                buttonAdd.IsEnabled = false;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            
            if(comboBoxStatus.SelectedItem == comboBoxItemB)
                _studentDTO.Status = StatusEnum.B;
            else
                _studentDTO.Status = StatusEnum.S;

            _controller.AddStudent(_studentDTO.ToStudent());
            Close();
        }
        
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
