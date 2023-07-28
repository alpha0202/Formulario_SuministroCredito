using Formulario_SuministroCredito.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using System.Text;

namespace Formulario_SuministroCredito.Service
{
    public class ServiceFileUpload : IServiceFileUpload
    {


        private readonly IWebHostEnvironment _webHostEnvironment;
        public IFormFile? FilePdf { get; set; } 

        public ServiceFileUpload(IWebHostEnvironment webHostEnvironment) 
        {
            _webHostEnvironment = webHostEnvironment;
           
            
        }


        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "Drive API .NET Quickstart";
        private const string MimeTypeFolder = "application/vnd.google-apps.folder";
        private const string carpetaPrincipal = "1pnWeHpy0Mm8LySCI5npA5PlE_pkdGzHx";
        private string url0 = "";
        private string id_docDrive ="";

        public string Url0 { get { return url0; } }



       
        public DriveService ServicioDrive()
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string path = Path.Combine(contentRootPath, "Uploads", "credentials.json");
            string path2 = Path.Combine(contentRootPath, "Uploads", "token.json");

            UserCredential credential;

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = path2;
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    //GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new Google.Apis.Util.Store.FileDataStore(credPath, true)).Result;

            }


            // Create Drive API service.

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
            
            
        }

        public bool AdjuntarArchivos(IFormFile FilePdf)
        {

            try
            {

                string contentRootPath = _webHostEnvironment.ContentRootPath;
                string path = Path.Combine(contentRootPath, "Uploads", "credentials.json");
                string path2 = Path.Combine(contentRootPath, "Uploads", "token.json");

                UserCredential credential;

                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    string credPath = path2;
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        //GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new Google.Apis.Util.Store.FileDataStore(credPath, true)).Result;

                }


                // Create Drive API service.

                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });




                if (FilePdf != null)
                {

                    //string url0 = "";
                    string archivo0 = ("FIRMANTE-" + DateTime.Now.ToString("yyyyMMddHHmmss")).ToLower();
                    int BufferSize = 1130702268;//2130702268
                    byte[] fileByte = new byte[BufferSize];

                    BinaryReader rdr1 = new BinaryReader(FilePdf.OpenReadStream());
                    fileByte = rdr1.ReadBytes((int)FilePdf.Length);

                    Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
                    body.Name = Path.GetFileName(archivo0);
                    body.Description = "Test Description";
                    body.MimeType = MimeType.GetMimeType(fileByte, archivo0);
                    body.Parents = new List<string> { carpetaPrincipal };


                    System.IO.MemoryStream stream2 = new MemoryStream(fileByte);
                    FilesResource.CreateMediaUpload request = service.Files.Create(body, stream2, MimeType.GetMimeType(fileByte, archivo0));
                    request.Fields = "id, name, webViewLink";
                    var result = request.Upload();
                    Google.Apis.Drive.v3.Data.File fileR = request.ResponseBody; //returns null value
                    if (fileR.WebViewLink != null)
                    {

                        url0 = fileR.WebViewLink;


                    }
                }


    

                return true;


            }
            catch (Exception)
            {

                throw;
            }



           
        }


        public string CrearDirectorio_Firmante()
        {
            var fileName = "DocumentoFirma.pdf";
           

            string pathDoc_pdf = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "DocumentoFirma"));
            string path_docFirma;
            if (!Directory.Exists(pathDoc_pdf))
            {
                Directory.CreateDirectory(pathDoc_pdf);
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                path_docFirma = Path.Combine(contentRootPath, "DocumentoFirma", fileName);
            }
            else
            {

                string contentRootPath = _webHostEnvironment.ContentRootPath;
                path_docFirma = Path.Combine(contentRootPath, "DocumentoFirma", fileName);
            }

            return path_docFirma;
        }


        public string SubirArchivoDrive( )
        {
            string res = "";
            var service =  ServicioDrive();

            string pathPdf_firma = CrearDirectorio_Firmante();

            if (File.Exists(pathPdf_firma))
            {
                string archivo0 = ("FIRMANTE-" + DateTime.Now.ToString("yyyyMMddHHmmss")).ToLower();
               

                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = System.IO.Path.GetFileName(archivo0),
                    Description= "Test Description",
                    Parents = new List<string> { carpetaPrincipal }

                };
                FilesResource.CreateMediaUpload request;
                using (var stream = new System.IO.FileStream(pathPdf_firma, System.IO.FileMode.Open))
                {
                    request = service.Files.Create(
                        fileMetadata, stream, "application/octet-stream");
                        request.Fields = "id, name, webViewLink";
                        request.Upload();
                }
                if(request.ResponseBody.WebViewLink != null)
                {
                    url0 = request.ResponseBody.WebViewLink;
                    id_docDrive = request.ResponseBody.Id;
                    
                    StringBuilder sb = new StringBuilder();
                    sb.Append("https://drive.google.com/uc?export=download&id=").ToString();
                    sb.Append(id_docDrive).ToString();
                    res = sb.ToString();
                    //res = url0;
                    File.Delete(pathPdf_firma);
                }
                

            }
            else
            {
                res = "";
            }

            return res;

        }

    }
}
