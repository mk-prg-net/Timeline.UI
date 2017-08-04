using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Timeline
{
    public enum AppointmentCategory
    {
        // astronomisch bedeutsames Datum
        astronomical,

        // Klasse der geschäftlichen Termine
        business,

        // kirchlicher Feiertag
        church_festival,

        // Feiertag, Ferien
        holiday,

        // Nationalfeiertag
        national_day,

        // Gedenktag
        memorial_day,

        // Klasse der privaten Termine
        @private,
    }
}
