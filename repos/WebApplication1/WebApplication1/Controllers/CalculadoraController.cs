using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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



        Operacion op = new Operacion();
       
        // URL por defecto
        public string Index(string nombre)
        {
            return HtmlEncoder.Default.Encode($"Bienvenido a la Calculadora {nombre}");
        }



        // URL https://localhost:44361/Calculadora/sumar
        public String Sumar(double[] numeros)
        {
            // Creo objeto Nlog y escribo la operación en el registro la operación.
            var logger = NLog.LogManager.GetCurrentClassLogger();

            string json = "";
          //  double[] nums = { 0 };
            Suma desSum = new Suma();

            Random random = new Random();
            id = random.Next(0, 9999);

            // Recibe el objeto en formato JSON y lo almaceno en un objeto de tipo suma.
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                try
                {
                    desSum = JsonConvert.DeserializeObject<Suma>(result);

                    double[] nums = new double[desSum.valores.ToArray().Length];

                    for (int i = 0; i < desSum.valores.ToArray().Length; i++)
                    {
                        nums[i] = Double.Parse(desSum.valores[i]);
                    }



                    desSum.resultado = WebApplication1.Models.Calculadora.suma(nums);

                    // Creo un objeto operacion y almaceno sus datos

                    op.Tipo = "Sum";

                    for (int i = 0; i < nums.Length; i++)
                    {
                        if (i == 0)
                        {

                            op.Calculo += nums[i];
                        }
                        else
                        {
                            op.Calculo += " + " + nums[i];
                        }
                    }

                    op.Calculo += " = " + desSum.resultado;

                    DateTime fecha = DateTime.Now;

                    op.Fecha = Convert.ToString(fecha);

                    // Convierto el objeto a JSON y lo añado la operación al listado general
                    JournalList.Listado.Add(id, JsonConvert.SerializeObject(op));

                    // Añado nuevas propiedades al objeto y lo devuelvo
                    Response.Clear();
                    Response.ContentType = "application/json; charset=utf-8";
                    var json2 = JsonConvert.SerializeObject(desSum);
                    Response.WriteAsync(json2);

                    logger.Info("Operación de suma");
                } catch
                {
                    Error500();

                }

            }


            
            

            return "ds";

        }


        public String Restar(double[] numeros)
        {
            // Creo objeto Nlog y escribo la operación en el registro la operación.
            var logger = NLog.LogManager.GetCurrentClassLogger();

            string json = "";
            double minuendo = 0;
            double sustraendo = 0;
            Resta desRest = new Resta();
            Random random = new Random();
            id = random.Next(0, 100);

            // Recibe el objeto en formato JSON y lo almaceno en un objeto de tipo Resta.
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                
                var result = streamReader.ReadToEnd();
                try
                {
                    desRest = JsonConvert.DeserializeObject<Resta>(result);
                    minuendo = Double.Parse(desRest.minuendo);
                    sustraendo = Double.Parse(desRest.sustraendo);

                    desRest.resultado = WebApplication1.Models.Calculadora.resta(minuendo, sustraendo);



                    // Creo un objeto operacion y almaceno sus datos
                    op.Tipo = "Rest";

                    op.Calculo += desRest.minuendo + " - " + desRest.sustraendo + " = " + desRest.resultado;




                    logger.Info("Operación de Resta");

                    DateTime fecha = DateTime.Now;

                    op.Fecha = Convert.ToString(fecha);

                    // Convierto el objeto a JSON y lo añado la operación al listado general

                    JournalList.Listado.Add(JournalList.id, JsonConvert.SerializeObject(op));


                    // Añado nuevas propiedades al objeto, lo serializo y lo envío

                    Response.Clear();
                    Response.ContentType = "application/json; charset=utf-8";
                    var json2 = JsonConvert.SerializeObject(desRest);
                    Response.WriteAsync(json2);

                } catch
                {
                    Error500();

                }
            }

           
            return "";

            
        }

        public String Multiplicar(double[] numeros)
        {
            // Creo objeto Nlog y escribo la operación en el registro la operación.
            var logger = NLog.LogManager.GetCurrentClassLogger();
            string json = "";
            Multiplicacion desMult = new Multiplicacion();

            Random random = new Random();
            id = random.Next(0, 100);

            // Recibe el objeto en formato JSON y lo almaceno en un objeto de tipo Multiplicacion.
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                try
                {
                    logger.Info("Operación de multiplicación");
                    desMult = JsonConvert.DeserializeObject<Multiplicacion>(result);

                    double[] nums = new double[desMult.valores.ToArray().Length];

                    for (int i = 0; i < desMult.valores.ToArray().Length; i++)
                    {
                        nums[i] = Double.Parse(desMult.valores[i]);
                    }

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

                }
                catch
                {
                    Error500();
                }
            }


          

            return "";
        }



        public String Dividir(double[] numeros)
        {
            // Creo objeto Nlog y escribo la operación en el registro la operación.
            var logger = NLog.LogManager.GetCurrentClassLogger();
            string json = "";
            double dividendo;
            double divisor;
            Division desDiv = new Division();
            Random random = new Random();
            id = random.Next(0, 100);

            // Recibe el objeto en formato JSON y lo almaceno en un objeto de tipo Division.
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                try
                {
                    desDiv = JsonConvert.DeserializeObject<Division>(result);

                    dividendo = Double.Parse(desDiv.dividendo);
                    divisor = Double.Parse(desDiv.divisor);


                    desDiv.cociente = WebApplication1.Models.Calculadora.division(dividendo, divisor);
                    desDiv.resto = WebApplication1.Models.Calculadora.resto(dividendo, divisor);



                    // Creo un objeto operacion y almaceno sus datos
                    op.Tipo = "Div";

                    op.Calculo += desDiv.dividendo + " / " + desDiv.divisor + " = Cociente: " + desDiv.cociente + ", Resto: " + desDiv.resto;

                    DateTime fecha = DateTime.Now;

                    op.Fecha = Convert.ToString(fecha);

                    logger.Info("Operación de división");

                    // Convierto el objeto a JSON y lo añado la operación al listado general
                    JournalList.Listado.Add(id, JsonConvert.SerializeObject(op));
                    id++;

                    // Añado nuevas propiedades al objeto, lo serializo y lo envío
                    Response.Clear();
                    Response.ContentType = "application/json; charset=utf-8";
                    var json2 = JsonConvert.SerializeObject(desDiv);
                    Response.WriteAsync(json2);


                }

                catch
                {
                    Error500();
                }

            }

           

            return "";
        }
        


        public String raiz(double numero)
        {
            // Creo objeto Nlog y escribo la operación en el registro la operación.
            var logger = NLog.LogManager.GetCurrentClassLogger();

            string json = "";
            double num = 0;
            Raiz desRaz = new Raiz();
            Random random = new Random();
            id = random.Next(0, 100);

            // Recibe el objeto en formato JSON y lo almaceno en un objeto de tipo Raiz.
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                try
                {
                    desRaz = JsonConvert.DeserializeObject<Raiz>(result);


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


                    logger.Info("Operación de raíz cuadrada");

                }
                catch
                {
                    Error500();
                }
            }

            
           
            return "";

        }

        public void Error500()
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();

            BadResponse badResponse = new BadResponse();
            badResponse.ErrorCode = "InternalError";
            badResponse.ErrorStatus = 500;
            badResponse.ErrorMessage = "Unable to process request";


            logger.Info("ERROR, no se pudo procesar la petición.");

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            var json3 = JsonConvert.SerializeObject(badResponse);
            Response.WriteAsync(json3);
        }
    }
}