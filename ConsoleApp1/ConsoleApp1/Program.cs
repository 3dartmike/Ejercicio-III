using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPracticoIII
{

    class Program
    {

        //Lista que contendra los productos comprados
        List<Productos> lCompraUsuario;
        decimal dinero = 300;
        private static decimal unidades;

        static void Main(string[] args)
        {

            //List<Productos> lCompraUsuario = new List<Productos>();
            

            Program p = new Program();
            p.lCompraUsuario = new List<Productos>();
            // string listaCompra;
            Console.WriteLine("Lista de productos disponibles en el Mercado:");
            Console.WriteLine("Tienes disponible para gastar: " + p.dinero + " euros");
            Console.WriteLine();
            Productos productoTemp;
            Productos productoTemp2;
            for (int i = 0; i < Inventario.lInventario.Count; i++)
            {
                productoTemp = Inventario.lInventario[i];
                Console.WriteLine("- Hay " + productoTemp.cantidad + " " + Program.udOKG(productoTemp) + " de " + productoTemp.nombre + " a " + productoTemp.precio + " euros.");
            }
            Console.WriteLine();


            //---------------   Codigo de la compra, ----------------------------------------// 


            Productos bItem;
            for (int i = 0; i < Inventario.lInventario.Count; i++)
            {
                bItem = Inventario.lInventario[i];

                Console.WriteLine("Quieres comprar " + bItem.nombre + " ? Escribe 'Si o 'No'");
                string quiereComprar = Console.ReadLine().ToUpper();
                repetirInput2:
                while (!(String.Equals(quiereComprar, "SI") || String.Equals(quiereComprar, "S") || String.Equals(quiereComprar, "N") || String.Equals(quiereComprar, "NO")))
                {
                    Console.WriteLine("Valor incorrecto, entre Si o No o S o N");
                    quiereComprar = Console.ReadLine().ToUpper();
                }
                
                if (String.Equals(quiereComprar, "SI") || String.Equals(quiereComprar, "S"))
                {
                repetirInput3:
                    Console.WriteLine("Cuantos " + Program.udOKG(bItem) + " de " + bItem.nombre + " quieres comprar?");
                    //decimal unidades = decimal.Parse(Console.ReadLine());
                    decimal unidades = 0;
                    bool b = false;
                    while(!b)
                    {

                        if(decimal.TryParse(Console.ReadLine(), out unidades))
                        {
                            //TryParse Funciona
                       
                            decimal precio = bItem.precio;

                            decimal importe_producto = precio * unidades;

                            if (bItem.cantidad <= unidades) //( bItem.cantidad <= importe_producto)
                            {
                                Console.WriteLine("No hay productos suficientes");
                                goto repetirInput3;
                            }

                            if (importe_producto <= p.dinero)
                            {
                                p.dinero -= importe_producto;
                                bItem.cantidad = unidades;
                                p.lCompraUsuario.Add(bItem);
                                Console.WriteLine("Comprado " + unidades.ToString() + " " + Program.udOKG(bItem) + " de " + bItem.nombre + " a un costo de " + importe_producto.ToString());
                                Console.WriteLine("Le quedan " + p.dinero.ToString());
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Tienes fondos insuficientes para esta compra. Prueba de nuevo a insertar la cifra que quieres comprar. ");
                                Console.WriteLine("Le quedan " + p.dinero.ToString());
                                Console.WriteLine(unidades.ToString() + " " + Program.udOKG(bItem) + " de " + bItem.nombre + " cuestan: " + importe_producto.ToString());
                                goto repetirInput2;

                            }

                        }
                        else 
                        {
                            //TryParse no Funciona
                            Console.WriteLine("No has escrito correctamente");
                        }
                    }
                    Console.WriteLine(unidades);

                }
                


            }
            Console.WriteLine();

            decimal costoTotal = 0;

            for (int i = 0; i < p.lCompraUsuario.Count; i++)
            {
                bItem = p.lCompraUsuario[i];
                decimal costo = bItem.cantidad * bItem.precio;
                costoTotal += costo;
                Console.WriteLine("Has comprado " + bItem.cantidad.ToString() + " " + Program.udOKG(bItem) + " de " + bItem.nombre + " te ha costado " + string.Format("{0:N2}", costo) + " EUR");
                
            }

            

            Console.WriteLine("Gasto Total " + string.Format("{0:N2}", costoTotal) + " EUR");
            Console.WriteLine();
            Console.WriteLine("Quedan disponibles los siguientes productos");
            for (int i = 0; i < Inventario.lInventario.Count; i++)
            {
                productoTemp2 = Inventario.lInventario[i];
                Console.WriteLine("- Hay " + (productoTemp2.cantidad - unidades) + " " + Program.udOKG(productoTemp2) + " de " + productoTemp2.nombre + " a " + productoTemp2.precio + " euros.");
            }




            Console.ReadLine();


            //--------------- FIN   Codigo de la compra, ----------------------------------------// 

        }

        public static string udOKG(Productos p)
        {
            string s;
            if (p.aPeso)
            {
                s = "Kilogramos";
            }
            else
            {
                s = "Unidades";
            }
            return s;
        }

    }


    public static class Inventario
    {
        public static List<Productos> lInventario = new List<Productos>
        {
            new ProductoUnitario("Monster Verde", 2, 100),
            new ProductoPeso.Verduras.Lechuga("Lechuga", (decimal)0.80, 10),
            new ProductoPeso.Verduras.Cebollas("Cebollas", (decimal)0.25, 34),
            new ProductoPeso.Verduras.ColChina("ColChina", (decimal)0.70, 24),
            new ProductoPeso.Frutas.Guayaba("Guayaba del Peru", 5, 10),
            new ProductoPeso.Frutas.Mango("Mango", 4, 89),
            new ProductoPeso.Frutas.FrutaBomba("Fruta Bomba", 5, 53),
            new ProductoUnitario.PescadoCongelado.Calamar("Calamar", (decimal)4.50, 11),
            new ProductoUnitario.PescadoCongelado.Merluza("Merluza", (decimal)6.50, 23),
            new ProductoUnitario.PescadoCongelado.Langostino("Langostino", 16, 90)
        };
    }



    public abstract class Productos //clase abstracta base Productos
    {
        //public double PrecioProducto;
        // public abstract Categorias cat { get; }
        public string nombre;
        public decimal precio;
        public decimal cantidad; // Unidades o peso
        public abstract bool aPeso { get; }

        public virtual decimal PrecioProducto()
        {
            return precio * cantidad;
        }
        public virtual decimal PrecioProducto(decimal cantPers)
        {
            return precio * cantPers;
        }
        


    }

    public class ProductoUnitario : Productos
    {
        public override bool aPeso { get { return false; } }

        public ProductoUnitario()
        {

        }
        public ProductoUnitario(string n, decimal p, decimal c)
        {
            nombre = n;
            precio = p;
            cantidad = c;
        }

        public class PescadoCongelado : ProductoUnitario
        {
            public PescadoCongelado() { }
            public PescadoCongelado(string n, decimal p, decimal c)
            {
                nombre = n;
                precio = p;
                cantidad = c;
            }

            public class Calamar : PescadoCongelado
            {
                public Calamar(string n, decimal p, decimal c)
                {
                    nombre = n;
                    precio = p;
                    cantidad = c;
                }
            }

            public class Merluza : PescadoCongelado
            {
                public Merluza(string n, decimal p, decimal c)
                {
                    nombre = n;
                    precio = p;
                    cantidad = c;
                }
            }

            public class Langostino : PescadoCongelado
            {
                public Langostino(string n, decimal p, decimal c)
                {
                    nombre = n;
                    precio = p;
                    cantidad = c;
                }
            }


        }




    }

    public class ProductoPeso : Productos
    {
        public override bool aPeso { get { return true; } }

        public class Verduras : ProductoPeso
        {

            public Verduras() { }
            public Verduras(string n, decimal p, decimal c)
            {
                nombre = n;
                precio = p;
                cantidad = c;

            }

            public class Lechuga : Verduras
            {
                public Lechuga(string n, decimal p, decimal c)
                {
                    nombre = n;
                    precio = p;
                    cantidad = c;

                }
            }

            public class Cebollas : Verduras
            {
                public Cebollas(string n, decimal p, decimal c)
                {
                    nombre = n;
                    precio = p;
                    cantidad = c;

                }
            }

            public class ColChina : Verduras
            {
                public ColChina(string n, decimal p, decimal c)
                {
                    nombre = n;
                    precio = p;
                    cantidad = c;

                }
            }
        }

        public class Frutas : ProductoUnitario
        {
            public Frutas() { }
            public Frutas(string n, decimal p, decimal c)
            {
                nombre = n;
                precio = p;
                cantidad = c;

            }

            public class Guayaba : Frutas
            {

                public Guayaba(string n, decimal p, decimal c)
                {
                    nombre = n;
                    precio = p;
                    cantidad = c;

                }


            }

            public class Mango : Frutas
            {
                public Mango(string n, decimal p, decimal c)
                {
                    nombre = n;
                    precio = p;
                    cantidad = c;

                }
            }

            public class FrutaBomba : Frutas
            {
                public FrutaBomba(string n, decimal p, decimal c)
                {
                    nombre = n;
                    precio = p;
                    cantidad = c;

                }
            }


        }



    }







}