using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makao
{
    public class Karta
    {
        public int broj;
        public char znak;
        public Karta(int br, char zn)
        {
            broj = br;
            znak = zn;
        }
        public Karta()
        {
            broj = 0;
            znak = 'e';
        }
    }
}
