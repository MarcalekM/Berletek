using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bérletek
{
    internal class Utas
    {
        public string felszallasHelye { get; set; }
        public string datum {  get; set; }
        public string azonosito { get; set; }
        public string tipus {  get; set; }
        public string ervenyesseg { get; set; }

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
