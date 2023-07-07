// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})


//habilitar las secciones del representante legal
const tipo_persona = document.querySelector("#tipoPersona");
const seccion_Legal = document.querySelector("#seccionLegal");
const seccion_firma_legal = document.querySelector("#seccion_firma_legal")

tipo_persona.addEventListener("change", () => {
    if (tipo_persona.value === "Jurídica") {
        seccion_Legal.classList.remove("invisible");
        seccion_firma_legal.classList.remove("invisible");
        seccion_Legal.classList.add("required");
        seccion_firma_legal.classList.add("required");
    } else {
        seccion_Legal.classList.add("invisible");
        seccion_firma_legal.classList.add("invisible");
        seccion_Legal.classList.remove("required");
        seccion_firma_legal.classList.remove("required");
    }
});



//habilitar seccion de anexar documentos para personal no obligado.
const seccionNoObligados = document.querySelector("#seccion_no_obligados");
const seccionClass_NoObligados = document.querySelector("#no_obligatorios");


tipo_persona.addEventListener("change", () => {
    if (tipo_persona.value ==="Jurídica") {
        seccionNoObligados.classList.add("invisible")
        seccionClass_NoObligados.classList.add("invisible")
    } else {
        seccionNoObligados.classList.remove("invisible")
        seccionClass_NoObligados.classList.remove("invisible")
    }
});






//copia representante legal, al campo representate legal de la sección firma.
document.getElementById("repLegalName").addEventListener('keyup', autoCompleteNew);

function autoCompleteNew(e) {
    
    var value = $(this).val();
    $("#representaLegal_firma").val(value);

}









// Example starter JavaScript for disabling form submissions if there are invalid fields
(() => {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    const forms = document.querySelectorAll('.validarForm')

    // Loop over them and prevent submission
    Array.from(forms).forEach(form => {
        form.addEventListener('submit', event => {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
            }

            form.classList.add('was-validated')
        }, false)
    })
})()






//$("#consecutivo").val('2');

//$(function() {
//    $('#datepicker').datepicker();
//});


//var fecha = new Date();
//document.getElementById("fecha_reg").value = fecha.toJSON().slice(0, 10);


//function inicio() {
//    document.suministros.consec.consecutivo.value = 1
//};
//window.onload = inicio;



//let contador = 1;
//function generarNumero() {
//    contador++;
//    document.getElementById('consecutivo') == contador;
//    console.log(contador);
//}
//window.onload = generarNumero;