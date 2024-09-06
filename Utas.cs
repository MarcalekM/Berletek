using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bérletek
{
    internal class Utas
    {
        string felszallasHelye { get; set; }
        string datum {  get; set; }
        string azonosito { get; set; }
        string tipus {  get; set; }
        string ervenyesseg { get; set; }

        public Utas(string sor)
        {
            var v = sor.Split(' ').ToList();
            felszallasHelye = v[0];
            datum = v[1];
            azonosito = v[2];
            tipus = v[3];
            ervenyesseg = v[4];
        }
    }
}
