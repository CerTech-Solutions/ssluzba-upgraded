using CLI.Controller;
using CLI.Model;
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
    /// Interaction logic for AddProfessorToSubjectWindow.xaml
    /// </summary>
    public partial class AddProfessorToSubjectWindow : Window
    {
        private SubjectDTO _subject;
        private Controller _controller;

        public AddProfessorToSubjectWindow(Controller controller, SubjectDTO subject, ObservableCollection<ProfessorDTO> _professors)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _subject = subject;
            _controller = controller;

            listViewProfessors.ItemsSource = _professors;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddProfessor(object sender, RoutedEventArgs e)
        {
            if(listViewProfessors.SelectedItem != null)
            {
                _subject.Professor = (ProfessorDTO)listViewProfessors.SelectedItem;
                Close();
            }
            else
            {
                MessageBox.Show("You must select a professor!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }      
        }
    }
}
