using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
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
        Random random = new Random();
        int id = 0;
        string accion = "";
        // TODO añadir funciónes en calculadora para dejar más limpio el código, 

        Operacion op = new Operacion();

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

            Random random = new Random();
            id = random.Next(0, 9999);

            // Recibe el objeto en formato JSON
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desSum = JsonConvert.DeserializeObject<Suma>(result);
            }


            
            nums = desSum.valores.ToArray();

            desSum.resultado = WebApplication1.Models.Calculadora.suma(nums);

            // Creo un objeto operacion y almaceno sus datos

            op.Tipo = "Sum";

            for(int i = 0; i < nums.Length; i++)
            {
                if(i == 0)
                {
                    
                    op.Calculo += nums[i];
                } else
                {
                    op.Calculo += " + " + nums[i];
                } 
            }

            op.Calculo +=  " = " + desSum.resultado;

            DateTime fecha = DateTime.Now;

            op.Fecha = Convert.ToString(fecha);

            // Convierto el objeto a JSON y lo añado la operación al listado general

            JournalList.Listado.Add(id, JsonConvert.SerializeObject(op));

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
            Random random = new Random();
            id = random.Next(0, 100);

            // Recibe el objeto en formato JSON

            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desRest = JsonConvert.DeserializeObject<Resta>(result);
            }

            minuendo = desRest.minuendo;
            sustraendo = desRest.sustraendo;

            desRest.resultado = WebApplication1.Models.Calculadora.resta(minuendo, sustraendo);



            // Creo un objeto operacion y almaceno sus datos
            op.Tipo = "Rest";

            op.Calculo +=  desRest.minuendo + " - " + desRest.sustraendo + " = " + desRest.resultado;
                
        

        

            DateTime fecha = DateTime.Now;

            op.Fecha = Convert.ToString(fecha);

            // Convierto el objeto a JSON y lo añado la operación al listado general

            JournalList.Listado.Add(JournalList.id, JsonConvert.SerializeObject(op));


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

            Random random = new Random();
            id = random.Next(0, 100);

            // Recibo el objeto en formato JSON
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desMult = JsonConvert.DeserializeObject<Multiplicacion>(result);
            }


            nums = desMult.valores.ToArray();
            
           desMult.resultado = WebApplication1.Models.Calculadora.multiplicacion(nums);



            // Creo un objeto operacion y almaceno sus datos
            op.Tipo = "Mul";


            for (int i = 0; i < nums.Length; i++)
            {
                if (i == 0)
                {
                    op.Calculo += " * " + nums[i];
                }
                else
                {
                    op.Calculo += nums[i];
                }
            }

            op.Calculo += " = " + desMult.resultado;



            DateTime fecha = DateTime.Now;

            op.Fecha = Convert.ToString(fecha);

            // Convierto el objeto a JSON y lo añado la operación al listado general

            JournalList.Listado.Add(id, JsonConvert.SerializeObject(op));


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
            Random random = new Random();
            id = random.Next(0, 100);

            // Recibo el objeto en formato JSON
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desDiv = JsonConvert.DeserializeObject<Division>(result);
            }

            dividendo = Convert.ToDouble(desDiv.dividendo);
            divisor = Convert.ToDouble(desDiv.divisor);

            desDiv.cociente = WebApplication1.Models.Calculadora.division(dividendo, divisor);
            desDiv.resto = WebApplication1.Models.Calculadora.resto(dividendo, divisor);



            // Creo un objeto operacion y almaceno sus datos
            op.Tipo = "Div";

            op.Calculo += desDiv.dividendo + " / " + desDiv.divisor + " = Cociente: " + desDiv.cociente + ", Resto: " + desDiv.resto;



            DateTime fecha = DateTime.Now;

            op.Fecha = Convert.ToString(fecha);

            // Convierto el objeto a JSON y lo añado la operación al listado general

            JournalList.Listado.Add(id, JsonConvert.SerializeObject(op));
            id++;

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
            Random random = new Random();
            id = random.Next(0, 100);

            // Recibo el objeto en formato JSON
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                desRaz = JsonConvert.DeserializeObject<Raiz>(result);
            }

            
            num = Convert.ToDouble(desRaz.num);

            desRaz.resultado = WebApplication1.Models.Calculadora.raiz(num);



            // Creo un objeto operacion y almaceno sus datos
            op.Tipo = "Raiz";

            op.Calculo += "√" + desRaz.num + " = " + desRaz.resultado;

            DateTime fecha = DateTime.Now;

            op.Fecha = Convert.ToString(fecha);

            // Convierto el objeto a JSON y lo añado la operación al listado general

            JournalList.Listado.Add(id, JsonConvert.SerializeObject(op));

            // Añado nuevas propiedades al objeto, lo serializo y lo envío

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            var json2 = JsonConvert.SerializeObject(desRaz);
            Response.WriteAsync(json2);

            return "";

        }
    }
}