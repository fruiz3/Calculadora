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


        // URL por defecto
        public string Index(string nombre)
        {
            return HtmlEncoder.Default.Encode($"Bienvenido a la Calculadora {nombre}");
        }

        // URL https://localhost:44361/Calculadora/sumar

        public String Sumar(double[] numeros)
        {
            string json = "";
            double[] nums = { 0 };
            Suma desSum = new Suma();


            // Recibe el objeto en formato JSON
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desSum = JsonConvert.DeserializeObject<Suma>(result);
            }


         
            nums = desSum.valores.ToArray();

            desSum.resultado = Calculadora.suma(nums);

            // Añado nuevas propiedades al objeto y lo devuelvo

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            var json2 = JsonConvert.SerializeObject(desSum);
            Response.WriteAsync(json2);


            return "";

        }


        public String Restar(double[] numeros)
        {
            string json = "";
            double minuendo = 0;
            double sustraendo = 0;
            Resta desRest = new Resta();


            // Recibe el objeto en formato JSON

            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desRest = JsonConvert.DeserializeObject<Resta>(result);
            }

            minuendo = desRest.minuendo;
            sustraendo = desRest.sustraendo;

            desRest.resultado = Calculadora.resta(minuendo, sustraendo);

            // Añado nuevas propiedades al objeto, lo serializo y lo envío

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            var json2 = JsonConvert.SerializeObject(desRest);
            Response.WriteAsync(json2);

            return "";

            
        }

        public String Multiplicar(double[] numeros)
        {
            string json = "";
            double[] nums = { 0 };
            Multiplicacion desMult = new Multiplicacion();


            // Recibo el objeto en formato JSON
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desMult = JsonConvert.DeserializeObject<Multiplicacion>(result);
            }


            nums = desMult.valores.ToArray();
            
           desMult.resultado = Calculadora.multiplicacion(nums);

            // Añado nuevas propiedades al objeto, lo serializo y lo envío

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            var json2 = JsonConvert.SerializeObject(desMult);
            Response.WriteAsync(json2);

            return "";
        }

        public String Dividir(double[] numeros)
        {
            string json = "";
            double dividendo;
            double divisor;
            Division desDiv = new Division();


            // Recibo el objeto en formato JSON
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desDiv = JsonConvert.DeserializeObject<Division>(result);
            }

            dividendo = Convert.ToDouble(desDiv.dividendo);
            divisor = Convert.ToDouble(desDiv.divisor);

            desDiv.cociente = Calculadora.division(dividendo, divisor);
            desDiv.resto = Calculadora.resto(dividendo, divisor);

            // Añado nuevas propiedades al objeto, lo serializo y lo envío

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            var json2 = JsonConvert.SerializeObject(desDiv);
            Response.WriteAsync(json2);

            return "";
        }
        


        public String raiz(double numero)
        {
            string json = "";
            double num = 0;
            Raiz desRaz = new Raiz();


            // Recibo el objeto en formato JSON
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desRaz = JsonConvert.DeserializeObject<Raiz>(result);
            }

            
            num = Convert.ToDouble(desRaz.num);

            desRaz.resultado = Calculadora.raiz(num);


            // Añado nuevas propiedades al objeto, lo serializo y lo envío

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            var json2 = JsonConvert.SerializeObject(desRaz);
            Response.WriteAsync(json2);

            return "";

        }


    }
}