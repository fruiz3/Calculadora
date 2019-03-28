using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CalculadoraClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class CalculadoraController : Controller
    {
        double[] nums;

        public string Index(string nombre)
        {
            return HtmlEncoder.Default.Encode($"Bienvenido a la Calculadora {nombre}");
        }

        public String Sumar(double[] numeros)
        {
            string json = "";
            double[] nums = { 0 };
            Suma desSum = new Suma();



            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desSum = JsonConvert.DeserializeObject<Suma>(result);
            }
            
           
                nums = desSum.valores.ToArray();
            

            if (desSum.valores.Count < 2)
            {
                return "Debes Introducir mínimo dos  números" ;
            } else
            {
                return "El resultado es " + Calculadora.suma(nums);
            }
        }


        public String Restar(double[] numeros)
        {
            string json = "";
            double minuendo = 0;
            double sustraendo = 0;
            Resta desRest = new Resta();



            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desRest = JsonConvert.DeserializeObject<Resta>(result);
            }

            minuendo = desRest.minuendo;
            sustraendo = desRest.sustraendo;

                return "El resultado es " + Calculadora.resta(minuendo, sustraendo);
            
        }

        public String Multiplicar(double[] numeros)
        {
            string json = "";
            double[] nums = { 0 };
            Multiplicacion desMult = new Multiplicacion();



            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desMult = JsonConvert.DeserializeObject<Multiplicacion>(result);
            }


            nums = desMult.valores.ToArray();


            if (desMult.valores.Count < 2)
            {
                return "Debes Introducir mínimo dos  números";
            }
            else
            {
                return "El resultado es " + Calculadora.multiplicacion(nums);
            }
        }

        public String Dividir(double[] numeros)
        {
            string json = "";
            double dividendo;
            double divisor;
            Division desDiv = new Division();



            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desDiv = JsonConvert.DeserializeObject<Division>(result);
            }

            dividendo = Convert.ToDouble(desDiv.dividendo);
            divisor = Convert.ToDouble(desDiv.divisor);

                return "El resultado es " + Calculadora.division(dividendo, divisor);
            }
        


        public String raiz(double numero)
        {
            string json = "";
            double num = 0;
            Raiz desRaz = new Raiz();



            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desRaz = JsonConvert.DeserializeObject<Raiz>(result);
            }

            
            num = Convert.ToDouble(desRaz.num);

            return "El resultado es " + Calculadora.raiz(num);
            
        }


    }
}