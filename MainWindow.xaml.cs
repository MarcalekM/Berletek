using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
        List<string> tipusok = new(["FEB", "TAB", "NYB", "NYP", "RVS", "GYK"]);
        public MainWindow()
        {
            InitializeComponent();
            Megallo.Items.Add("Válasszon megállót!");
            Megallo.SelectedItem = Megallo.Items[0];
            for (int i = 0; i < 30; i++) Megallo.Items.Add(i.ToString());
            BerletTipus.Items.Add("Válasszon típust!");
            BerletTipus.SelectedItem = BerletTipus.Items[0];
            foreach (var tipus in tipusok) BerletTipus.Items.Add(tipus);
            Datum.Text =  DateTime.Now.ToString();
            AzonositoHossz.Content = Azonosito.Text.Length + " db";
        }

        private void UtasAdat_Click(object sender, RoutedEventArgs e)
        {
            SecondWindow win2 = new SecondWindow();
            win2.Show();
        }
        private void Megallo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Megallo.Items[0].ToString() == "Válasszon megállót!" && Megallo.Items.Count > 1) Megallo.Items.Remove(Megallo.Items[0]);
        }
        private void BerletTipus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BerletTipus.Items[0].ToString() == "Válasszon típust!" && BerletTipus.Items.Count > 1) BerletTipus.Items.Remove(BerletTipus.Items[0]);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string content = ((RadioButton)sender).Content.ToString();
            if (content == "Bérlet")
            {
                Bérlet.Visibility = Visibility.Visible;
                Jegy.Visibility = Visibility.Hidden;
            }
            if (content == "Jegy")
            {
                Jegy.Visibility = Visibility.Visible;
                Bérlet.Visibility = Visibility.Hidden;
                JegyErvenyessegErtek.Content = JegyErvenyesseg.Value.ToString() + " db";
            }
        }

        private void JegyErvenyesseg_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            JegyErvenyessegErtek.Content = ((Slider)sender).Value.ToString() + " db";
        }
        private void Azonosito_TextChanged(object sender, TextChangedEventArgs e)
        {
            AzonositoHossz.Content = Azonosito.Text.Length + " db";
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            string megallo = string.Empty;
            string datum = string.Empty;
            string ido = string.Empty;
            string azonosito = string.Empty;
            Regex r = new("[^0-9]+");
            string tipus = string.Empty;
            string ervenyes = string.Empty;
            string result = string.Empty;

            if (0 <= int.Parse(Megallo.Text) && int.Parse(Megallo.Text) < 30) megallo = Megallo.Text;
            else MessageBox.Show("Nem választott megállót!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

            if (Datum.Text != "") datum = Datum.Text.Replace(". ", "").TrimEnd('.');
            else MessageBox.Show("Nem adott meg dátumot!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

            if (Ido.Text.Length == 5 && int.Parse(Ido.Text.Substring(0, 2)) <= 23 && Ido.Text[2] == ':' && int.Parse(Ido.Text.Substring(3, 2)) <= 60) ido = Ido.Text.Replace(":", "") ;
            else if(Ido.Text == "") MessageBox.Show("Nem adott meg időpontot!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            else MessageBox.Show("Rossz formátumban adta meg az időpontot!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            

            if(Azonosito.Text.Length == 0) MessageBox.Show("Nem adott meg azonosítót!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (!r.IsMatch(Azonosito.Text) && Azonosito.Text.Length == 7) azonosito = Azonosito.Text;
            else if(Azonosito.Text.Length < 7) MessageBox.Show("Rövid az azonosító, nincs meg 7 számjegy!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            else MessageBox.Show("Rossz formátumban adta meg az azonosítót!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

            if (RadioBérlet.IsChecked == true)
            {
                if (tipusok.Contains(BerletTipus.SelectedItem.ToString())) tipus = BerletTipus.SelectedItem.ToString();
                else MessageBox.Show("Nem adott meg bérlet típust!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

                if (BerletErvenyesseg.Text.Length > 0 && DateTime.Parse(BerletErvenyesseg.Text) >= DateTime.Parse(Datum.Text)) ervenyes = BerletErvenyesseg.Text.Replace(". ", "").TrimEnd('.');
                else MessageBox.Show("Nem adta meg a bérlet érvényességét!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if(RadioJegy.IsChecked == true && JegyErvenyesseg.Value > 0)
            {
                tipus = "JGY";
                ervenyes = JegyErvenyesseg.Value.ToString();
            }
            else MessageBox.Show("Nem adott meg jegy darabszámot (0 nem lehet)", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

            if (megallo != string.Empty && datum != string.Empty && ido != string.Empty && azonosito != string.Empty && tipus != string.Empty  && ervenyes != string.Empty)
            {
                result = megallo + " " + datum + "-" + ido + " " + azonosito + " " + tipus + " " + ervenyes;
                using StreamWriter sw = new(
                    path: @"..\..\..\src\utasadat.txt",
                    append: true);
                sw.Write($"\n{result}");
                MessageBox.Show("A felszállás eltárolása sikeres volt!", "EUtazás 2020", MessageBoxButton.OK);
            }
        }
    }
}