﻿$(document).ready(main);

function main() {
    //$('.bt-menu').on('click', function () {
    //    $('.contenido').toggleClass('abrir-contenido');

    //    $('.barra-lateral').toggleClass('abrir-barralateral');
    //});

    //Mostramos y ocultamos submenus
    $('.submenu').click(function () {
        $(this).children('.children').slideToggle();
    });

    $(".bt-menu").on('click',function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("active");
    });

    $("#click-registro-prueba").on('click', function (e) {
        e.preventDefault();
        $(".subtable").toggleClass("mostrar-tabla");
    });
}



//var contador = 1;
//function main() {
//    $('.bt-menu').click(function () {
//        if (contador == 1) {
//            $('.barra-lateral').animate({
//                left: '0'
//            });
//            contador = 0;
//        } else {
//            contador = 1;
//            $('.barra-lateral').animate({
//                left: '-100%'
//            });
//        }
//    });

//     Mostramos y ocultamos submenus
//    $('.submenu').click(function () {
//        $(this).children('.children').slideToggle();
//    });
//}
