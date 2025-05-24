using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final_ADA
{
    public class Listapuntos
    {
        private List<Punto> puntos=new List<Punto>();
        public void agregarPunto(Punto p) 
        {
            puntos.Add(p);
        }

        public List<Punto> obtenerPuntos()
        { 
            return puntos; 
        }

        public override string ToString()
        {
            string resultado = "Lista de puntos: \n";
            foreach (Punto p in puntos)
            { 
                resultado += p.ToString(); 
            }
            return resultado;
        }

        public void Limpiar()
        {
            puntos.Clear();
        }
    }
}
