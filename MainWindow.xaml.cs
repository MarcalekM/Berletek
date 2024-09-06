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

namespace Bérletek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Utas> utasok = new();
        public MainWindow()
        {
            InitializeComponent();
            using StreamReader sr = new(
                path: @"..\..\..\src\utasadat.txt",
                encoding: Encoding.UTF8);
            while (!sr.EndOfStream) utasok.Add(new(sr.ReadLine()));
        }

        private void UtasokSzama_Click(object sender, RoutedEventArgs e)
        {
            int f3 = utasok.Count();
            MessageBox.Show($"A mai napon {f3} utas szállt fel a buszra", "Utasok száma", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ElutasitottUtasok_Click(object sender, RoutedEventArgs e)
        {
            int f4 = 0;
            foreach (var utas in utasok)
            {
                if (utas.ervenyesseg == "0") f4++;
                else
                {
                    string datum = utas.datum.Split('-').First();
                    string ervenyes = utas.ervenyesseg;
                }
            }
            MessageBox.Show($"A mai napon {f4} utast kellett elutasítani", "Elutasított utasok száma", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}