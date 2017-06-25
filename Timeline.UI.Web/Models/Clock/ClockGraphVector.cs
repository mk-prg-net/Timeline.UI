using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Numerics;

namespace Timeline.UI.Web.Models.Clock
{
    public class ClockGraphVector
    {

        public enum TimePointerType
        {
            ScoundHand,
            MinuteHand,
            HourHand
        }

        /// <summary>
        /// Transformiert die Grundform eines Zeitzeigers so, dass die gewünschte Uhrzeit dargestellt wird
        /// </summary>
        /// <param name="time60">Position des Zeigers auf einer 60-er Teilung (Kreis in 60 Segmente aufgeteilt)</param>
        /// <param name="TimePointerShape">Liste aus Vektoren, welche den Unmriss des Zeigers beschreiben</param>
        /// <param name="center">Mittelpunkt des Ziffernblattes</param>
        /// <returns></returns>
        public Vector2[] SetTimePointer60(int time60, IEnumerable<Vector2> TimePointerShape, Vector2 center)
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
            var mat =Matrix3x2.Multiply(Matrix3x2.CreateRotation(Rad), Matrix3x2.CreateTranslation(center));

            List<Vector2> transformed = new List<Vector2>(TimePointerShape.Count());

            foreach(var vec in TimePointerShape)
            {
                transformed.Add(Vector2.Transform(vec, mat));
            }

            return transformed.ToArray();
        }
    }
}