using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueHome.Entities.Exceptions
{
    public class ExistingScheduleException : Exception
    {
        public ExistingScheduleException() : base() { }
        public ExistingScheduleException(string message)
        : base(message) { }

        public ExistingScheduleException(string format, params object[] args)
        : base(string.Format(format, args)) { }

        public ExistingScheduleException(string message, Exception innerException)
        : base(message, innerException) { }

        public ExistingScheduleException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException) { }

        public static string BASE_ERROR_MESSAGE = "Horario no disponible";
    }
}
