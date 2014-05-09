using System;
using System.Reactive.Subjects;
using Leap;

namespace Theremin
{
    public interface ILeapListener
    {
        IObservable<Frame> Frames { get; }

        bool LeapControllerAvailable { get; }
    }

    class LeapListener : Listener, ILeapListener
    {
        private readonly Subject<Frame> frameSubject = new Subject<Frame>();
        public IObservable<Frame> Frames { get { return frameSubject; } }
        public bool LeapControllerAvailable { get; private set; }

        public override void OnInit(Controller controller)
        {
            LeapControllerAvailable = controller.IsConnected;

            controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
            controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
            controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
            controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
            controller.SetPolicyFlags(Controller.PolicyFlag.POLICYBACKGROUNDFRAMES);
        }

        public override void OnFrame(Controller controller)
        {
            frameSubject.OnNext(controller.Frame());
        }
    }
}
