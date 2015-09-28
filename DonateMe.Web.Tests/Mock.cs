using NSubstitute;

namespace DonateMe.Web.Tests
{
    public static class Mock  
    {
        public static T For<T>() where T : class
        {
            return Substitute.For<T>();
        }
    }
}