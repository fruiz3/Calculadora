using CalculadoraClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Calculadora
    {


        /** Calcula la suma de todos los números pasado como parámetro.
             Parámetros:
                -numeros -> Array con números para ser sumados.
             @return resultado -> Suma de todos los números
        */
        public static double suma(double[] numeros)
        {
            double resultado = numeros[0];

            for(int i = 1; i < numeros.Length; i++)
            {
                resultado += numeros[i];
            }
            return resultado;
        }

        /** Calcula la resta de los dos números pasados como parámetro.
            Parámetros: 
                -minuendo -> Número al que se le va a restar.
                -sustraendo -> Número que se va a restar al minuendo
            @return resultado -> Resultado de la resta entre minuendo y sustraendo.
        */
        public static double resta(double minuendo, double sustraendo)
        {
            double resultado = 0;
            resultado = minuendo - sustraendo;      
            return resultado;
        }

        /** Calcula la multiplicación de todos los números pasado como parámetro.
            Parámetros:
                -numeros -> Array con números para ser multiplicados.
            @return resultado -> Multiplicación de todos los números.
        */
        public static double multiplicacion(double[] numeros)
        {
            double resultado = numeros[0];

            for (int i = 1; i < numeros.Length; i++)
            {
                resultado = resultado * numeros[i];
            }
            return resultado;
        }

        /** Calcula la división de los dos números pasados como parámetro.
              Parámetros: 
                -dividendo -> Número al que se va a dividir.
                -divisor -> Número por el que se va a dividir.
              @return resultado -> Resultado de divisíón entre dividendo y divisor.
        */
        public static double division(double dividendo, double divisor)
        {

            double resultado;

            resultado = dividendo / divisor;
            
            return resultado;
        }

        /** Calcula el resto de la división entre los dos números pasados como parámetro.
              Parámetros: 
                  -dividendo -> Número al que se va a dividir.
                  -divisor -> Número por el que se va a dividir.
              @return resultado -> Resto de la  divisíón entre dividendo y divisor.
          */
        public static double resto (double dividendo, double divisor)
        {

            double resto;

            resto = dividendo % divisor;

            return resto;
        }

        /** Calcula la raíz de los dos números pasados como parámetro.
              Parámetros:
                -num -> Número al que se va a realizar la raíz cuadrada.
              @return  resultado -> Resultado de la raíz cuadrada del número pasado como parámetro.
        */
        public static double raiz (double num)
        {
            double resultado = 0;
            resultado = Math.Sqrt(num);

            return resultado;
        }
     
      
    }
}
