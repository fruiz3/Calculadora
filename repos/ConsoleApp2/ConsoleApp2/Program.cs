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

        // Creo objeto vacío para luego meter el json desserializado
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

        }




        static void Main(string[] args)
        {
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

                Console.WriteLine("0: Salir");
                Console.WriteLine("¿Qué desea hacer?\n");

                linea = Console.ReadLine();

                // Opcion 1, suma todos los números que el usuario elija, hasta que escriba "salir"
                if (linea.Equals("1")) {
                    
                    Console.WriteLine("Has Escogido la suma de números, \n escriba \"salir\" para no meter más numeros");
                    Console.WriteLine("Escribe los números que quieres sumar");
                    urlAddress = "https://localhost:44361/Calculadora/sumar";

                  

                    do
                    {
                        linea2 = Console.ReadLine();

                        if (double.TryParse(linea2, out valor))
                        {
                            suma.valores.Add(valor);
                        } else
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
                    urlAddress = "https://localhost:44361/Calculadora/multiplicar";



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
                    urlAddress = "https://localhost:44361/Calculadora/dividir";


                    do
                    {
                        linea2 = Console.ReadLine();

                        if (double.TryParse(linea2, out valor))
                        {
                            if(aux == 0)
                            {
                                div.dividendo = valor;
                            } else
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
                    urlAddress = "https://localhost:44361/Calculadora/raiz";
                    Console.WriteLine("Introduce el número para calcular su raiz");

                     linea2 = Console.ReadLine();

                    if (double.TryParse(linea2, out valor))
                    {
                        raiz.num = valor;
                    } else
                    {
                        Console.WriteLine("Has introducido un caracter que no es un número");
                    }

                    obj = raiz;
                }


                else if (linea.Equals("6"))
                {
                    Console.Write("Escriba el ID: ");
                    Journal peticion = new Journal();

                    urlAddress = "https://localhost:44361/Journal/query";
                    Console.WriteLine("Introduce el número para calcular su raiz");

                    linea2 = Console.ReadLine();

                    if (int.TryParse(linea2, out valor2))
                    {
                         peticion.id= valor2;
                    }
                    else
                    {
                        Console.WriteLine("Has introducido un caracter que no es un número");
                    }

                    obj = raiz;
                }

            } while(!linea.Equals("0"));


      

            // LLamada a la función para enviar el objeto como JSON
            sendJson(urlAddress, obj);

            Console.ReadKey();
        }
    }

    }



