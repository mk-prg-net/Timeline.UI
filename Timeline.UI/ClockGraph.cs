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

        /// <summary>
        /// Mittelpunkt des Ziffernblattes in Bezug zur linken, oberen Bildecke
        /// </summary>
        public Vector2 CenterPoint
        {
            get;
        }      
          
        /// <summary>
        /// Durchmesser des Ziffernblattes
        /// </summary>
        public float Radius
        {
            get;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="centerPoint"></param>
        /// <param name="diameter"></param>
        public ClockGraph(Vector2 centerPoint, float diameter)
        {
            CenterPoint = centerPoint;
            Radius = diameter / 2.0f;

            var PointerLenght = Radius * 0.95f;
            

            SecoundHand = new Vector2[] {
                new Vector2(),
                new Vector2(1.0f * PointerLenght, 0)
            };

            MinuteHand = new Vector2[] {
                new Vector2(),
                new Vector2(0.8f * PointerLenght, 0)
            };


            HourHand = new Vector2[] {
                new Vector2(),
                new Vector2(0.6f * PointerLenght, 0)
            };


            ScaleGraduation = new Vector2[] {
                new Vector2(PointerLenght, 0),
                new Vector2(Radius, 0)
            };
        }

        /// <summary>
        /// Silhouette des Sekundenzeigers
        /// </summary>
        public IEnumerable<Vector2> SecoundHand
        {
            get;
        }

        /// <summary>
        /// Silhouette des Strundenzeigers
        /// </summary>
        public IEnumerable<Vector2> HourHand
        {
            get;
        }

        /// <summary>
        /// Berechnet die Position des Stundenzeigers auf einem 60 Minuten Kreis
        /// für eine gegeben Uhrzeit
        /// </summary>
        /// <param name="Hour"></param>
        /// <param name="Minutes"></param>        
        /// <returns></returns>
        public int HourHandPos60(int Hour, int Minutes)
        {
            int pos = Hour > 12 ? (Hour - 12) * 5 : Hour * 5;
            return pos + Minutes/12 ;

        }

        /// <summary>
        /// Silhouette des Minutenzeigers
        /// </summary>
        public IEnumerable<Vector2> MinuteHand
        {
            get;
        }

        /// <summary>
        /// Silhouette einer Skalenteilung
        /// </summary>
        public IEnumerable<Vector2> ScaleGraduation
        {
            get;
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
            // Eine Spiegelung an der x- Achse wurde eingebaut, da die y- Achsen der  Koordinatensysteme im 
            // Computer nach unten zeigen, und damit entgegen der Mathematik, in der sie nach oben zeigen.
            var mat = Matrix3x2.Multiply(Matrix3x2.Multiply(Matrix3x2.CreateRotation(Rad), Matrix3x2.CreateScale(1.0f, -1.0f)), Matrix3x2.CreateTranslation(CenterPoint));

            List<Vector2> transformed = new List<Vector2>(TimePointerShape.Count());

            foreach (var vec in TimePointerShape)
            {
                transformed.Add(Vector2.Transform(vec, mat));
            }

            return transformed.ToArray();
        }





    }
}
