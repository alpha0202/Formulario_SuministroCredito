namespace Formulario_SuministroCredito.Service
{
    public interface IServiceFileUpload
    {
        public string GetFile(string rutaPDF_firmar);
        public bool AdjuntarArchivos(IFormFile FilePdf);

        public bool SubirArchivoDrive(string pathPdf);
    }
}
