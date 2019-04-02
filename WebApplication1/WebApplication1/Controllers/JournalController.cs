using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CalculadoraClient;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Calculadora.Controllers
{
    public class JournalController : Controller
    {
        public string Index()
        {
            return "Clase Journal";
        }


        // Devuelve el fichero correspondiente al ID.
        public string query()
        {
            Journal peticion = new Journal();


            // Recibo el objeto en formato JSON

            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var result = streamReader.ReadToEnd();
                peticion = JsonConvert.DeserializeObject<Journal>(result);
            }

            // Usa el ID que le llega para buscar el fichero correspondiente del Listado.
            string fichero = peticion.getByID(peticion.id);

            return  fichero;
        }
    }
}