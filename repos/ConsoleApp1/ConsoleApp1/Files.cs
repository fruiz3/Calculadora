using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Files
    {
        static void Main(string[] args)
        {
            String fichero = "C:\\Users\\Francisco\\Google Drive\\test.txt";
            String linea = "";
            // Ejemplo de Escritura de fichero, cada vez que se ejecuta sobreescribe el fichero

            /*
        
            StreamWriter writer;
            writer = new StreamWriter("fichero");

            Console.WriteLine("Escritura de fichero\n");

            do
            {
                Console.WriteLine("Linea a escribir. \n Escriba \"Salir\" para detener el programa");
                linea = Console.ReadLine();

                if (!linea.ToLower().Equals("salir"))
                {
                    writer.WriteLine(linea);

                }


            } while (!linea.Equals("salir"));

            writer.Close();
        } */


            // Ejemplo de escribir en fichero sin sobreescribir
            /*
            StreamWriter writer;
            writer = File.AppendText(fichero);

            Console.WriteLine("Escritura sin sobreescribir\n");

            do
            {
                Console.WriteLine("Linea a escribir. \n Escriba \"Salir\" para detener el programa");
                linea = Console.ReadLine();

                if (!linea.ToLower().Equals("salir"))
                {
                    writer.WriteLine(linea);
                }

            } while (!linea.Equals("salir"));
            writer.Close();
        } */

            // Ejemplo de Lectura de fichero
            /*    StreamReader reader;
                reader = File.OpenText(fichero);

                do
                {
                    linea = reader.ReadLine();
                    Console.WriteLine(linea);
                } while (linea != null);


                Console.ReadKey();
            }
            */
        }
    }
}