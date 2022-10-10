using System.Globalization;

namespace BookStore2.MiddleWare
{
    public class AppExeption : Exception
    {
        public AppExeption() : base() { }

        public AppExeption(string message) : base(message) { }

        public AppExeption(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }

    }
}
