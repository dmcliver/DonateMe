using NSubstitute;

namespace TestCommon
{
    public static class Mock  
    {
        public static T Instantiate<T>() where T : class
        {
            return Substitute.For<T>();
        }
    }
}