using CalculadoraClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Calculadora
    {

        // Creo objeto vacío para luego meter el json deserializado
        object obj1 = new object();


        /** Serializa un JSON y lo envía la dirección del parámetro
         * @params 
         * uri -> URL a la que se va a enviar el parámetro
         * obj -> Objeto que se va a enviar
        */
        static void sendJson(string uri, Object obj)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            object obj2 = new object();

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(obj);

                HttpWebRequest webrq = (HttpWebRequest)WebRequest.Create(uri);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();


            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                obj2 = JsonConvert.DeserializeObject(result);

                // Falta mostrar solo resultado en vez objeto entero
                Console.WriteLine("El resultado es: " + obj2);
            }


            //Obtener el código de estado de petición HTTP.
            Console.Write((int)httpResponse.StatusCode);

        }

        public static void mostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("Bienvenido a la Calculadora\n");


            Console.WriteLine("1: Sumar Numeros");
            Console.WriteLine("2: Restar Numeros");
            Console.WriteLine("3: Multiplicar Numeros");
            Console.WriteLine("4: Dividir Numeros");
            Console.WriteLine("5: Raiz Cuadrada de un número");
            Console.WriteLine("6: Consultar Journal Con ID");
            Console.WriteLine("7: Consultar registro");


            Console.WriteLine("0: Salir");
            Console.WriteLine("¿Qué desea hacer?\n");
        }

        public static void opcion()
        {
            string opt = Console.ReadLine();
            switch (opt)
            {
                case "1":
                    sumar();
                    break;

                case "2":
                    restar();
                    break;

                case "3":
                    multiplicar();
                    break;

                case "4":
                    dividir();
                    break;

                case "5":
                    raiz();
                    break;

                case "6":
                    journal();
                    break;

                case "7":
                    registro();
                    break;

                case "0":
                    Console.WriteLine("Saliendo de la aplicación...");
                    System.Environment.Exit(1);
                    break;

                default:
                    Console.WriteLine("Introduce una opción valida");
                    opcion();
                    break;
            }
        }

        public static void sumar()
        {
            Suma suma = new Suma();
            string urlAddress = "https://calculadora20190401125057.azurewebsites.net/Calculadora/Sumar";
            string linea2 = "";

            Console.WriteLine("Has Escogido la suma de números, \n escriba \"salir\" para no meter más numeros");
            Console.WriteLine("Escribe los números que quieres sumar");



            // El usuario introduce datos hasta que escriba "salir", si mete valores no numéricos, no se guardan.
            do
            {
                linea2 = Console.ReadLine();
                if (!linea2.Equals("salir"))
                {
                    suma.valores.Add(linea2);
                }
            } while (!linea2.Equals("salir"));

            sendJson(urlAddress, suma);
        }

        public static void restar()
        {
            string linea2 = "";
            int aux = 0;
            string urlAddress = "https://calculadora20190401125057.azurewebsites.net/Calculadora/Restar";
            Resta resta = new Resta();
            Console.WriteLine("Escribe los números que quieres restar");
            // El usuario introduce dos números para restar, si mete valores no numéricos no se guardan.
            do
            {
                linea2 = Console.ReadLine();

                if (aux == 0)
                {
                    resta.minuendo = linea2;
                }
                else
                {
                    resta.sustraendo = linea2;
                }

                aux++;
            } while (aux < 2);

            sendJson(urlAddress, resta);
        }

        public static void multiplicar()
        {
            string linea2 = "";
            Console.WriteLine("Escribe los números que quieres multiplicar");
            string urlAddress = "https://calculadora20190401125057.azurewebsites.net/Calculadora/Multiplicar";
            Multiplicacion mult = new Multiplicacion();


            // El usuario introduce datos hasta que escriba "salir", si mete valores no numéricos, no se guardan.
            do
            {
                linea2 = Console.ReadLine();
                if (!linea2.Equals("salir"))
                {
                    mult.valores.Add(linea2);
                }

            } while (!linea2.Equals("salir"));

            sendJson(urlAddress, mult);
        }

        public static void dividir()
        {
            string linea2 = "";
            Console.WriteLine("Escribe los números que quieres dividir");
            string urlAddress = "https://calculadora20190401125057.azurewebsites.net/Calculadora/Dividir";
            int aux = 0;
            Division div = new Division();

            // El usuario introduce dos números para dividirlos, si mete valores no numéricos, no se guardan.
            do
            {
                linea2 = Console.ReadLine();

                if (aux == 0)
                {
                    div.dividendo = linea2;
                }
                else
                {
                    div.divisor = linea2;
                }
                aux++;
            } while (aux < 2);

            sendJson(urlAddress, div);
        }

        public static void raiz()
        {
            string linea2 = "";
            Console.WriteLine("Has Escogido la raíz cuadrada");
            string urlAddress = "https://calculadora20190401125057.azurewebsites.net/Calculadora/Raiz";
            Console.WriteLine("Introduce el número para calcular su raiz");
            Raiz raiz = new Raiz();
            linea2 = Console.ReadLine();
            raiz.num = linea2;

            sendJson(urlAddress, raiz);
        }

        public static void journal()
        {
            string linea2 = "";
            Console.Write("Escriba el ID: ");
            Journal peticion = new Journal();
            int valor2 = 0;

            string urlAddress = "https://calculadora20190401125057.azurewebsites.net/Journal/query";
            Console.WriteLine("Introduce un ID para ver el Journal correspondiente");

            linea2 = Console.ReadLine();

            if (int.TryParse(linea2, out valor2))
            {
                peticion.id = valor2;
            }
            else
            {
                Console.WriteLine("Has introducido un caracter que no es un número");
            }

            sendJson(urlAddress, peticion);
        }

        public static void registro()
        {
            Console.WriteLine("Consultando fichero de Registro...");

            // Leer fichero linea a linea.
            string line;
            int nLinea = 0;
            string entrada;
            System.IO.StreamReader file =
                new System.IO.StreamReader("log.txt");
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine("Número de Linea: " + nLinea + " Contenido: " + line);
                Console.WriteLine("\n");
                nLinea++;
                Console.ReadKey();
            }
        }


        static void Main(string[] args)
        {
            string datoUsuario = "";

            do
            {
                mostrarMenu();
                opcion();

                Console.ReadKey();
                Console.Clear();


                Console.WriteLine("Escriba \"continuar\" para realizar más operaciones");
                datoUsuario = Console.ReadLine();

            } while (datoUsuario.ToLower().Equals("continuar"));


        }
    }
    }



