using System;

namespace DonateMe.DataLayer
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class InjectedAttribute : Attribute
    {
    }
}