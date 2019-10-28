/****************************************************************
 * Objeto Personas que contiene la logica para la vista personas 
*****************************************************************/

var Personas = function () {

    return {

        tipoDocumento: null,
        numeroDocumento: null,
        primerNombre: null,
        segundonombre: null,
        primerApellido: null,
        segundoApellido: null,
        sexo: null,
        fechaNacimiento: null,

        // Funcion de inicio del objeto javascript de la vista 
        start: function () {
        },

        // Función para validar el contenido del formulario
        validateForm: function () {
            if ($("#NumeroDocumento").val() == "") {
                alertify.alert('OOPS!', 'Por favor registra el campo tipo de documento', function () { });
                $("#NumeroDocumento").focus();
                return false;
            }
            if ($("#PrimerNombre").val() == "") {
                alertify.alert('OOPS!', 'Por favor registra el campo tipo de primer nombre', function () { });
                $("#PrimerNombre").focus();
                return false;
            }
            if ($("#PrimerApellido").val() == "") {
                alertify.alert('OOPS!', 'Por favor registra el campo primer apellido', function () { });
                $("#PrimerApellido").focus();
                return false;
            }  

            Personas.setValues();
        },

        // Función para asignar los valores del formulario antes de enviarlos 
        setValues: function () {
            this.tipoDocumento = $("#TipoDocumento").val();
            this.numeroDocumento = $("#NumeroDocumento").val();
            this.primerNombre = $("#PrimerNombre").val();
            this.segundonombre = $("#SegundoNombre").val();
            this.primerApellido = $("#PrimerApellido").val();
            this.segundoApellido = $("#SegundoApellido").val();
            this.sexo = $("#Sexo").val();
            this.fechaNacimiento = $("#FechaNacimiento").val();

            Personas.sendForm();
        },

        // Función para enviar el la informacion del formulario
        sendForm: function () {

            var serviceURL = '/Personas/SavePersonas';

            $.ajax({
                type: "POST",
                url: serviceURL,
                cache: true,
                async: false,
                data: JSON.stringify({ TipoDocumento: this.tipoDocumento, NumeroDocumento: this.numeroDocumento, PrimerNombre: this.primerNombre, SegundoNombre: this.segundonombre, PrimerApellido: this.primerApellido, SegundoApellido: this.segundoApellido, Sexo: this.sexo, FechaNacimiento: this.fechaNacimiento }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: successFunc,
                error: errorFunc
            });

            function successFunc(data, status) {
                switch (data.Code)
                {
                    case 23505:
                        alertify.alert('OOPS!', data.Message, function () { alertify.error('Lo sentimos'); });
                        break;
                    case 200:
                        alertify.alert('Genial!', data.Message, function () { alertify.success('Correcto'); });
                        break;
                    default: break;
                }            
            }

            function errorFunc() {
                alertify.alert('OOPS!', 'ocurrió un error! :(', function () { alertify.error('Lo sentimos'); });
            }  
        }


    }

}();