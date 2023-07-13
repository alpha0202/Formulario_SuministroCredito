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

namespace Formulario_SuministroCredito.Controllers
{
    public class SumiCredController : Controller
    {
        private readonly ISumiCredRepository _sumiCredRepository;
        private readonly IValidator<SuministroCredito> _validator;

        public SumiCredController(ISumiCredRepository sumiCredRepository, IValidator<SuministroCredito> validator)
        {
            _sumiCredRepository = sumiCredRepository;
            _validator = validator;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<ActionResult> Index()
        {
            var suministros = await _sumiCredRepository.GetAll();

           

            return View(suministros);
        }


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
            try
            {
                var contador = _sumiCredRepository.CountRowDb();

                var suministroCredito = new SuministroCredito()
                {
                    //Consecutivo = collection["Consecutivo"],
                    //FechaRegistro= DateTime.Now,
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
                    NumeroContador = contador
                    

                };
                //var response = contador;
                //ViewData["MiContador"] = response;

                SuministrosValidator validadorSumi = new SuministrosValidator();

                //var resValidate = await _validator.ValidateAsync(suministroCredito);
                var resValidate = await validadorSumi.ValidateAsync(suministroCredito);

                if (!resValidate.IsValid)
                {
                    resValidate.AddToModelState(this.ModelState);
                    return View("Insert",suministroCredito);
                }



                bool resultado = await _sumiCredRepository.Insert(suministroCredito);
                //var resAdjuntar = await _sumiCredRepository.UploadFile(suministroCredito.RutFile);

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        public IActionResult PdfNew(int id)
        {
            var contador = _sumiCredRepository.CountRowDb();
            var detalle =  _sumiCredRepository.GetById(id);
           
            var suministroCredito = new SuministroCredito()
            {
                NumeroContador = contador,
                FechaRegistro = detalle.FechaRegistro,
                Tipo_solicitud=detalle.Tipo_solicitud,
                Monto = detalle.Monto,
                Plazo = detalle.Plazo,
                Apellidos_nombres_razon_social = detalle.Apellidos_nombres_razon_social,
                Tipo_persona = detalle.Tipo_persona,
                Tipo_identificacion = detalle.Tipo_identificacion,
                Numero_identificacion = detalle.Numero_identificacion,
                DV = detalle.DV,
                Representante_legal = detalle.Representante_legal,
                Cargo = detalle.Cargo,
                Correo_electronico=detalle.Correo_electronico,
                Direccion_correspondencia = detalle.Direccion_correspondencia,
                Ciudad=detalle.Ciudad,
                Departamento=detalle.Departamento,
                Telefono=detalle.Telefono,
                Celular= detalle.Celular,
                Correo_electronico_facturacion=detalle.Correo_electronico_facturacion,
                Entidad_razon_social_ref_comerciales = detalle.Entidad_razon_social_ref_comerciales,
                Direccion_ref_comerciales=detalle.Direccion_ref_comerciales,
                Nom_contacto_ref_comerciales = detalle.Nom_contacto_ref_comerciales,
                Cargo_ref_comerciales = detalle.Cargo_ref_comerciales,
                Telefono_ref_comerciales = detalle.Telefono_ref_comerciales,
                Entidad_razon_social_ref_comerciales_dos = detalle.Entidad_razon_social_ref_comerciales_dos,
                Direccion_ref_comerciales_dos=detalle.Direccion_ref_comerciales_dos,
                Nom_contacto_ref_comerciales_dos = detalle.Nom_contacto_ref_comerciales_dos,
                Cargo_ref_comerciales_dos=detalle.Cargo_ref_comerciales_dos,
                Telefono_ref_comerciales_dos = detalle.Telefono_ref_comerciales_dos,
                Entidad_financiera = detalle.Entidad_financiera,
                Tipo_cuenta = detalle.Tipo_cuenta,
                Numero_cuenta = detalle.Numero_cuenta,
                Oficina=detalle.Oficina,
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
                Cargo_contacto_compras=detalle.Cargo_contacto_compras,
                Telefono_contacto_compras = detalle.Telefono_contacto_compras,
                Celular_contacto_compras = detalle.Celular_contacto_compras,
                Correo_electronico_contacto_compras = detalle.Correo_electronico_contacto_compras,
                Nombre_apellido_firma = detalle.Nombre_apellido_firma,
                Nro_cedula_firma =detalle.Nro_cedula_firma,
                Representa_legal_firma = detalle.Representa_legal_firma

            };


            return new ViewAsPdf("PdfNew", suministroCredito)

            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                FileName = "probandoPdf.pdf"
            };
        }
    


    }
}
