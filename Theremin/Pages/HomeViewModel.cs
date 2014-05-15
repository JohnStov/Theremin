using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;

namespace Theremin.Pages
{
    public class HomeViewModel : ReactiveObject
    {
        public HomeViewModel()
        {
            var controller = LeapController.GetController();

            var listener = controller.Listener;

            listener.Frames.Timestamp().Buffer(2).Select(f =>
            {
                var span = f[1].Timestamp - f[0].Timestamp;
                return 1.0/span.TotalSeconds;
            }).ToProperty(this, x => x.FrameRate, out frameRate);

            listener.Frames.Select(f => f.Hands.Any() ? f.Hands[0].PalmPosition.y : 0.0f)
                .ToProperty(this, x => x.HandHeight, out handHeight);

            listener.Connected.Select(f => f ? "Connected" : "Disconnected")
                .ToProperty(this, x => x.Connected, out connected);
        }

        private readonly ObservableAsPropertyHelper<double> frameRate;
        public double FrameRate { get { return frameRate.Value; } }

        private readonly ObservableAsPropertyHelper<float> handHeight;
        public float HandHeight { get { return handHeight.Value; } }

        private readonly ObservableAsPropertyHelper<string> connected;
        public string Connected { get { return connected.Value; } }

    }
}
