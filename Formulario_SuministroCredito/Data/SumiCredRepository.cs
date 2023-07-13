using Dapper;
using Formulario_SuministroCredito.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Policy;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Formulario_SuministroCredito.Data
{
    public class SumiCredRepository : ISumiCredRepository
    {
        private readonly IDbConnection _dbconnection;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SumiCredRepository(IDbConnection dbConnection, IWebHostEnvironment webHostEnvironment)
        {
            _dbconnection = dbConnection;
            _webHostEnvironment = webHostEnvironment;
        }


        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "Drive API .NET Quickstart";
        private const string MimeTypeFolder = "application/vnd.google-apps.folder";
        private const string carpetaPrincipal = "1XP3rr0b8a9lFS3TuMNf3fjZehHq9YncK";
        private string url0 = "";
        private string url1 = "";
        private string url2 = "";
        private string url3 = "";
        private string url4 = "";
        private string url5 = "";
       

        public string Url0 { get { return url0; } }
        public string Url1 { get { return url1; } }
        public string Url2 { get { return url2; } }
        public string Url3 { get { return url3; } }
        public string Url4 { get { return url4; } }
        public string Url5 { get { return url5; } }

        


        public async Task<IEnumerable<SuministroCredito>> GetAll()
        {
            var sql = @"SELECT * FROM n97eed5_MaestrosProcesos.Suministro_Credito";

             var resultado =  await _dbconnection.QueryAsync<SuministroCredito>(sql, new { });
            return resultado;
        }

        public async Task<SuministroCredito> GetDatail(int id)
        {
            var sql = @"SELECT     
                                   fecha_registro,    
                                   tipo_solicitud, 
                                   monto, 
                                   plazo,
                                   apellidos_nombres_razon_social,
                                   tipo_persona,
                                   tipo_identificacion,
                                   numero_identificacion,    
                                   dv,
                                   representante_legal,
                                   cargo,
                                   correo_electronico,
                                   direccion_correspondecia,
                                   ciudad,
                                   departamento,
                                   telefono,
                                   celular,
                                   correo_electronico_facturacion,
                                   entidad_razon_social_ref_comerciales,
                                   direccion_ref_comerciales,
                                   nom_contacto_ref_comerciales,
                                   cargo_ref_comerciales,
                                   telefono_ref_comerciales,
                                   entidad_razon_social_ref_comerciales_dos,
                                   direccion_ref_comerciales_dos,
                                   nom_contacto_ref_comerciales_dos,
                                   cargo_ref_comerciales_dos,
                                   telefono_ref_comerciales_dos,
                                   entidad_financiera,
                                   tipo_cuenta,
                                   numero_cuenta,
                                   oficina,                                                      
                                   nombre_contacto_tesoreria,
                                   cargo_contacto_tesoreria,
                                   telefono_contacto_tesoreria,
                                   celular_contacto_tesoreria,
                                   correo_electronico_contacto_tesoreria,                                                       
                                   nombre_contacto_contabilidad,
                                   cargo_contacto_contabilidad,
                                   telefono_contacto_contabilidad,
                                   celular_contacto_contabilidad,
                                   correo_electronico_contacto_contabilidad,                                                    
                                   nombre_contacto_compras,
                                   cargo_contacto_compras,
                                   telefono_contacto_compras,
                                   celular_contacto_compras,
                                   correo_electronico_contacto_compras,    
                                   ruta_rut,
                                   ruta_estado_financiero,
                                   ruta_existencia,
                                   ruta_cert_ingresos,
                                   ruta_tarjeta_profesional,
                                   ruta_cert_antecedentes,
                                   nombre_apellido_firma,
                                   nro_cedula_firma,
                                   representa_legal_firma,
                                   centro_distribucion,
                                   cupo_sugerido,
                                   plazo_aliar,
                                   nom_asesor_comercial
                      

                        FROM n97eed5_MaestrosProcesos.Suministro_Credito
                        WHERE idSuministro_Credito = @IdSuministro_Credito ";

            return await _dbconnection.QueryFirstOrDefaultAsync<SuministroCredito>(sql, new { IdSuministro_Credito = id });
        }


        public string CountRowDb()
        {
            var sql = @"select count(*) + 1 as contador from n97eed5_MaestrosProcesos.Suministro_Credito";

            var count = _dbconnection.ExecuteScalar(sql);
            
            return count.ToString();
        }





        public async Task<bool> Insert(SuministroCredito suministroCredito)
        {


            AdjuntarArchivos(suministroCredito.RutFile, 
                             suministroCredito.EstadoFinancieroFile, 
                             suministroCredito.ExistenciaFile, 
                             suministroCredito.CertificadoIngresosFile, 
                             suministroCredito.TarjetaProfesionalFile, 
                             suministroCredito.CertificadoAntecedentesFile);

            suministroCredito.Ruta_rut = Url0;
            suministroCredito.Ruta_estado_financiero = Url1;
            suministroCredito.Ruta_existencia= Url2;
            suministroCredito.Ruta_cert_ingresos = Url3;
            suministroCredito.Ruta_tarjeta_profesional = Url4;
            suministroCredito.Ruta_cert_antecedentes = Url5;
       



            try
            {
                var sql = @"INSERT INTO Suministro_Credito(
                                                        fecha_registro,    
                                                        tipo_solicitud, 
                                                        monto, 
                                                        plazo,
                                                        apellidos_nombres_razon_social,
                                                        tipo_persona,
                                                        tipo_identificacion,
                                                        numero_identificacion,    
                                                        dv,
                                                        representante_legal,
                                                        cargo,
                                                        correo_electronico,
                                                        direccion_correspondencia,
                                                        ciudad,
                                                        departamento,
                                                        telefono,
                                                        celular,
                                                        correo_electronico_facturacion,
                                                        entidad_razon_social_ref_comerciales,
                                                        direccion_ref_comerciales,
                                                        nom_contacto_ref_comerciales,
                                                        cargo_ref_comerciales,
                                                        telefono_ref_comerciales,
                                                        entidad_razon_social_ref_comerciales_dos,
                                                        direccion_ref_comerciales_dos,
                                                        nom_contacto_ref_comerciales_dos,
                                                        cargo_ref_comerciales_dos,
                                                        telefono_ref_comerciales_dos,
                                                        entidad_financiera,
                                                        tipo_cuenta,
                                                        numero_cuenta,
                                                        oficina,                                                      
                                                        nombre_contacto_tesoreria,
                                                        cargo_contacto_tesoreria,
                                                        telefono_contacto_tesoreria,
                                                        celular_contacto_tesoreria,
                                                        correo_electronico_contacto_tesoreria,                                                       
                                                        nombre_contacto_contabilidad,
                                                        cargo_contacto_contabilidad,
                                                        telefono_contacto_contabilidad,
                                                        celular_contacto_contabilidad,
                                                        correo_electronico_contacto_contabilidad,                                                    
                                                        nombre_contacto_compras,
                                                        cargo_contacto_compras,
                                                        telefono_contacto_compras,
                                                        celular_contacto_compras,
                                                        correo_electronico_contacto_compras,    
                                                        ruta_rut,
                                                        ruta_estado_financiero,
                                                        ruta_existencia,
                                                        ruta_cert_ingresos,
                                                        ruta_tarjeta_profesional,
                                                        ruta_cert_antecedentes,
                                                        nombre_apellido_firma,
                                                        nro_cedula_firma,
                                                        representa_legal_firma,
                                                        centro_distribucion,
                                                        cupo_sugerido,
                                                        plazo_aliar,
                                                        nom_asesor_comercial,
                                                        fecha_solicitud)                                          
                                                  VALUES
                                                           (
                                                            @FechaRegistro,    
                                                            @Tipo_solicitud, 
                                                            @Monto, 
                                                            @plazo,
                                                            @Apellidos_nombres_razon_social,
                                                            @Tipo_persona,
                                                            @Tipo_identificacion,
                                                            @Numero_identificacion,    
                                                            @DV,
                                                            @Representante_legal,
                                                            @Cargo,
                                                            @Correo_electronico,
                                                            @Direccion_correspondencia,
                                                            @Ciudad,
                                                            @Departamento,
                                                            @Telefono,
                                                            @Celular,
                                                            @Correo_electronico_facturacion,
                                                            @Entidad_razon_social_ref_comerciales,
                                                            @Direccion_ref_comerciales,
                                                            @Nom_contacto_ref_comerciales,
                                                            @Cargo_ref_comerciales,
                                                            @Telefono_ref_comerciales,
                                                            @Entidad_razon_social_ref_comerciales_dos,
                                                            @Direccion_ref_comerciales_dos,
                                                            @Nom_contacto_ref_comerciales_dos,
                                                            @Cargo_ref_comerciales_dos,
                                                            @Telefono_ref_comerciales_dos,
                                                            @Entidad_financiera,
                                                            @Tipo_cuenta,
                                                            @Numero_cuenta,
                                                            @Oficina,                                      
                                                            @Nombre_contacto_tesoreria,
                                                            @Cargo_contacto_tesoreria,
                                                            @Telefono_contacto_tesoreria,
                                                            @Celular_contacto_tesoreria,
                                                            @Correo_electronico_contacto_tesoreria,        
                                                            @Nombre_contacto_contabilidad,
                                                            @Cargo_contacto_contabilidad,
                                                            @Telefono_contacto_contabilidad,
                                                            @Celular_contacto_contabilidad,
                                                            @Correo_electronico_contacto_contabilidad,     
                                                            @Nombre_contacto_compras,
                                                            @Cargo_contacto_compras,
                                                            @Telefono_contacto_compras,
                                                            @Celular_contacto_compras,
                                                            @Correo_electronico_contacto_compras,    
                                                            @Ruta_rut,
                                                            @Ruta_estado_financiero,
                                                            @Ruta_existencia,
                                                            @Ruta_cert_ingresos,
                                                            @Ruta_tarjeta_profesional,
                                                            @Ruta_cert_antecedentes,
                                                            @Nombre_apellido_firma,
                                                            @Nro_cedula_firma,
                                                            @Representante_legal,
                                                            @Centro_distribucion,
                                                            @Cupo_sugerido,
                                                            @Plazo_aliar,
                                                            @Nom_asesor_comercial,
                                                            @FechaSolicitud)";


                await _dbconnection.ExecuteAsync(sql, new
                {                  
                    suministroCredito.Fecha_registro,
                    suministroCredito.Tipo_solicitud,
                    suministroCredito.Monto,
                    suministroCredito.Plazo,
                    suministroCredito.Apellidos_nombres_razon_social,
                    suministroCredito.Tipo_persona,
                    suministroCredito.Tipo_identificacion,
                    suministroCredito.Numero_identificacion,
                    suministroCredito.DV,
                    suministroCredito.Representante_legal,
                    suministroCredito.Cargo,
                    suministroCredito.Correo_electronico,
                    suministroCredito.Direccion_correspondencia,
                    suministroCredito.Ciudad,
                    suministroCredito.Departamento,
                    suministroCredito.Telefono,
                    suministroCredito.Celular,
                    suministroCredito.Correo_electronico_facturacion,
                    suministroCredito.Entidad_razon_social_ref_comerciales,
                    suministroCredito.Direccion_ref_comerciales,
                    suministroCredito.Nom_contacto_ref_comerciales,
                    suministroCredito.Cargo_ref_comerciales,
                    suministroCredito.Telefono_ref_comerciales,
                    suministroCredito.Entidad_razon_social_ref_comerciales_dos,
                    suministroCredito.Direccion_ref_comerciales_dos,
                    suministroCredito.Nom_contacto_ref_comerciales_dos,
                    suministroCredito.Cargo_ref_comerciales_dos,
                    suministroCredito.Telefono_ref_comerciales_dos,
                    suministroCredito.Entidad_financiera,
                    suministroCredito.Tipo_cuenta,
                    suministroCredito.Numero_cuenta,
                    suministroCredito.Oficina,
                    suministroCredito.Nombre_contacto_tesoreria,
                    suministroCredito.Cargo_contacto_tesoreria,
                    suministroCredito.Telefono_contacto_tesoreria,
                    suministroCredito.Celular_contacto_tesoreria,
                    suministroCredito.Correo_electronico_contacto_tesoreria,
                    suministroCredito.Nombre_contacto_contabilidad,
                    suministroCredito.Cargo_contacto_contabilidad,
                    suministroCredito.Telefono_contacto_contabilidad,
                    suministroCredito.Celular_contacto_contabilidad,
                    suministroCredito.Correo_electronico_contacto_contabilidad,
                    suministroCredito.Nombre_contacto_compras,
                    suministroCredito.Cargo_contacto_compras,
                    suministroCredito.Telefono_contacto_compras,
                    suministroCredito.Celular_contacto_compras,
                    suministroCredito.Correo_electronico_contacto_compras,
                    suministroCredito.Ruta_rut,
                    suministroCredito.Ruta_estado_financiero,
                    suministroCredito.Ruta_existencia,
                    suministroCredito.Ruta_cert_ingresos,
                    suministroCredito.Ruta_tarjeta_profesional,
                    suministroCredito.Ruta_cert_antecedentes,
                    suministroCredito.Nombre_apellido_firma,
                    suministroCredito.Nro_cedula_firma,
                    suministroCredito.Representa_legal_firma,
                    suministroCredito.Centro_distribucion,
                    suministroCredito.Cupo_sugerido,
                    suministroCredito.Plazo_aliar,
                    suministroCredito.Nom_asesor_comercial,
                    suministroCredito.Fecha_solicitud
                    


                });

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
                return false;
            }



        }



    


        [NonAction]
        public  bool AdjuntarArchivos(IFormFile file, IFormFile file2, IFormFile file3, IFormFile file4, IFormFile file5, IFormFile file6)
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




                if (file != null)
                {

                    //string url0 = "";
                    string archivo0 = ("RUT-" + DateTime.Now.ToString("yyyyMMddHHmmss")).ToLower();
                    int BufferSize = 1130702268;//2130702268
                    byte[] fileByte = new byte[BufferSize];

                    BinaryReader rdr1 = new BinaryReader(file.OpenReadStream());
                    fileByte = rdr1.ReadBytes((int)file.Length);

                    Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
                    body.Name = System.IO.Path.GetFileName(archivo0);
                    body.Description = "Test Description";
                    body.MimeType = MimeType.GetMimeType(fileByte, archivo0);
                    body.Parents = new List<string> { carpetaPrincipal };


                    System.IO.MemoryStream stream2 = new System.IO.MemoryStream(fileByte);
                    FilesResource.CreateMediaUpload request = service.Files.Create(body, stream2, MimeType.GetMimeType(fileByte, archivo0));
                    request.Fields = "id, name, webViewLink";
                    var result = request.Upload();
                    Google.Apis.Drive.v3.Data.File fileR = request.ResponseBody; //returns null value
                    if (fileR.WebViewLink != null)
                    {

                        url0 = fileR.WebViewLink;
                         

                    }
                }


                if (file2 != null)
                {

                    
                    string archivo0 = ("Estado_financiero-" + DateTime.Now.ToString("yyyyMMddHHmmss")).ToLower();
                    int BufferSize = 1130702268;//2130702268
                    byte[] fileByte = new byte[BufferSize];

                    BinaryReader rdr1 = new BinaryReader(file2.OpenReadStream());
                    fileByte = rdr1.ReadBytes((int)file2.Length);

                    Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
                    body.Name = System.IO.Path.GetFileName(archivo0);
                    body.Description = "Test Description";
                    body.MimeType = MimeType.GetMimeType(fileByte, archivo0);
                    body.Parents = new List<string> { carpetaPrincipal };


                    System.IO.MemoryStream stream2 = new System.IO.MemoryStream(fileByte);
                    FilesResource.CreateMediaUpload request = service.Files.Create(body, stream2, MimeType.GetMimeType(fileByte, archivo0));
                    request.Fields = "id, name, webViewLink";
                    var result = request.Upload();
                    Google.Apis.Drive.v3.Data.File fileR = request.ResponseBody; //returns null value
                    if (fileR.WebViewLink != null)
                    {

                        url1 = fileR.WebViewLink;

                    }
                }


                if (file3 != null)
                {

                    
                    string archivo0 = ("Existencia-" + DateTime.Now.ToString("yyyyMMddHHmmss")).ToLower();
                    int BufferSize = 1130702268;//2130702268
                    byte[] fileByte = new byte[BufferSize];

                    BinaryReader rdr1 = new BinaryReader(file3.OpenReadStream());
                    fileByte = rdr1.ReadBytes((int)file3.Length);

                    Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
                    body.Name = System.IO.Path.GetFileName(archivo0);
                    body.Description = "Test Description";
                    body.MimeType = MimeType.GetMimeType(fileByte, archivo0);
                    body.Parents = new List<string> { carpetaPrincipal };


                    System.IO.MemoryStream stream2 = new System.IO.MemoryStream(fileByte);
                    FilesResource.CreateMediaUpload request = service.Files.Create(body, stream2, MimeType.GetMimeType(fileByte, archivo0));
                    request.Fields = "id, name, webViewLink";
                    var result = request.Upload();
                    Google.Apis.Drive.v3.Data.File fileR = request.ResponseBody; //returns null value
                    if (fileR.WebViewLink != null)
                    {

                        url2 = fileR.WebViewLink;

                    }
                }


                if (file4 != null)
                {

                    string archivo0 = ("Certificado_ingresos-" + DateTime.Now.ToString("yyyyMMddHHmmss")).ToLower();
                    int BufferSize = 1130702268;//2130702268
                    byte[] fileByte = new byte[BufferSize];

                    BinaryReader rdr1 = new BinaryReader(file4.OpenReadStream());
                    fileByte = rdr1.ReadBytes((int)file4.Length);

                    Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
                    body.Name = System.IO.Path.GetFileName(archivo0);
                    body.Description = "Test Description";
                    body.MimeType = MimeType.GetMimeType(fileByte, archivo0);
                    body.Parents = new List<string> { carpetaPrincipal };


                    System.IO.MemoryStream stream2 = new System.IO.MemoryStream(fileByte);
                    FilesResource.CreateMediaUpload request = service.Files.Create(body, stream2, MimeType.GetMimeType(fileByte, archivo0));
                    request.Fields = "id, name, webViewLink";
                    var result = request.Upload();
                    Google.Apis.Drive.v3.Data.File fileR = request.ResponseBody; //returns null value
                    if (fileR.WebViewLink != null)
                    {

                        url3 = fileR.WebViewLink;

                    }
                }

                if (file5 != null)
                {

                   
                    string archivo0 = ("Tarjeta_profesional-" + DateTime.Now.ToString("yyyyMMddHHmmss")).ToLower();
                    int BufferSize = 1130702268;//2130702268
                    byte[] fileByte = new byte[BufferSize];

                    BinaryReader rdr1 = new BinaryReader(file5.OpenReadStream());
                    fileByte = rdr1.ReadBytes((int)file5.Length);

                    Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
                    body.Name = System.IO.Path.GetFileName(archivo0);
                    body.Description = "Test Description";
                    body.MimeType = MimeType.GetMimeType(fileByte, archivo0);
                    body.Parents = new List<string> { carpetaPrincipal };


                    System.IO.MemoryStream stream2 = new System.IO.MemoryStream(fileByte);
                    FilesResource.CreateMediaUpload request = service.Files.Create(body, stream2, MimeType.GetMimeType(fileByte, archivo0));
                    request.Fields = "id, name, webViewLink";
                    var result = request.Upload();
                    Google.Apis.Drive.v3.Data.File fileR = request.ResponseBody; //returns null value
                    if (fileR.WebViewLink != null)
                    {

                        url4 = fileR.WebViewLink;

                    }
                }

                if (file6 != null)
                {


                    string archivo0 = ("Certificado_antecedentes-" + DateTime.Now.ToString("yyyyMMddHHmmss")).ToLower();
                    int BufferSize = 1130702268;//2130702268
                    byte[] fileByte = new byte[BufferSize];

                    BinaryReader rdr1 = new BinaryReader(file6.OpenReadStream());
                    fileByte = rdr1.ReadBytes((int)file6.Length);

                    Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
                    body.Name = System.IO.Path.GetFileName(archivo0);
                    body.Description = "Test Description";
                    body.MimeType = MimeType.GetMimeType(fileByte, archivo0);
                    body.Parents = new List<string> { carpetaPrincipal };


                    System.IO.MemoryStream stream2 = new System.IO.MemoryStream(fileByte);
                    FilesResource.CreateMediaUpload request = service.Files.Create(body, stream2, MimeType.GetMimeType(fileByte, archivo0));
                    request.Fields = "id, name, webViewLink";
                    var result = request.Upload();
                    Google.Apis.Drive.v3.Data.File fileR = request.ResponseBody; //returns null value
                    if (fileR.WebViewLink != null)
                    {

                        url5 = fileR.WebViewLink;

                    }
                }










                return true;


            }
            catch (Exception)
            {

                throw;
            }



            //try
            //{
            //    string pathArchAdjuntar = "";
            //    if (file.Length > 0)
            //    {
            //        pathArchAdjuntar = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
            //        if (!Directory.Exists(pathArchAdjuntar))
            //        {
            //            Directory.CreateDirectory(pathArchAdjuntar);
            //        }
            //        using (var fileStream = new FileStream(Path.Combine(pathArchAdjuntar, file.FileName), FileMode.Create))
            //        {
            //            file.CopyToAsync(fileStream);
            //        }
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("File Copy Failed", ex);
            //}
        }

        public SuministroCredito GetById(int id)
        {
            var sql = @"SELECT     idSuministro_Credito,
                                   fecha_registro,    
                                   tipo_solicitud, 
                                   monto, 
                                   plazo,
                                   apellidos_nombres_razon_social,
                                   tipo_persona,
                                   tipo_identificacion,
                                   numero_identificacion,    
                                   dv,
                                   representante_legal,
                                   cargo,
                                   correo_electronico,
                                   direccion_correspondencia,
                                   ciudad,
                                   departamento,
                                   telefono,
                                   celular,
                                   correo_electronico_facturacion,
                                   entidad_razon_social_ref_comerciales,
                                   direccion_ref_comerciales,
                                   nom_contacto_ref_comerciales,
                                   cargo_ref_comerciales,
                                   telefono_ref_comerciales,
                                   entidad_razon_social_ref_comerciales_dos,
                                   direccion_ref_comerciales_dos,
                                   nom_contacto_ref_comerciales_dos,
                                   cargo_ref_comerciales_dos,
                                   telefono_ref_comerciales_dos,
                                   entidad_financiera,
                                   tipo_cuenta,
                                   numero_cuenta,
                                   oficina,                                                      
                                   nombre_contacto_tesoreria,
                                   cargo_contacto_tesoreria,
                                   telefono_contacto_tesoreria,
                                   celular_contacto_tesoreria,
                                   correo_electronico_contacto_tesoreria,                                                       
                                   nombre_contacto_contabilidad,
                                   cargo_contacto_contabilidad,
                                   telefono_contacto_contabilidad,
                                   celular_contacto_contabilidad,
                                   correo_electronico_contacto_contabilidad,                                                    
                                   nombre_contacto_compras,
                                   cargo_contacto_compras,
                                   telefono_contacto_compras,
                                   celular_contacto_compras,
                                   correo_electronico_contacto_compras,    
                                   ruta_rut,
                                   ruta_estado_financiero,
                                   ruta_existencia,
                                   ruta_cert_ingresos,
                                   ruta_tarjeta_profesional,
                                   ruta_cert_antecedentes,
                                   nombre_apellido_firma,
                                   nro_cedula_firma,
                                   representa_legal_firma,
                                   centro_distribucion,
                                   cupo_sugerido,
                                   plazo_aliar,
                                   nom_asesor_comercial
                      

                        FROM n97eed5_MaestrosProcesos.Suministro_Credito
                        WHERE idSuministro_Credito = @IdSuministro_Credito ";

            var detalle = _dbconnection.QueryFirstOrDefault<SuministroCredito>(sql, new { IdSuministro_Credito = id });
            return detalle;
        }

        public async Task<bool> UploadFile(IFormFile file)
        {
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "img"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }

    }



    
}
