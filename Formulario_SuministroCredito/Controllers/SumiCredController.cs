using FluentValidation;
using FluentValidation.AspNetCore;
using Formulario_SuministroCredito.Data;
using Formulario_SuministroCredito.Models;
using Formulario_SuministroCredito.Validator;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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


        // GET: SumiCredController/Create
        public ActionResult Insert()
        {
            //var contador = _sumiCredRepository.CountRowDb();

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





    }
}
