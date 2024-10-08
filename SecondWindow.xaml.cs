﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

namespace Bérletek
{
    /// <summary>
    /// Interaction logic for SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        List<Utas> utasok = new();
        public SecondWindow()
        {
            InitializeComponent();
            using StreamReader sr = new(
                path: @"..\..\..\src\utasadat.txt",
                encoding: Encoding.UTF8);
            while (!sr.EndOfStream) utasok.Add(new(sr.ReadLine()));
            int f3 = utasok.Count();
            UtasokSzama.Text = $"A mai napon {f3} utas szállt fel a buszra";

            List<string> elutasitott = new();
            foreach (var utas in utasok)
            {
                if (utas.ervenyesseg == "0") elutasitott.Add(utas.azonosito);
                else if (utas.ervenyesseg.Length == 8)
                {
                    string datum = utas.datum.Split('-').First();
                    string ervenyes = utas.ervenyesseg;
                    //if (int.Parse(datum.Substring(4, 2)) == int.Parse(ervenyes.Substring(4, 2)) && (int.Parse(datum.Substring(6, 2)) > int.Parse(ervenyes.Substring(6, 2)))) elutasitott.Add(utas.azonosito);
                    if (DateTime.ParseExact(datum, "yyyyMMdd", CultureInfo.InvariantCulture) > DateTime.ParseExact(ervenyes, "yyyyMMdd", CultureInfo.InvariantCulture)) elutasitott.Add(utas.azonosito);
                }
            }
            int f4 = elutasitott.Count;
            ElutasitottUtasok.Text = $"A mai napon {f4} utast kellett elutasítani";

            int f5 = 0;
            int max = utasok.Where(u => u.felszallasHelye.Equals("0")).Count();
            for (int i = 1; i < 30; i++)
            {
                if (max < utasok.Where(u => u.felszallasHelye.Equals(Convert.ToString(i))).Count())
                {
                    f5 = i;
                    max = utasok.Where(u => u.felszallasHelye.Equals(Convert.ToString(i))).Count();
                }
            }
            LegtobbFelszallo.Text = $"A legtöbben a {f5} megállóban szálltak fel {max} fő";

            int kedvezmeny = 0;
            int ingyenes = 0;
            foreach (var utas in utasok)
            {
                if (utas.tipus.Equals("NYP") || utas.tipus.Equals("GYK") || utas.tipus.Equals("RVS") && !elutasitott.Contains(utas.azonosito)) ingyenes++;
                else if (utas.tipus.Equals("TAB") || utas.tipus.Equals("NYB") && !elutasitott.Contains(utas.azonosito)) kedvezmeny++;
            }
            Kedvezmenyes.Text = $"Ingyenesen utazók száma: {ingyenes}\nKedvezménnyle utazok száma: {kedvezmeny}";

            List<string> figyelmeztetett = new();
            foreach (var utas in utasok)
            {
                if (utas.ervenyesseg == "0") f4++;
                else if (utas.ervenyesseg.Length == 8)
                {
                    string datum = utas.datum.Split('-').First();
                    string ervenyes = utas.ervenyesseg;
                    int hatralevo = int.Parse(ervenyes.Substring(6, 2)) - int.Parse(datum.Substring(6, 2));
                    if (int.Parse(datum.Substring(4, 2)) == int.Parse(ervenyes.Substring(4, 2)) && hatralevo <= 3)
                    {
                        string helyes = ervenyes.Substring(0, 4) + '-' + ervenyes.Substring(4, 2) + '-' + ervenyes.Substring(6, 2);
                        figyelmeztetett.Add(utas.azonosito);
                        figyelmeztetett.Add(helyes);
                    };
                }
            }

            using StreamWriter sw = new(
                path: @"..\..\..\src\figyelmeztetes.txt",
                append: false);
            for (int i = 0; i < figyelmeztetett.Count; i += 2)
            {
                sw.WriteLine($"{figyelmeztetett[i]} {figyelmeztetett[i + 1]}");
            }
        }
    }
}
