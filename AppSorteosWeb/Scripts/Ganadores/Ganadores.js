/****************************************************************
 * Objeto Ganadores que contiene la logica para la vista Ganadores 
*****************************************************************/

var Ganadores = function () {

    return {        

        // Funcion de inicio del objeto javascript de la vista 
        start: function () {

        },

        showMenssage: function ()
        {
            alertify.alert('Genial!', 'Haz activado la loteria', function () { alertify.success('Loteria completada'); });
        },

        generateLottery: function ()
        {
            var serviceURL = '/Ganadores/GenerateLottery';

            $.ajax({
                type: "POST",
                url: serviceURL,
                cache: true,
                async: false,
                contentType: "application/json; charset=utf-8",
                success: successFunc,
                error: errorFunc
            });

            function successFunc(data, status) {
                switch (data.Code) {
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