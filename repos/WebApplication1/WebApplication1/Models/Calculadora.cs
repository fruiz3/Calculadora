using CalculadoraClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Calculadora
    {
        public static Object suma(double[] numeros)
        {
            double resultado = numeros[0];

            for(int i = 1; i < numeros.Length; i++)
            {
                resultado += numeros[i];
            }

            Suma sum = new Suma();
            sum.resultado = resultado;

            return sum;
        }

        public static object resta(double minuendo, double sustraendo)
        {
            double resultado = 0;

            resultado = minuendo - sustraendo;

            Resta rest = new Resta();
            rest.resultado = resultado;
            return rest;
        }


        public static double multiplicacion(double[] numeros)
        {
            double resultado = numeros[0];

            for (int i = 1; i < numeros.Length; i++)
            {
                resultado = resultado * numeros[i];
            }
            return resultado;
        }


        public static double division(double dividendo, double divisor)
        {

            double resultado;
            double resto;

            resultado = dividendo / divisor;
            resto = dividendo % divisor;
            


            return resultado;
        }

        
        public static double raiz (double num)
        {
            double resultado = 0;
            resultado = Math.Sqrt(num);

            return resultado;
        }
     
    }
}
