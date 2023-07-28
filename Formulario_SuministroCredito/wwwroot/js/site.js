
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
        document.querySelector('#estados_file').required = true;
        document.querySelector('#existencias_file').required = true;
        document.querySelector('#repLegalName').required = true;
        document.querySelector('#cargoRepLegal').required = true;
        document.querySelector('#email_reprLegal').required = true;

    }
    else {
        seccion_Legal.classList.add("invisible");
        seccion_firma_legal.classList.add("invisible");
        seccion_Legal.classList.remove("required");
        seccion_firma_legal.classList.remove("required");
        document.querySelector('#estados_file').required = false;
        document.querySelector('#existencias_file').required = false;
        document.querySelector('#repLegalName').required = false;
        document.querySelector('#cargoRepLegal').required = false;
        document.querySelector('#email_reprLegal').required = false;
    }
    if (tipo_persona.value === "Natural") {
        document.querySelector('#certIngr_file').required = true;
        document.querySelector('#tarjPro_file').required = true;
        document.querySelector('#antec_file').required = true;
        document.querySelector('#estadoFinanciero').classList.add("invisible");
        document.querySelector('#certificadoExistencia').classList.add("invisible");
    }
    else {

        document.querySelector('#certIngr_file').required = false;
        document.querySelector('#tarjPro_file').required = false;
        document.querySelector('#antec_file').required = false;
        document.querySelector('#estadoFinanciero').classList.remove("invisible");
        document.querySelector('#certificadoExistencia').classList.remove("invisible");
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


const botonGenerar = document.querySelector("#generarPdf");
const botonGuardaSolicitud = document.querySelector("#guardaSolicitud");
const filaGuardar_solicitud = document.querySelector("#filaGuarda_Solicitud");

botonGenerar.addEventListener("click", () => {
    botonGuardaSolicitud.removeAttribute("disabled")
    botonGenerar.classList.add("disabled")
    //filaGuardar_solicitud.classList.remove("invisible")

   
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
    const rowGuardar_solicitud = document.querySelector("#filaGuarda_Solicitud");

    // Loop over them and prevent submission
    Array.from(forms).forEach(form => {
        form.addEventListener('submit', event => {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
                botonGenerar.classList.remove("disabled")

            }
            else {
                botonGenerar.classList.add("disabled")
                botonGenerar.classList.add("invisible")
                rowGuardar_solicitud.classList.remove("invisible")
            }
            form.classList.add('was-validated')
        }, false)
    })
})()






















//const alertaEmail = document.querySelectorAll('.alertEmail')
//const form = document.getElementById("formSuministros");
//const email = document.getElementById("email_reprLegal");
//const emailFact = document.getElementById("email_facturaElc");
//const alertSuccess = document.getElementById("alertSuccess");
//const alertEmail = document.getElementById("alertEmail");
//const alertEmail2 = document.getElementById("alertEmail2");
//const regUserEmail = /^[a-z0-9]+(\.[_a-z0-9]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,15})$/;

//const pintarMensajeExito = () => {
//    alertSuccess.classList.remove("d-none");
//    alertSuccess.textContent = "Solicitud enviado con éxito";
//};

//const pintarMensajeError = (errores) => {
//    errores.forEach((item) => {
//        item.tipo.classList.remove("d-none");
//        item.tipo.textContent = item.msg;
//    });
//};



//form.addEventListener('submit', e => {
//    e.preventDefault();


//    alertSuccess.classList.add("d-none");
//    const errores = [];


//    // validar email
//    if (!regUserEmail.test(email.value) || !regUserEmail.test(emailFact.value) || !email.value.trim() || !emailFact.value.trim())  {
//        email.classList.add("is-invalid");
//        emailFact.classList.add("is-invalid");

//        errores.push({
//            tipo: alertEmail,
//            msg: "Escriba un correo válido",
//        });
//    } else {
//        email.classList.remove("is-invalid");
//        email.classList.add("is-valid");
//        emailFact.classList.remove("is-invalid");
//        emailFact.classList.add("is-valid");
//        alertEmail.classList.add("d-none");
//        alertEmail2.classList.add("d-none");
//    }

//    if (errores.length !== 0) {
//        pintarMensajeError(errores);
//        return;
//    }

//    console.log("Formulario enviado con éxito");
//    pintarMensajeExito();


//});






//form.addEventListener('submit', e => {
//    e.preventDefault();

//    checkInputs();
//});































//function checkInputs() {
//    // trim to remove the whitespaces
//    //const usernameValue = username.value.trim();
//    //const passwordValue = password.value.trim();
//    //const password2Value = password2.value.trim();
//    const emailValue = email.value.trim();

//    //if (usernameValue === '') {
//    //    setErrorFor(username, 'Username cannot be blank');
//    //} else {
//    //    setSuccessFor(username);
//    //}

//    if (emailValue === '') {
//        setErrorFor(email, 'Email no puedes estar vacío.');
//    } else if (!isEmail(emailValue)) {
//        setErrorFor(email, 'Not a valid email');
//    } else {
//        setSuccessFor(email);
//    }


//    //if (passwordValue === '') {
//    //    setErrorFor(password, 'Password cannot be blank');
//    //} else {
//    //    setSuccessFor(password);
//    //}

//    //if (password2Value === '') {
//    //    setErrorFor(password2, 'Password2 cannot be blank');
//    //} else if (passwordValue !== password2Value) {
//    //    setErrorFor(password2, 'Passwords does not match');
//    //} else {
//    //    setSuccessFor(password2);
//    //}
//}

//function setErrorFor(input, message) {
//	const formControl = input.parentElement;
//	const small = formControl.querySelector('small');
//    formControl.className = 'form-control-rl error';
//	small.innerText = message;
//}

//function setSuccessFor(input) {
//	const formControl = input.parentElement;
//    formControl.className = 'form-control-rl success';
//}



//function isEmail(email) {
//    return /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(email);
//}






////extraer departamentes y ciudades COL
//function loadJSON(callback) {
//    var xobj = new XMLHttpRequest();
//    xobj.overrideMimeType("application/json");
//    xobj.open("GET", 'https://raw.githubusercontent.com/marcovega/colombia-json/master/colombia.min.json', true); // Reemplaza colombia-json.json con el nombre que le hayas puesto
//    xobj.onreadystatechange = function () {
//        if (xobj.readyState == 4 && xobj.status == "200") {
//            callback(xobj.responseText);
//        }
//    };
//    xobj.send(null);
//}





//(() => {
//    loadJSON(function (response) {
//        // Parse JSON string into object
//        var JSONFinal = JSON.parse(response);
//        var departamento = JSONFinal[0].departamento;
//        var ciudades = JSONFinal[0].ciudades;
//        console.log("Departamento: " + departamento);
//        console.log("Ciudades: " + ciudades);


//    });
//})();



//const request = new XMLHttpRequest();
//request.open('GET', 'https://raw.githubusercontent.com/marcovega/colombia-json/master/colombia.min.json', true);
//request.onload = function () {
//    if (this.status === 200) {
//        const data = JSON.parse(this.responseText);
//        let options = '';
//        for (let i = 0; i < data.length; i++) {
//            options += '<option value="' + data[i].departamento + '">' + data[i].departamento + '</option>';
//        }
//        document.getElementById('departamentos').innerHTML = options;
//    }
//};
//request.send();


//const request = new XMLHttpRequest();
//request.open('GET', 'https://raw.githubusercontent.com/marcovega/colombia-json/master/colombia.min.json', true);
//request.onload = function () {
//    if (this.status === 200) {
//        const data = JSON.parse(this.responseText);
//        let options = data.map(function (departamento) {
//            return '<option value="' + departamento.departamento + '">' + departamento.departamento + '</option>';
//        });
//        document.getElementById('departamentos').innerHTML = options.join('');
//    }
//};
//request.send();

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