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
    /// Interaction logic for InfoDepartmentWindow.xaml
    /// </summary>
    public partial class InfoDepartmentWindow : Window
    {
        private DepartmentDTO _departmentDTO;

        public InfoDepartmentWindow(DepartmentDTO departmentDTO)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _departmentDTO = departmentDTO;

            List<SubjectDTO> allSubjects = _departmentDTO.Professors.SelectMany(professor => professor.Subjects).ToList();

            listViewSubjects.ItemsSource = allSubjects;
        }
    }
}
