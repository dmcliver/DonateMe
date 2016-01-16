using NSubstitute;

namespace TestCommon
{
    public static class Mock  
    {
        public static T It<T>() where T : class
        {
            return Substitute.For<T>();
        }
    }
}