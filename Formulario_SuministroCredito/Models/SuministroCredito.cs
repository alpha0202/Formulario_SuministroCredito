using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Formulario_SuministroCredito.Models
{
    public class SuministroCredito
    {
        private DateTime _fechaRegistro;

        public int IdSuministro_Credito { get; set; }  
        public DateTime Fecha_registro { get { return _fechaRegistro; } set { _fechaRegistro = value; } }
        public string Tipo_solicitud { get; set; }
        public string Monto { get; set; }
        public string Plazo { get; set; }

        public string Apellidos_nombres_razon_social { get; set; }
        public string Tipo_persona { get; set; }
        public string Tipo_identificacion { get; set; }
        public string Numero_identificacion { get; set; }

        public string DV { get; set; }
        public string Representante_legal { get; set; }
        public string Cargo { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Email.")]
        [EmailAddress(ErrorMessage = "Dirección de correo incorrecto.")]
        public string Correo_electronico { get; set; }
        public string Direccion_correspondencia { get; set; }
        public string Ciudad { get; set; }
        public string Departamento { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Correo_electronico_facturacion { get; set; }

        public string Entidad_razon_social_ref_comerciales { get; set; }
        public string Direccion_ref_comerciales { get; set; }
        public string Nom_contacto_ref_comerciales { get; set; }
        public string Cargo_ref_comerciales { get; set; }
        public string Telefono_ref_comerciales { get; set; }

        public string Entidad_razon_social_ref_comerciales_dos { get; set; }
        public string Direccion_ref_comerciales_dos { get; set; }
        public string Nom_contacto_ref_comerciales_dos { get; set; }
        public string Cargo_ref_comerciales_dos { get; set; }
        public string Telefono_ref_comerciales_dos { get; set; }

        public string Entidad_financiera { get; set; }
        public string Tipo_cuenta { get; set; }
        public string Numero_cuenta { get; set; }
        public string Oficina { get; set; }

        public string Nombre_contacto_tesoreria { get; set; }
        public string Cargo_contacto_tesoreria { get; set; }
        public string Telefono_contacto_tesoreria { get; set; }
        public string Celular_contacto_tesoreria { get; set; }
        public string Correo_electronico_contacto_tesoreria { get; set; }
        public string Nombre_contacto_contabilidad { get; set; }
        public string Cargo_contacto_contabilidad { get; set; }
        public string Telefono_contacto_contabilidad { get; set; }
        public string Celular_contacto_contabilidad { get; set; }
        public string Correo_electronico_contacto_contabilidad { get; set; }
        public string Nombre_contacto_compras { get; set; }
        public string Cargo_contacto_compras { get; set; }
        public string Telefono_contacto_compras { get; set; }
        public string Celular_contacto_compras { get; set; }
        public string Correo_electronico_contacto_compras { get; set; }

        public string Ruta_rut { get; set; }
        public string Ruta_estado_financiero { get; set; }
        public string Ruta_existencia { get; set; }
        public string Ruta_cert_ingresos { get; set; }
        public string Ruta_tarjeta_profesional { get; set; }
        public string Ruta_cert_antecedentes { get; set; }
        public IFormFile RutFile { get; set; }
        public IFormFile EstadoFinancieroFile { get; set; }
        public IFormFile ExistenciaFile { get; set; }
        public IFormFile CertificadoIngresosFile { get; set; }
        public IFormFile TarjetaProfesionalFile { get; set; }
        public IFormFile CertificadoAntecedentesFile { get; set; }

        public string Nombre_apellido_firma { get; set; }
        public string Nro_cedula_firma { get; set; }
        public string Representa_legal_firma { get; set; }

        public string Centro_distribucion { get; set; }
        public string Cupo_sugerido { get; set; }
        public string Plazo_aliar { get; set; }
        public string Nom_asesor_comercial { get; set; }

        public string NumeroContador { get; set; }
        public string Fecha_solicitud { get; set; }




    }



}
