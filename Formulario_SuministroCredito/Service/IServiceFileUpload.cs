namespace Formulario_SuministroCredito.Service
{
    public interface IServiceFileUpload
    {
      
        public bool AdjuntarArchivos(IFormFile FilePdf);

        public string SubirArchivoDrive(string pathPdf);
    }
}
