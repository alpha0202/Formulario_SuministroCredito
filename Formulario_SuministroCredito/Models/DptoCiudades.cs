using Newtonsoft.Json;
using System.Collections;

namespace Formulario_SuministroCredito.Models
{
    public class DptoCiudades
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("departamento")]
        public string? Departamento { get; set; }

        [JsonProperty("ciudades")]
        public List<string>? Ciudades { get; set; }

        //public int id { get; set; }
        //public string departamento { get; set; }
        //public ArrayList? ciudades { get; set; }
    }
}

