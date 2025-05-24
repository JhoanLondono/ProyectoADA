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

        public static List<double> OrdenarLados(double a, double b, double c)
        {
            List<double> ordenados = new List<double>();
            if(a<=b && a<=c)
            {
                ordenados.Add(a);
                if(b<=c)
                {
                    ordenados.Add(b);
                    ordenados.Add(c);
                }
                else
                {
                    ordenados.Add(c);
                    ordenados.Add(b);
                }
            }
            else if(b<=a && b<=c)
            {
                ordenados.Add(b);
                if(a<=c)
                {
                    ordenados.Add(a);
                    ordenados.Add(c);
                }
                else
                {
                    ordenados.Add(c);
                    ordenados.Add(a);
                }
            }
            else
            {
                ordenados.Add(c);
                if(a<=b)
                {
                    ordenados.Add(a);
                    ordenados.Add(b);
                }
                else
                {
                    ordenados.Add(b);
                    ordenados.Add(a);
                }
            }
            return ordenados;
        }

        public static bool TrianguloRectangulo(Punto a, Punto b, Punto c)
        {
            var lados = OrdenarLados(Distancia(a, b), Distancia(b, c), Distancia(c, a));

            double a2 = Math.Pow(lados[0], 2);
            double b2 = Math.Pow(lados[1], 2);
            double c2 = Math.Pow(lados[2], 2);

            return Math.Abs(a2 + b2 - c2) < 1e-6;
        }

        public static bool TrianguloAcutangulo(Punto a, Punto b, Punto c)
        {
            var lados = OrdenarLados(Distancia(a, b), Distancia(b, c), Distancia(c, a));

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

        public static double AreaTriangulo(Punto a, Punto b, Punto c)
        {
            return 0.5 * Math.Abs(a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y));
        }

        public static double AreaPoligono(List<Punto> puntos)
        {
            double area = 0;
            int n = puntos.Count;
            for(int i = 0; i < n; i++)
            {
                var p1 = puntos[i];
                var p2 = puntos[(i+1) % n];
                area += (p1.X * p2.Y - p2.X * p1.Y);
            }
            return Math.Abs(area)/2.0;
        }

        public static List<Figura> OrdenarPorArea(List<Figura> figuras)
        {
            List<Figura> ordenadas = new List<Figura>(figuras);

            for (int i = 0; i < ordenadas.Count - 1; i++)
            {
                for (int j = 0; j < ordenadas.Count - i - 1; j++)
                {
                    if (ordenadas[j].Area > ordenadas[j + 1].Area)
                    {
                        var temp = ordenadas[j];
                        ordenadas[j] = ordenadas[j + 1];
                        ordenadas[j + 1] = temp;
                    }
                }
            }
            return ordenadas;
        }

        public static List<Figura> ObtenerFiguras(List<Punto> puntos)
        {
            var figuras = new List<Figura>();
            var triangulos = Geometria.Combinaciones(puntos, 3);
            foreach(var trio in triangulos)
            {
                if (Geometria.TrianguloRectangulo(trio[0], trio[1], trio[2]))
                {
                    double area = AreaTriangulo(trio[0], trio[1], trio[2]);
                    figuras.Add(new Figura("Triangulo rectangulo", area, new List<Punto>(trio)));
                }
                else if (Geometria.TrianguloAcutangulo(trio[0], trio[1], trio[2]))
                {
                    double area = AreaTriangulo(trio[0], trio[1], trio[2]);
                    figuras.Add(new Figura("Triangulo acutangulo", area, new List<Punto>(trio)));
                }
            }
            var grupos4 = Geometria.Combinaciones(puntos, 4);
            foreach(var grupo in grupos4)
            {
                foreach(var perm in Geometria.Permutaciones(grupo))
                {
                    if (Geometria.Cuadrado(perm))
                    {
                        double area = AreaPoligono(perm);
                        figuras.Add(new Figura("Cuadrado", area, new List<Punto>(perm)));
                        break;
                    }
                }
                foreach(var perm in Geometria.Permutaciones(grupo))
                {
                    if (Geometria.Rectangulo(perm))
                    {
                        double area = AreaPoligono(perm);
                        figuras.Add(new Figura("Rectangulo", area, new List<Punto>(perm)));
                        break;
                    }
                }
            }
            figuras = OrdenarPorArea(figuras);
            return figuras;
        }
    }
}