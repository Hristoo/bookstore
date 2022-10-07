using System.Globalization;

namespace BookStore2.MiddleWare
{
    public class CustomExeptioHendler : Exception
    {

        public CustomExeptioHendler() : base() { }

        public CustomExeptioHendler(string message) : base(message) { }

        public CustomExeptioHendler(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
