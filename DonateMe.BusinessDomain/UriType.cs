using System;

namespace DonateMe.BusinessDomain
{
    public class UriType
    {
        private readonly Uri _uri;

        public UriType(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
            Path = path;

            _uri = new Uri(Path, UriKind.RelativeOrAbsolute);
        }

        protected UriType()
        {
        }

        public string Path { get; private set; }

        public Uri GetUri()
        {
            return _uri;
        }
    }
}
