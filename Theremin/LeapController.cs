using System;
using Leap;

namespace Theremin
{
    public class LeapController : ILeapListener
    {
        public IObservable<Frame> Frames { get; private set; }
        public bool LeapControllerAvailable { get; private set; }
    }
}