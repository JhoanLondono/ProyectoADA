using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final_ADA
{
    public class Figura
    {
        public string Tipo { get; set; }
        public double Area { get; set; }
        public List<Punto> Puntos { get; set; }

        public Figura(string tipo, double area, List<Punto> puntos)
        {
            Tipo = tipo;
            Area = area;
            Puntos = puntos;
        }
    }
}
