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




        static void Main(string[] args)
        {
            bool enviar = true;
            String linea = "";
            String linea2 = "";
            Suma suma = new Suma();
            Resta resta = new Resta();
            Multiplicacion mult = new Multiplicacion();
            Division div = new Division();
            Raiz raiz = new Raiz();
            int aux = 0;
            int valor2 = 0;


            object obj = new object();
        
            List<double> numeros = new List<double>();

            string urlAddress = "";
            double valor;

            // Menú de opciones para que el usuario elija la acción
            do
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

                linea = Console.ReadLine();

                // Opcion 1, suma todos los números que el usuario elija, hasta que escriba "salir"
                if (linea.Equals("1"))
                {

                    Console.WriteLine("Has Escogido la suma de números, \n escriba \"salir\" para no meter más numeros");
                    Console.WriteLine("Escribe los números que quieres sumar");
                    urlAddress = "https://localhost:44361/Calculadora/sumar";


                    // El usuario introduce datos hasta que escriba "salir", si mete valores no numéricos, no se guardan.
                    do
                    {
                        linea2 = Console.ReadLine();

                        if (double.TryParse(linea2, out valor))
                        {
                            suma.valores.Add(valor);
                        }
                        else
                        {
                            Console.WriteLine("Introduce valores numéricos");
                        }

                    } while (!linea2.Equals("salir"));

                    Console.WriteLine("objeto" + suma);
                    obj = suma;

                }

                // Opcion 2, resta dos números introducidos por el usuario
                else if (linea.Equals("2"))
                {
                    Console.WriteLine("Escribe los números que quieres restar");
                    urlAddress = "https://localhost:44361/Calculadora/restar";


                    // El usuario introduce dos números para restar, si mete valores no numéricos no se guardan.
                    do
                    {
                        linea2 = Console.ReadLine();

                        if (double.TryParse(linea2, out valor))
                        {
                            if (aux == 0)
                            {
                                resta.minuendo = valor;
                            }
                            else
                            {
                                resta.sustraendo = valor;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Introduce valores numéricos");
                        }

                        aux++;
                    } while (aux < 2);

                    obj = resta;

                }

                // Opcion 3, multiplica todos los números que el usuario introduzca, hasta que escriba "salir"
                else if (linea.Equals("3"))
                {
                    Console.WriteLine("Escribe los números que quieres multiplicar");
                    urlAddress = "https://calculadora20190401125057.azurewebsites.net/Calculadora/multiplicar";


                    // El usuario introduce datos hasta que escriba "salir", si mete valores no numéricos, no se guardan.
                    do
                    {
                        linea2 = Console.ReadLine();

                        if (double.TryParse(linea2, out valor))
                        {
                            mult.valores.Add(valor);
                        }
                        else
                        {
                            Console.WriteLine("Introduce valores numéricos");
                        }

                    } while (!linea2.Equals("salir"));



                    obj = mult;


                }

                // Opcion 4, divide dos números introducidos por el usuario
                else if (linea.Equals("4"))
                {
                    Console.WriteLine("Escribe los números que quieres dividir");
                    urlAddress = "https://calculadora20190401125057.azurewebsites.net/Calculadora/dividir";

                    // El usuario introduce dos números para dividirlos, si mete valores no numéricos, no se guardan.
                    do
                    {
                        linea2 = Console.ReadLine();

                        if (double.TryParse(linea2, out valor))
                        {
                            if (aux == 0)
                            {
                                div.dividendo = valor;
                            }
                            else
                            {
                                div.divisor = valor;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Introduce valores numéricos");
                        }

                        aux++;
                    } while (aux < 2);


                    obj = div;
                }

                // Opcion 5, calcula la raíz del número introducido por el usuario
                else if (linea.Equals("5"))
                {
                    Console.WriteLine("Has Escogido la raíz cuadrada");
                    urlAddress = "https://calculadora20190401125057.azurewebsites.net/Calculadora/raiz";
                    Console.WriteLine("Introduce el número para calcular su raiz");

                    linea2 = Console.ReadLine();

                    // El usuario introduce un número para calcular su raíz, si mete valores no numéricos, no se guardan.
                    if (double.TryParse(linea2, out valor))
                    {
                        raiz.num = valor;
                    }
                    else
                    {
                        Console.WriteLine("Has introducido un caracter que no es un número");
                    }

                    obj = raiz;
                }


                // Consultar un journal por su ID.
                else if (linea.Equals("6"))
                {
                    Console.Write("Escriba el ID: ");
                    Journal peticion = new Journal();

                    urlAddress = "https://calculadora20190401125057.azurewebsites.net/Journal/query";
                    Console.WriteLine("Introduce el número para calcular su raiz");

                    linea2 = Console.ReadLine();

                    if (int.TryParse(linea2, out valor2))
                    {
                        peticion.id = valor2;
                    }
                    else
                    {
                        Console.WriteLine("Has introducido un caracter que no es un número");
                    }

                    obj = raiz;
                }

                // Cosultar registro.
                else if (linea.Equals("7"))
                {
                    enviar = false;
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
                } while(!linea.Equals("0"));


      

            // LLamada a la función para enviar el objeto como JSON
            if(enviar == true)
            {
                sendJson(urlAddress, obj);
            }
            

            Console.ReadKey();
        }
    }

    

    }



