﻿using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Leap;

namespace Theremin
{
    public interface ILeapListener
    {
        IObservable<Frame> Frames { get; }
        IObservable<bool> Connected { get; }
    }

    class LeapListener : Listener, ILeapListener
    {
        private readonly ISubject<Frame> frameSubject = new Subject<Frame>();
        private readonly ISubject<bool> connectedSubject = new BehaviorSubject<bool>(false);
        public IObservable<Frame> Frames { get; private set; }
        public IObservable<bool> Connected { get; private set; }

        public LeapListener()
        {
            Frames = frameSubject;
            Connected = connectedSubject;
        }

        public override void OnInit(Controller controller)
        {
            controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
            controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
            controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
            controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
            controller.SetPolicyFlags(Controller.PolicyFlag.POLICYBACKGROUNDFRAMES);

            connectedSubject.OnNext(controller.IsConnected);
        }

        public override void OnConnect(Controller controller)
        {
            connectedSubject.OnNext(controller.IsConnected);
        }

        public override void OnDisconnect(Controller controller)
        {
            connectedSubject.OnNext(controller.IsConnected);
        }

        public override void OnFrame(Controller controller)
        {
            frameSubject.OnNext(controller.Frame());
        }
    }
}
