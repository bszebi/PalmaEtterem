using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalmaEtterem
{
    class Desszert
    {
        public string Nev { get; set; }
        public string Tipus { get; set; }
        public bool Dijazott { get; set; }
        public int Ar { get; set; }
        public string Egyseg { get; set; }

        public Desszert(string r)
        {
            var n = r.Split(';'); 
            Nev = n[0];
            Tipus = n[1];
            Dijazott = bool.Parse(n[2]);
            Ar = int.Parse(n[3]);
            Egyseg = n[4];
        }
    }
}
