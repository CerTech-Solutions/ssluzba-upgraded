using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;     

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();


            // Initializing timer for statusbar
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimeTicker;
            timer.Start();
        }

        private void TimeTicker(object sender, EventArgs e)
        {
            statusBarItemTime.Content = DateTime.Now.ToString("HH:mm:ss dd-MMM-yyyy");
        }

        private void AddNewEntity(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = tabControl.SelectedItem as TabItem;

            if (selectedTab != null)
                switch (selectedTab.Content)
                {
                    case "Students": break;
                    case "Professors": break;
                    case "Subjects": break;
                }
        }

        private void EditEntity(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab = tabControl.SelectedItem as TabItem;

            if (selectedTab != null)
                switch (selectedTab.Content)
                {
                    case "Students": break;
                    case "Professors": break;
                    case "Subjects": break;
                }
        }

        private void DeleteEntity(object sender, RoutedEventArgs e) 
        {
            
        }
    }
}
