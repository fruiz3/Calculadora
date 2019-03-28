using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Calculadora
    {

        // Suma los números pasados como parámetro
        public static double suma(double[] numeros)
        {
            double resultado = numeros[0];

            for(int i = 1; i < numeros.Length; i++)
            {
                resultado += numeros[i];
            }
            return resultado;
        }

        // Resta dos números pasado como parámetro
        public static double resta(double minuendo, double sustraendo)
        {
            double resultado = 0;
            resultado = minuendo - sustraendo;      
            return resultado;
        }

        // Multiplica todos los números pasados como parámetro
        public static double multiplicacion(double[] numeros)
        {
            double resultado = numeros[0];

            for (int i = 1; i < numeros.Length; i++)
            {
                resultado = resultado * numeros[i];
            }
            return resultado;
        }

        // Divide dos números pasados como parámetro
        public static double division(double dividendo, double divisor)
        {

            double resultado;

            resultado = dividendo / divisor;
            
            return resultado;
        }

        // Calcula el resto de la división de los dos números pasados como parámetro
        public static double resto (double dividendo, double divisor)
        {

            double resto;

            resto = dividendo % divisor;

            return resto;
        }


        public static double raiz (double num)
        {
            double resultado = 0;
            resultado = Math.Sqrt(num);

            return resultado;
        }
     
    }
}
