using Dapper;
using Formulario_SuministroCredito.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Formulario_SuministroCredito.Data
{
    public class SumiCredRepository : ISumiCredRepository
    {
        private readonly IDbConnection _dbconnection;

        public SumiCredRepository(IDbConnection dbConnection)
        {
                _dbconnection = dbConnection;
        }

      


        public async Task<IEnumerable<SuministroCredito>> GetAll()
        {
            var sql = @"select * from Suministro_Credito";

             var resultado =  await _dbconnection.QueryAsync<SuministroCredito>(sql, new { });
            return resultado;
        }


        string ISumiCredRepository.CountRowDb()
        {
            var sql = @"select count(*) from n97eed5_MaestrosProcesos.Suministro_Credito";

            var count = _dbconnection.ExecuteScalar(sql);
            return (string)count;
        }


        //public Task<SuministroCredito> GetDatail(int id)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<bool> Insert(SuministroCredito suministroCredito)
        {

            AdjuntarArchivos(suministroCredito.RutFile);



            try
            {
                var sql = @"INSERT INTO Suministro_Credito(
                                                        consecutivo, 
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
                                                        nom_asesor_comercial)                                          
                                                  VALUES
                                                           (@Consecutivo, 
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
                                                            'null',
                                                            'null',
                                                            'null',
                                                            'null',
                                                            'null',
                                                            'null',
                                                            @Nombre_apellido_firma,
                                                            @Nro_cedula_firma,
                                                            @Representante_legal,
                                                            @Centro_distribucion,
                                                            @Cupo_sugerido,
                                                            @Plazo_aliar,
                                                            @Nom_asesor_comercial)";


                await _dbconnection.ExecuteAsync(sql, new
                {
                    suministroCredito.Consecutivo,
                    suministroCredito.FechaRegistro,
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
                    suministroCredito.Nom_asesor_comercial


                });

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
                return false;
            }



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

        [NonAction]
        public  bool AdjuntarArchivos(IFormFile file)
        {
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        file.CopyToAsync(fileStream);
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


        //public async Task<IActionResult> Upload(IFormFile file)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        var fileName = Path.GetFileName(file.FileName);
        //        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ruta/especifica", fileName);
        //        using (var stream = new FileStream(path, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }
        //        ViewBag.Message = "Archivo guardado correctamente.";
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Error al guardar el archivo.";
        //    }
        //    return View();
        //}

    
}
