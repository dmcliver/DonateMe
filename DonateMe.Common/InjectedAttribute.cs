using System;

namespace DonateMe.Common
{
    /// <summary>
    /// Marker attribute used by autofac framework to notify it's container that the classes implementing this are to be injected.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class InjectedAttribute : Attribute
    {
    }
}