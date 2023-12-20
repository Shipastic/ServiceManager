using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.ServiceProcess;
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


namespace ServiceManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnServiceCommand(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnRefresh(object sender, RoutedEventArgs e)
        {
            ServiceController[] scServices;
            scServices = ServiceController.GetServices("10.71.10.155").OrderBy(s => s.DisplayName).ToArray();
            foreach (var service in scServices)
            {
                listBoxServices.Items.Add(service);
            }

        }

        private void OnExit(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnStop(object sender, RoutedEventArgs e)
        {
            
            ServiceController service = (ServiceController)listBoxServices.SelectedItem;
            string serviceName = service.DisplayName;
            ServiceController sc = new ServiceController(serviceName, "10.71.10.155");
            sc.Refresh();
            Thread.Sleep(1000);
            if (sc.Status == ServiceControllerStatus.Running)
            {
                sc.Stop();
                Thread.Sleep(1000);
                sc.Refresh();
                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    MessageBox.Show("Service Stopped!");
                }
            }
            else
            {
                MessageBox.Show("This service is already stopped!");
            }
        }

        private void OnStart(object sender, RoutedEventArgs e)
        {
            ServiceController service = (ServiceController)listBoxServices.SelectedItem;
            string serviceName = service.DisplayName;
            ServiceController sc = new ServiceController(serviceName, "10.71.10.155");
            sc.Refresh();
            Thread.Sleep(1000);
            if (sc.Status == ServiceControllerStatus.Stopped)
            {
                sc.Start();
                Thread.Sleep(1000);
                sc.Refresh();
                if (sc.Status == ServiceControllerStatus.Running)
                {
                    MessageBox.Show("Service Running!");
                }
            }
            else
            {
                MessageBox.Show("This service is already running!");
            }
        }

        private void listBoxServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the currently selected item in the ListBox.
            //string curItem = listBoxServices.SelectedItem.ToString();
            ServiceController service = (ServiceController)listBoxServices.SelectedItem;
            string serviceName = service.DisplayName;
            ServiceController sc = new ServiceController(serviceName, "10.71.10.155");
            textDisplayName.Text = serviceName;
            textStatus.Text = sc.Status.ToString();
            textType.Text = sc.ServiceType.ToString();
            textName.Text = sc.ServiceName;
        }
    }
}
