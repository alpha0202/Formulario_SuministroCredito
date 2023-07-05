using FluentValidation;
using Formulario_SuministroCredito.Models;

namespace Formulario_SuministroCredito.Validator
{
    public class SuministrosValidator : AbstractValidator<SuministroCredito>
    {

        public SuministrosValidator()   
        {
                RuleFor(x => x.Apellidos_nombres_razon_social).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("Debe escribir nombres y apellidos o la razón social.");
                RuleFor(x => x.Tipo_persona).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("Seleccione tipo persona.");
                RuleFor(x => x.Tipo_identificacion).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("Seleccione tipo identificación.");
                RuleFor(x => x.Numero_identificacion).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("Falta el número de identificación.");
                RuleFor(x => x.DV).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("Falta el dígito de verificación.");
                RuleFor(x => x.Representante_legal).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("Digite el representante legal.");
                RuleFor(x => x.Cargo).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("Digite el cargo.");
                RuleFor(x => x.Correo_electronico).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("Falta email del representante").EmailAddress().WithMessage("email inválido.");
        
        }   


    }
}
