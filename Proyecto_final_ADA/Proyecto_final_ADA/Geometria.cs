using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final_ADA
{
    public class Geometria
    {
        public static double Distancia(Punto p1, Punto p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }


        public static double ProductoEscalar(Punto a, Punto b, Punto c)
        {
            int ABx = b.X - a.X;
            int ABy = b.Y - a.Y;

            int BCx = c.X - b.X;
            int BCy = c.Y - b.Y;

            return ABx * BCx + ABy * BCy;
        }

        public static bool TrianguloRectangulo(Punto a, Punto b, Punto c)
        {
            var lados = new List<double>
            {
                Distancia(a,b), Distancia(b,c), Distancia(c,a)
            };
            lados.Sort();

            double a2 = Math.Pow(lados[0], 2);
            double b2 = Math.Pow(lados[1], 2);
            double c2 = Math.Pow(lados[2], 2);

            return Math.Abs(a2 + b2 - c2) < 1e-6;
        }

        public static bool TrianguloAcutangulo(Punto a, Punto b, Punto c)
        {
            var lados = new List<double>
            {
                Distancia(a,b), Distancia(b,c), Distancia(c,a)
            };
            lados.Sort();

            double a2 = Math.Pow(lados[0], 2);
            double b2 = Math.Pow(lados[1], 2);
            double c2 = Math.Pow(lados[2], 2);

            return (a2 + b2) > c2;
        }

        public static bool Rectangulo(List<Punto> puntos)
        {
            if (puntos.Count != 4)
                return false;

            for (int i = 0; i < 4; i++)
            {
                var a = puntos[i];
                var b = puntos[(i+1) % 4];
                var c = puntos[(i+2) % 4];

                if (Math.Abs(ProductoEscalar(a, b, c)) > 1e-6)
                    return false;
            }
            return true;
        }

        public static bool Cuadrado(List<Punto> puntos)
        {
            if (puntos.Count != 4)
                return false;
            var lados = new List<double>
            {
                Distancia(puntos[0], puntos[1]),
                Distancia(puntos[1], puntos[2]),
                Distancia(puntos[2], puntos[3]),
                Distancia(puntos[3], puntos[0])
            };

            var diagonales = new List<double>
            {
                Distancia(puntos[0], puntos[2]),
                Distancia(puntos[1], puntos[3])
            };

            bool ladosIguales = lados.All(l => Math.Abs(l - lados[0]) < 1e-6);
            bool diagonalesIguales = Math.Abs(diagonales[0] - diagonales[1]) < 1e-6;

            for(int i = 0; i<4; i++)
            {
                var a = puntos[i];
                var b = puntos[(i+1) % 4];
                var c = puntos[(i+2) % 4];

                if(Math.Abs(ProductoEscalar(a, b, c)) > 1e-6)
                    return false;
            }
            return ladosIguales && diagonalesIguales;
        }

        public static List<List<T>> Combinaciones<T>(List<T> lista, int k)
        {
            var resultado = new List<List<T>>();
            CombinacionesRecursivo(lista, k, 0, new List<T>(), resultado);
            return resultado;
        }

        private static void CombinacionesRecursivo<T>(List<T> lista, int k, int index, List<T> actual, List<List<T>> resultado)
        {
            if(actual.Count == k)
            {
                resultado.Add(new List<T>(actual));
                return;
            }

            for(int i = index; i < lista.Count; i++)
            {
                actual.Add(lista[i]);
                CombinacionesRecursivo(lista, k, i + 1, actual, resultado);
                actual.RemoveAt(actual.Count - 1);
            }
        }

        public static List<List<T>> Permutaciones<T>(List<T> lista)
        {
            var resultado = new List<List<T>>();
            PermutarRecursivo(lista, 0 , resultado);
            return resultado;
        }

        private static void PermutarRecursivo<T>(List<T> lista, int index, List<List<T>> resultado)
        {
            if (index == lista.Count)
            {
                resultado.Add(new List<T>(lista));
                return;
            }

            for (int i = index; i < lista.Count;i++)
            {
                (lista[index],lista[i]) = (lista[i], lista[index]);
                PermutarRecursivo(lista, index + 1 , resultado);
                (lista[index], lista[i]) = (lista[i], lista[index]);
            }
        }

        public static string Analizar(List<Punto> puntos)
        {
            int triangulosRectangulos = 0;
            int triangulosAcutangulos = 0;
            int cuadrados = 0;
            int rectangulos = 0;

            var triangulos = Combinaciones(puntos, 3);
            foreach (var trio in triangulos)
            {
                if (TrianguloRectangulo(trio[0], trio[1], trio[2]))
                    triangulosRectangulos++;
                else if (TrianguloAcutangulo(trio[0], trio[1], trio[2]))
                    triangulosAcutangulos++;
            }

            var grupos4 = Combinaciones(puntos, 4);
            foreach (var grupo in grupos4)
            {
                bool encontrado = false;
                foreach(var perm in Permutaciones(grupo))
                {
                    if(Cuadrado(perm))
                    {
                        cuadrados++;
                        encontrado = true;
                        break;
                    }
                }

                if (!encontrado)
                {
                    foreach(var perm in Permutaciones(grupo))
                    {
                        if(Rectangulo(perm))
                        {
                            rectangulos++;
                            break;
                        }
                    }
                }
            }

            return $"Triangulos rectangulos: {triangulosRectangulos}\n" +
                $"Triangulos acutangulos: {triangulosAcutangulos}\n" +
                $"Cuadrados: {cuadrados}\n" +
                $"Rectangulos: {rectangulos}";
        }
    }
}
