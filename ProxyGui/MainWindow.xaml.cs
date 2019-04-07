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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProxyGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            btnHabilitar.Content = AppBehavior.ProxyEnabled() ? "Desabilitar" : "Habilitar";

            if (AppBehavior.CheckCertificate())
            {
                btnInstalar.Content = "Reinstalar Certificado";
            }
            else
            {
                btnInstalar.Content = "Instalar Certificado";
            }
        }
        
        private void CarregarLista(object sender, RoutedEventArgs e)
        {
        	List<string> Lista = AppBehavior.GetIpList();
        	cbUrlLista.ItemsSource = Lista;
        }

        private void btnHabilitar_Click(object sender, RoutedEventArgs e)
        {
            if (AppBehavior.ProxyEnabled())
            {
                AppBehavior.DisableProxy();
                btnHabilitar.Content = "Habilitar";
            }
            else
            {
            	AppBehavior.SetProxy(cbUrlLista.Items[this.cbUrlLista.SelectedIndex].ToString());
                btnHabilitar.Content = "Desabilitar";
            }
        }

        private void btnInstalar_Click(object sender, RoutedEventArgs e)
        {
            if (AppBehavior.CheckCertificate())
            {
                MessageBoxResult result = MessageBox.Show("Deseja reinstalar o certificado?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    AppBehavior.InstallCertificate();
                    btnInstalar.Content = "Reinstalar Certificado";
                }
            }
            else
            {
                AppBehavior.InstallCertificate();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //AppBehavior.DisableProxy();
        }

        private void MenuSobre_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
    }
}
