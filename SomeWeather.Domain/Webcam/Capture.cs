using System;
using System.Collections.Generic;

namespace SomeWeather.Domain.Webcam
{
    public class Capture : Entity
    {
        public DateTime Captured { get; set; }

        public string Caption { get; set; }
    }

    public class CaptureSet : Entity
    {
        public string Title { get; set; }

        public IEnumerable<Capture> Captures { get; set; }
    }
}