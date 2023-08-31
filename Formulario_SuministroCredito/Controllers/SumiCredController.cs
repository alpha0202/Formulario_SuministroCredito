using FluentValidation;
using FluentValidation.AspNetCore;
using Formulario_SuministroCredito.Data;
using Formulario_SuministroCredito.Models;
using Formulario_SuministroCredito.Validator;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Net.Http;
using Newtonsoft.Json;
using Grpc.Core;
using Path = System.IO.Path;
using Rotativa;
using ViewAsPdf = Rotativa.AspNetCore.ViewAsPdf;
using Formulario_SuministroCredito.Service;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2013.Excel;
using Microsoft.AspNetCore.Hosting;

namespace Formulario_SuministroCredito.Controllers
{
    public class SumiCredController : Controller
    {
        private readonly ISumiCredRepository _sumiCredRepository;
        private readonly IServiceFileUpload _serviceFileUpload;
        private readonly IValidator<SuministroCredito> _validator;
        private readonly HttpClient _httpClient;    
        private readonly IWebHostEnvironment _webHostEnvironment;


        public SumiCredController(
                                     ISumiCredRepository sumiCredRepository, 
                                     IValidator<SuministroCredito> validator, 
                                     HttpClient httpClient, 
                                     IServiceFileUpload serviceFileUpload, 
                                     IWebHostEnvironment webHostEnvironment)

        {
            _sumiCredRepository = sumiCredRepository;
            _validator = validator;
            _httpClient = httpClient;
            _serviceFileUpload = serviceFileUpload;
            _webHostEnvironment = webHostEnvironment;
        }



        public async Task<ActionResult> Index()
        {

            var suministros = await _sumiCredRepository.GetAll();
           
            return View(suministros);
        }

        //GET:SumiCredController/detail
        public async Task<ActionResult> GetDatail(int id)
        {
            var detalle = await _sumiCredRepository.GetDatail(id);
            //PdfNew(detalle);

            return View(detalle);
        }



        // GET: SumiCredController/Create
        public IActionResult Insert()
        {
           
            var contador = _sumiCredRepository.CountRowDb();
            var response = contador;
            ViewData["MiContador"] = response;

            //var res = await _httpClient.GetAsync("https://raw.githubusercontent.com/marcovega/colombia-json/master/colombia.min.json");
            //var content = await res.Content.ReadAsStringAsync();
            ////var colombia = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(content);
            //var colombia = JsonConvert.DeserializeObject<List<DptoCiudades>>(content);
            //var colombia = System.Text.Json.JsonSerializer.Deserialize<List<DptoCiudades>>(content);
            //return Ok(colombia);
            //ViewData["Pais"] = colombia;

            return View();
        }




        // GET: SumiCredController/Create
        public ActionResult Insertar()
        {
            return View();
        }


        // POST: SumiCredController/Insert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Insert(IFormCollection collection)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    var contador = _sumiCredRepository.CountRowDb();

                    var suministroCredito = new SuministroCredito()
                    {
                        Fecha_registro = DateTime.Now,
                        Tipo_solicitud = collection["Tipo_solicitud"],
                        Monto = collection["Monto"],
                        Plazo = collection["Plazo"],
                        Apellidos_nombres_razon_social = collection["Apellidos_nombres_razon_social"],
                        Tipo_persona = collection["Tipo_persona"],
                        Tipo_identificacion = collection["Tipo_identificacion"],
                        Numero_identificacion = collection["Numero_identificacion"],
                        DV = collection["DV"],
                        Representante_legal = collection["Representante_legal"],
                        Cargo = collection["Cargo"],
                        Correo_electronico = collection["Correo_electronico"],
                        Direccion_correspondencia = collection["Direccion_correspondencia"],
                        Ciudad = collection["Ciudad"],
                        Departamento = collection["Departamento"],
                        Telefono = collection["Telefono"],
                        Celular = collection["Celular"],
                        Correo_electronico_facturacion = collection["Correo_electronico_facturacion"],
                        Entidad_razon_social_ref_comerciales = collection["Entidad_razon_social_ref_comerciales"],
                        Direccion_ref_comerciales = collection["Direccion_ref_comerciales"],
                        Nom_contacto_ref_comerciales = collection["Nom_contacto_ref_comerciales"],
                        Cargo_ref_comerciales = collection["Cargo_ref_comerciales"],
                        Telefono_ref_comerciales = collection["Telefono_ref_comerciales"],
                        Entidad_razon_social_ref_comerciales_dos = collection["Entidad_razon_social_ref_comerciales_dos"],
                        Direccion_ref_comerciales_dos = collection["Direccion_ref_comerciales_dos"],
                        Nom_contacto_ref_comerciales_dos = collection["Nom_contacto_ref_comerciales_dos"],
                        Cargo_ref_comerciales_dos = collection["Cargo_ref_comerciales_dos"],
                        Telefono_ref_comerciales_dos = collection["Telefono_ref_comerciales_dos"],
                        Entidad_financiera = collection["Entidad_financiera"],
                        Tipo_cuenta = collection["Tipo_cuenta"],
                        Numero_cuenta = collection["Numero_cuenta"],
                        Oficina = collection["Oficina"],
                        Nombre_contacto_tesoreria = collection["Nombre_contacto_tesoreria"],
                        Cargo_contacto_tesoreria = collection["Cargo_contacto_tesoreria"],
                        Telefono_contacto_tesoreria = collection["Telefono_contacto_tesoreria"],
                        Celular_contacto_tesoreria = collection["Celular_contacto_tesoreria"],
                        Correo_electronico_contacto_tesoreria = collection["Correo_electronico_contacto_tesoreria"],
                        Nombre_contacto_contabilidad = collection["Nombre_contacto_contabilidad"],
                        Cargo_contacto_contabilidad = collection["Cargo_contacto_contabilidad"],
                        Telefono_contacto_contabilidad = collection["Telefono_contacto_contabilidad"],
                        Celular_contacto_contabilidad = collection["Celular_contacto_contabilidad"],
                        Correo_electronico_contacto_contabilidad = collection["Correo_electronico_contacto_contabilidad"],
                        Nombre_contacto_compras = collection["Nombre_contacto_compras"],
                        Cargo_contacto_compras = collection["Cargo_contacto_compras"],
                        Telefono_contacto_compras = collection["Telefono_contacto_compras"],
                        Celular_contacto_compras = collection["Celular_contacto_compras"],
                        Correo_electronico_contacto_compras = collection["Correo_electronico_contacto_compras"],
                        Ruta_rut = collection["Ruta_rut"],
                        Ruta_estado_financiero = collection["Ruta_estado_financiero"],
                        Ruta_existencia = collection["Ruta_existencia"],
                        Ruta_cert_ingresos = collection["Ruta_cert_ingresos"],
                        Ruta_tarjeta_profesional = collection["Ruta_tarjeta_profesional"],
                        Ruta_cert_antecedentes = collection["Ruta_cert_antecedentes"],
                        Nombre_apellido_firma = collection["Nombre_apellido_firma"],
                        Nro_cedula_firma = collection["Nro_cedula_firma"],
                        Representa_legal_firma = collection["Representa_legal_firma"],
                        Centro_distribucion = collection["Centro_distribucion"],
                        Cupo_sugerido = collection["Cupo_sugerido"],
                        Plazo_aliar = collection["Plazo_aliar"],
                        Nom_asesor_comercial = collection["Nom_asesor_comercial"],
                        RutFile = collection.Files["RutFile"],
                        EstadoFinancieroFile = collection.Files["EstadoFinancieroFile"],
                        ExistenciaFile = collection.Files["ExistenciaFile"],
                        CertificadoIngresosFile = collection.Files["CertificadoIngresosFile"],
                        TarjetaProfesionalFile = collection.Files["TarjetaProfesionalFile"],
                        CertificadoAntecedentesFile = collection.Files["CertificadoAntecedentesFile"],
                        NumeroContador = contador,
                        Fecha_solicitud = DateTime.Now.ToString("yyyy-MMM-dd")


                    };

       


                    bool resultado = await _sumiCredRepository.Insert(suministroCredito);

                 
                    ViewBag.Mensaje = "Guardado Correcto";
                    TempData["MensajeAccion"] = "Solicitud creada correctamente!";
                    return RedirectToAction(nameof(Insert));
                    //return Redirect("https://www.aliar.com.co/");



                }
                catch
                {
                    return View();

                }

            }
            return RedirectToAction(nameof(Insert));
            //return Redirect("https://www.aliar.com.co/");

        }


        //creación del documento para firmar.
        public IActionResult PdfNew(IFormCollection collectionPdf)
        {
            var contador = _sumiCredRepository.CountRowDb();
            var suministroCreditoPdf = new SuministroCredito()
            {
                IdSuministro_Credito= int.Parse(contador),
                Fecha_registro = DateTime.Now,
                Tipo_solicitud = collectionPdf["Tipo_solicitud"],
                Monto = collectionPdf["Monto"],
                Plazo = collectionPdf["Plazo"],
                Apellidos_nombres_razon_social = collectionPdf["Apellidos_nombres_razon_social"],
                Tipo_persona = collectionPdf["Tipo_persona"],
                Tipo_identificacion = collectionPdf["Tipo_identificacion"],
                Numero_identificacion = collectionPdf["Numero_identificacion"],
                DV = collectionPdf["DV"],
                Representante_legal = collectionPdf["Representante_legal"],
                Cargo = collectionPdf["Cargo"],
                Correo_electronico = collectionPdf["Correo_electronico"],
                Direccion_correspondencia = collectionPdf["Direccion_correspondencia"],
                Ciudad = collectionPdf["Ciudad"],
                Departamento = collectionPdf["Departamento"],
                Telefono = collectionPdf["Telefono"],
                Celular = collectionPdf["Celular"],
                Correo_electronico_facturacion = collectionPdf["Correo_electronico_facturacion"],
                Entidad_razon_social_ref_comerciales = collectionPdf["Entidad_razon_social_ref_comerciales"],
                Direccion_ref_comerciales = collectionPdf["Direccion_ref_comerciales"],
                Nom_contacto_ref_comerciales = collectionPdf["Nom_contacto_ref_comerciales"],
                Cargo_ref_comerciales = collectionPdf["Cargo_ref_comerciales"],
                Telefono_ref_comerciales = collectionPdf["Telefono_ref_comerciales"],
                Entidad_razon_social_ref_comerciales_dos = collectionPdf["Entidad_razon_social_ref_comerciales_dos"],
                Direccion_ref_comerciales_dos = collectionPdf["Direccion_ref_comerciales_dos"],
                Nom_contacto_ref_comerciales_dos = collectionPdf["Nom_contacto_ref_comerciales_dos"],
                Cargo_ref_comerciales_dos = collectionPdf["Cargo_ref_comerciales_dos"],
                Telefono_ref_comerciales_dos = collectionPdf["Telefono_ref_comerciales_dos"],
                Entidad_financiera = collectionPdf["Entidad_financiera"],
                Tipo_cuenta = collectionPdf["Tipo_cuenta"],
                Numero_cuenta = collectionPdf["Numero_cuenta"],
                Oficina = collectionPdf["Oficina"],
                Nombre_contacto_tesoreria = collectionPdf["Nombre_contacto_tesoreria"],
                Cargo_contacto_tesoreria = collectionPdf["Cargo_contacto_tesoreria"],
                Telefono_contacto_tesoreria = collectionPdf["Telefono_contacto_tesoreria"],
                Celular_contacto_tesoreria = collectionPdf["Celular_contacto_tesoreria"],
                Correo_electronico_contacto_tesoreria = collectionPdf["Correo_electronico_contacto_tesoreria"],
                Nombre_contacto_contabilidad = collectionPdf["Nombre_contacto_contabilidad"],
                Cargo_contacto_contabilidad = collectionPdf["Cargo_contacto_contabilidad"],
                Telefono_contacto_contabilidad = collectionPdf["Telefono_contacto_contabilidad"],
                Celular_contacto_contabilidad = collectionPdf["Celular_contacto_contabilidad"],
                Correo_electronico_contacto_contabilidad = collectionPdf["Correo_electronico_contacto_contabilidad"],
                Nombre_contacto_compras = collectionPdf["Nombre_contacto_compras"],
                Cargo_contacto_compras = collectionPdf["Cargo_contacto_compras"],
                Telefono_contacto_compras = collectionPdf["Telefono_contacto_compras"],
                Celular_contacto_compras = collectionPdf["Celular_contacto_compras"],
                Correo_electronico_contacto_compras = collectionPdf["Correo_electronico_contacto_compras"],
                Nombre_apellido_firma = collectionPdf["Nombre_apellido_firma"],
                Nro_cedula_firma = collectionPdf["Nro_cedula_firma"],
                Representa_legal_firma = collectionPdf["Representa_legal_firma"],
                NumeroContador = contador,
                Fecha_solicitud = DateTime.Now.ToString("yyyy-MMM-dd")


            };

            string fileName = "DocumentoFirma.pdf";
            string path_docFirma = _serviceFileUpload.CrearDirectorio_Firmante();

            //ViewBag.Mensaje = "Guardado Correcto";
            TempData["MensajeAccion"] = "Documento PDF generado correctamente..!";
            return new ViewAsPdf("PdfNew", suministroCreditoPdf)

            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                FileName = fileName,
                SaveOnServerPath = path_docFirma

            };


        }


        public IActionResult EnviarDrive()
        {
            
            var enviarToDrive = _serviceFileUpload.SubirArchivoDrive();

            TempData["MensajeAccion"] = "Documento enviado OK...!";
            return RedirectToAction("Index");
        }



        public IActionResult pdf_firma_solicita(int id)
        {
            var contador = _sumiCredRepository.CountRowDb();
            var detalle = _sumiCredRepository.GetById(id);

            var suministroCredito = new SuministroCredito()
            {
                NumeroContador = contador,
                IdSuministro_Credito = detalle.IdSuministro_Credito,
                Fecha_registro = detalle.Fecha_registro,
                Tipo_solicitud = detalle.Tipo_solicitud,
                Monto = detalle.Monto,
                Plazo = detalle.Plazo,
                Apellidos_nombres_razon_social = detalle.Apellidos_nombres_razon_social,
                Tipo_persona = detalle.Tipo_persona,
                Tipo_identificacion = detalle.Tipo_identificacion,
                Numero_identificacion = detalle.Numero_identificacion,
                DV = detalle.DV,
                Representante_legal = detalle.Representante_legal,
                Cargo = detalle.Cargo,
                Correo_electronico = detalle.Correo_electronico,
                Direccion_correspondencia = detalle.Direccion_correspondencia,
                Ciudad = detalle.Ciudad,
                Departamento = detalle.Departamento,
                Telefono = detalle.Telefono,
                Celular = detalle.Celular,
                Correo_electronico_facturacion = detalle.Correo_electronico_facturacion,
                Entidad_razon_social_ref_comerciales = detalle.Entidad_razon_social_ref_comerciales,
                Direccion_ref_comerciales = detalle.Direccion_ref_comerciales,
                Nom_contacto_ref_comerciales = detalle.Nom_contacto_ref_comerciales,
                Cargo_ref_comerciales = detalle.Cargo_ref_comerciales,
                Telefono_ref_comerciales = detalle.Telefono_ref_comerciales,
                Entidad_razon_social_ref_comerciales_dos = detalle.Entidad_razon_social_ref_comerciales_dos,
                Direccion_ref_comerciales_dos = detalle.Direccion_ref_comerciales_dos,
                Nom_contacto_ref_comerciales_dos = detalle.Nom_contacto_ref_comerciales_dos,
                Cargo_ref_comerciales_dos = detalle.Cargo_ref_comerciales_dos,
                Telefono_ref_comerciales_dos = detalle.Telefono_ref_comerciales_dos,
                Entidad_financiera = detalle.Entidad_financiera,
                Tipo_cuenta = detalle.Tipo_cuenta,
                Numero_cuenta = detalle.Numero_cuenta,
                Oficina = detalle.Oficina,
                Nombre_contacto_tesoreria = detalle.Nombre_contacto_tesoreria,
                Cargo_contacto_tesoreria = detalle.Cargo_contacto_tesoreria,
                Telefono_contacto_tesoreria = detalle.Telefono_contacto_tesoreria,
                Celular_contacto_tesoreria = detalle.Celular_contacto_tesoreria,
                Correo_electronico_contacto_tesoreria = detalle.Correo_electronico_contacto_tesoreria,
                Nombre_contacto_contabilidad = detalle.Nombre_contacto_contabilidad,
                Cargo_contacto_contabilidad = detalle.Cargo_contacto_contabilidad,
                Telefono_contacto_contabilidad = detalle.Telefono_contacto_contabilidad,
                Celular_contacto_contabilidad = detalle.Celular_contacto_contabilidad,
                Correo_electronico_contacto_contabilidad = detalle.Correo_electronico_contacto_contabilidad,
                Nombre_contacto_compras = detalle.Nombre_contacto_compras,
                Cargo_contacto_compras = detalle.Cargo_contacto_compras,
                Telefono_contacto_compras = detalle.Telefono_contacto_compras,
                Celular_contacto_compras = detalle.Celular_contacto_compras,
                Correo_electronico_contacto_compras = detalle.Correo_electronico_contacto_compras,
                Nombre_apellido_firma = detalle.Nombre_apellido_firma,
                Nro_cedula_firma = detalle.Nro_cedula_firma,
                Representa_legal_firma = detalle.Representa_legal_firma,
                Fecha_solicitud = detalle.Fecha_solicitud

            };


            string fileName = "DocumentoFirma.pdf";                   
            string path_docFirma = _serviceFileUpload.CrearDirectorio_Firmante();


            TempData["MensajeAccion"] = "Documento PDF creado correctamente...!";

            return new ViewAsPdf("pdf_firma_solicita", suministroCredito)

            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                FileName = fileName,
                SaveOnServerPath = path_docFirma

            }; 
            

            //.BuildFile(this.ControllerContext);
            //bool enviarDrive = _serviceFileUpload.SubirArchivoDrive(path);

            //var myPDF = new ViewAsPdf("PdfNew", suministroCredito)
            //{
            //    FileName = fileName,
            //    PageSize = Rotativa.AspNetCore.Options.Size.A4,
            //    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
            //    SaveOnServerPath = path
            //};
            //var enviarToDrive = _serviceFileUpload.SubirArchivoDrive(fullPath);
            //return View("PdfNew", suministroCredito);
        }


        public IActionResult VerPdfNew(int idSuministro)
        {
            var detalle = _sumiCredRepository.GetById(idSuministro);
            return View("pdfNew", detalle);
        }


    }
}
