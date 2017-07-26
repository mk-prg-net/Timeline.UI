//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 26.7.2017
//
//  Projekt.......: Projektkontext
//  Name..........: Dateiname
//  Aufgabe/Fkt...: Synchronisieren eines Date- Input Elements mit den
//                  hidden Fields
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

$(document).ready(function () {

    $("input[type='date']").change(function () {
        let name = $(this).attr("id");
        let val = $(this).val();

        let parts = val.split('-');

        $("input[type='hidden'][id='" + name + ".Year']").attr("value", parts[0]);        
        $("input[type='hidden'][id='" + name + ".Month']").attr("value", parts[1]);
        $("input[type='hidden'][id='" + name + ".Day']").attr("value", parts[2]);

    });

    $("input[type='time']").change(function () {
        let name = $(this).attr("id");
        let val = $(this).val();

        let parts = val.split(':');

        $("input[type='hidden'][id='" + name + ".Hour']").attr("value", parts[0]);
        $("input[type='hidden'][id='" + name + ".Minute']").attr("value", parts[1]);
        $("input[type='hidden'][id='" + name + ".Second']").attr("value", parts[2]);

    });


});