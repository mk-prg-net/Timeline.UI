//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 25.6.2017
//
//  Projekt.......: Timeline.UI
//  Name..........: ClockGraph.cs
//  Aufgabe/Fkt...: Graphische Modellierung einer Zeitanzeige
//                  
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
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Timeline.UI
{
    public class ClockGraph
    {

        public enum TimePointerType
        {
            ScoundHand,
            MinuteHand,
            HourHand
        }


        Vector2 CenterPoint;

        public ClockGraph(Vector2 centerPoint)
        {
            CenterPoint = centerPoint;
        }

        /// <summary>
        /// Transformiert die Grundform eines Zeitzeigers so, dass die gewünschte Uhrzeit dargestellt wird
        /// </summary>
        /// <param name="time60">Position des Zeigers auf einer 60-er Teilung (Kreis in 60 Segmente aufgeteilt)</param>
        /// <param name="TimePointerShape">Liste aus Vektoren, welche den Unmriss des Zeigers beschreiben</param>
        /// <param name="center">Mittelpunkt des Ziffernblattes</param>
        /// <returns></returns>
        public Vector2[] SetTimePointer60(int time60, IEnumerable<Vector2> TimePointerShape)
        {
            mko.TraceHlp.ThrowArgExIfNot(0 <= time60 && time60 <= 60, "Zeigerposition ungültig");

            // Position des Sekundenzeigers berechnen
            // 1 sec <=> 6°

            // Sekunden in Winkel umrechnen

            var Deg = 90.0f - time60 * 6.0f;
            if (Deg < 0.0)
            {
                Deg = 360.0f + Deg;
            }

            // Winkel in Bogen
            var Rad = Deg * (float)Math.PI / 180.0f;


            // Koordinatentransformation als Produkt aus einer Dreh- und einer Verscheibematrix
            var mat = Matrix3x2.Multiply(Matrix3x2.CreateRotation(Rad), Matrix3x2.CreateTranslation(CenterPoint));

            List<Vector2> transformed = new List<Vector2>(TimePointerShape.Count());

            foreach (var vec in TimePointerShape)
            {
                transformed.Add(Vector2.Transform(vec, mat));
            }

            return transformed.ToArray();
        }
    }
}
