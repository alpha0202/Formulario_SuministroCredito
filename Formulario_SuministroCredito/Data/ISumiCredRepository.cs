using Formulario_SuministroCredito.Models;
using Microsoft.AspNetCore.Mvc;

namespace Formulario_SuministroCredito.Data
{
    public interface ISumiCredRepository
    {

        Task<IEnumerable<SuministroCredito>> GetAll();
        Task<SuministroCredito> GetDatail(int id);
        Task<bool> Insert(SuministroCredito suministroCredito);

        public SuministroCredito GetById(int id);

        public string CountRowDb();

        public string PdfNew(SuministroCredito suministroCredito);

        //Task<bool> UploadFile(IFormFile file);

        //Task<List<DptoCiudades>> Dpto_Ciudades();
        //Task<IActionResult> Dpto_Ciudades();
    }
}
