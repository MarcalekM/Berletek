using System.IO;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bérletek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 30; i++) Megallo.Items.Add(i.ToString());
        }


        private void Megallo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UtasAdat_Click(object sender, RoutedEventArgs e)
        {
            SecondWindow win2 = new SecondWindow();
            win2.Show();

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string content = ((RadioButton)sender).Content.ToString();
            if (content == "Bérlet")
            {


            }
        }
    }
}