using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final_ADA
{
    public class Punto
    {
        public int X {  get; set; }
        public int Y { get; set; }

        public Punto(int x, int y) 
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
