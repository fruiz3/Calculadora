using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1;

namespace CalculadoraClient
{
    class Journal
    {
        public int id;

        

        public string getByID(int id)
        {

            string str = JournalList.Listado[id];
            // Devuelve un JSON
            var seri = JsonConvert.SerializeObject(str);
            return seri;
        }
    }
}
