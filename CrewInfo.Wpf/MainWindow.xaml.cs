using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrewInfo.Wpf
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

        private void PilotsButton_Click(object sender, RoutedEventArgs e)
        {
            var pilotWindow = new PilotWindow();
            pilotWindow.Show();
            this.Close();
        }

        private void StewardsButton_Click(object sender, RoutedEventArgs e)
        {
            var stewardWindow = new StewardWindow();
            stewardWindow.Show();
            this.Close();
        }
    }
}