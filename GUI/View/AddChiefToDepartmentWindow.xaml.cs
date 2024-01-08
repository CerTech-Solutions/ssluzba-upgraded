using CLI.Controller;
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

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddChiefToDepartmentWindow.xaml
    /// </summary>
    public partial class AddChiefToDepartmentWindow : Window
    {
        private Controller _controller;
        private DepartmentDTO _department;

        public AddChiefToDepartmentWindow(Controller controller, DepartmentDTO department)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _controller = controller;
            _department = department;

            listViewProfessors.ItemsSource = department.Professors;
        }

        private void AddChief(object sender, RoutedEventArgs e)
        {
            if(listViewProfessors.SelectedItem == null)
            {
                MessageBox.Show("Please select professor to add as chief!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                ProfessorDTO professor = (ProfessorDTO) listViewProfessors.SelectedItem;
                _controller.AddChiefToDepartment(professor.Id, _department.Id);
                Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
