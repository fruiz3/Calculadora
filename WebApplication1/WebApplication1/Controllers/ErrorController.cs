using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculadoraClient;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Calculadora.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public String  NotFoundRequest()
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();

            BadResponse badResponse = new BadResponse();
            badResponse.ErrorCode = "NotFound";
            badResponse.ErrorStatus = 400;
            badResponse.ErrorMessage = "Unable to Process Request: " + Request.Path.ToString();


            logger.Info("ERROR, petición no encontrada.");

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            var json3 = JsonConvert.SerializeObject(badResponse);
            Response.WriteAsync(json3);

            return "ErrorController";
        }
    }
}