//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 13.5.2017
//
//  Projekt.......: learn.asp.mvc
//  Name..........: ClockGraph.cs
//  Aufgabe/Fkt...: Transfomationen von Zeitangaben in Geometrische Koorkdinaten
//                  einer graphischen Darstellung der Zeit
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timeline.UI.Web.Models.Clock
{
    public class ClockGraph
    {
        /// <summary>
        /// Berechnung der x, y - Koordinaten der Zeigerspitze in einer 
        /// Chronographen- Darstellung für einen Zeitpunkt
        /// </summary>
        /// <param name="r"></param>
        /// <param name="time"></param>
        /// <returns>XY- Koordinate der Zeigerspitze als Tuple</returns>
        public static Tuple<double, double> TimePointerTipXY(double r, double time, double centerX = 100.0, double centerY = 100.0)
        {

            // Position des Sekundenzeigers berechnen
            // 1 sec <=> 6°

            // Sekunden in Winkel umrechnen

            var SecDeg = 90.0 - time * 6.0;
            if (SecDeg < 0.0)
            {
                SecDeg = 360.0 + SecDeg;
            }

            // Winkel in Bogen
            var SecRad = SecDeg * Math.PI / 180.0;

            var SecX = r * Math.Cos(SecRad) + centerX;
            var SecY = centerY - r * Math.Sin(SecRad);

            return Tuple.Create(SecX, SecY);
        }        
    }
}