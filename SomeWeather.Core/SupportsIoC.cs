using System;

namespace SomeWeather.Core
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class SupportsIoC : Attribute
    {
    }
}