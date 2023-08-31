namespace Formulario_SuministroCredito.Service
{
    public interface IServiceFileUpload
    {
      //sin uso
        public bool AdjuntarArchivos(IFormFile FilePdf);

        public string SubirArchivoDrive();

        public string CrearDirectorio_Firmante();

        //public Tuple<string, string> Resultado();
    }
}
