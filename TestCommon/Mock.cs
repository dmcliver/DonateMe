using NSubstitute;

namespace TestCommon
{
    public static class Mock  
    {
        public static T For<T>() where T : class
        {
            return Substitute.For<T>();
        }
    }
}