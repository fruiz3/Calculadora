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


        /*     private static void PostJson(string uri, List<double> arr)
              {
                  Console.WriteLine("PostJson");
                  string postData = JsonConvert.SerializeObject(arr);
                  byte[] bytes = Encoding.UTF8.GetBytes(postData);
                  var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                  httpWebRequest.Method = "POST";
                  httpWebRequest.ContentLength = bytes.Length;
                  httpWebRequest.ContentType = "text/xml";
                  using (Stream requestStream = httpWebRequest.GetRequestStream())
                  {
                      requestStream.Write(bytes, 0, bytes.Count());
                  }
                  var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                  if (httpWebResponse.StatusCode != HttpStatusCode.OK)
                  {
                      string message = String.Format("POST failed. Received HTTP {0}", httpWebResponse.StatusCode);
                      throw new ApplicationException(message);
                  }
              }*/


        static void sendJson(string uri, Object obj)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

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
                Console.WriteLine(result);
            }

        }



        static void leerJson()
        {

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

            object obj = new object();

            List<double> numeros = new List<double>();
            string urlAddress = "";
            double valor;
            do
            {
                Console.Clear();
                Console.WriteLine("Bienvenido a la Calculadora\n");


                Console.WriteLine("1: Sumar Numeros");
                Console.WriteLine("2: Restar Numeros");
                Console.WriteLine("3: Multiplicar Numeros");
                Console.WriteLine("4: Dividir Numeros");
                Console.WriteLine("5: Raiz Cuadrada de un número");
                Console.WriteLine("0: Salir");
                Console.WriteLine("¿Qué desea hacer?\n");

                linea = Console.ReadLine();

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


                    obj = suma;

                }

                else if (linea.Equals("2"))
                {
                    Console.WriteLine("Escribe los números que quieres restar");
                    urlAddress = "https://localhost:44361/Calculadora/restar";



                    do
                    {
                        linea2 = Console.ReadLine();

                        if (double.TryParse(linea2, out valor))
                        {
                            resta.valores.Add(valor);
                        }
                        else
                        {
                            Console.WriteLine("Introduce valores numéricos");
                        }
                       
                    } while (!linea2.Equals("salir"));

                    obj = resta;

                }
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

                

            } while(!linea.Equals("0"));


            //var json = JsonConvert.SerializeObject(numeros);

            // PostJson(urlAddress, numeros);

            sendJson(urlAddress, obj);

            Console.ReadKey();
        }
    }

    }



