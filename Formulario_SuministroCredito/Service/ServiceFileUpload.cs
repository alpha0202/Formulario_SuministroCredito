﻿using Formulario_SuministroCredito.Models;
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

        public string Url0 { get { return url0; } }



        public string GetFile(string rutaPDF_firmar)
        {
            using (FileStream fs = new FileStream(rutaPDF_firmar, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    string contents = reader.ReadToEnd();
                    byte[] byteArray = Encoding.UTF8.GetBytes(contents);
                    MemoryStream stream = new MemoryStream(byteArray);
                    FilePdf = new FormFile(stream, 0, stream.Length, null, "ProbandoPdf.pdf");
                }
            }

            AdjuntarArchivos(FilePdf);

            return "listo";
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


        public bool SubirArchivoDrive( string pathPdf)
        {
           var service =  ServicioDrive();


            string archivo0 = ("FIRMANTE-" + DateTime.Now.ToString("yyyyMMddHHmmss")).ToLower();
            int BufferSize = 1130702268;//2130702268
            byte[] fileByte = new byte[BufferSize];

            //BinaryReader rdr1 = new BinaryReader(pathPdf.OpenReadStream());
            //fileByte = rdr1.ReadBytes((int)FilePdf.Length);

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = System.IO.Path.GetFileName(pathPdf),
                Description= "Test Description",
                Parents = new List<string> { carpetaPrincipal }

            };
            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(pathPdf, System.IO.FileMode.Open))
            {
                request = service.Files.Create(
                    fileMetadata, stream, "application/octet-stream");
                    request.Fields = "id, name, webViewLink";
                    request.Upload();
            }
            if(request.ResponseBody.WebViewLink != null)
            {
                url0 = request.ResponseBody.WebViewLink;
            }





            //BinaryReader rdr1 = new BinaryReader(FilePdf.OpenReadStream());
            //fileByte = rdr1.ReadBytes((int)FilePdf.Length);

            //Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
            //body.Name = Path.GetFileName(archivo0);
            //body.Description = "Test Description";
            //body.MimeType = MimeType.GetMimeType(fileByte, archivo0);
            //body.Parents = new List<string> { carpetaPrincipal };


            //System.IO.MemoryStream stream2 = new MemoryStream(fileByte);
            //FilesResource.CreateMediaUpload request = service.Files.Create(body, stream2, MimeType.GetMimeType(fileByte, archivo0));
            //request.Fields = "id, name, webViewLink";
            //var result = request.Upload();
            //Google.Apis.Drive.v3.Data.File fileR = request.ResponseBody; //returns null value
            //if (fileR.WebViewLink != null)
            //{

            //    url0 = fileR.WebViewLink;


            //}





            return true;

        }


    }
}