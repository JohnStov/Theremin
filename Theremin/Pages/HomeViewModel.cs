using System;
using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;

namespace Theremin.Pages
{
    public class HomeViewModel : ReactiveObject
    {
        public HomeViewModel()
        {
            var listener = ((App) (Application.Current)).Listener;

            ReloadListener = new ReactiveCommand();
            ReloadListener.Subscribe(_ => MessageBox.Show("Searching for LeapMotion"));

            listener.Frames.Timestamp().Buffer(2).Select(f =>
            {
                var span = f[1].Timestamp - f[0].Timestamp;
                return 1.0/span.TotalSeconds;
            }).ToProperty(this, x => x.FrameRate, out frameRate);

            listener.Frames.Select(f => f.Hands.Count).ToProperty(this, x => x.Hands, out hands);
        }

        private readonly ObservableAsPropertyHelper<double> frameRate;
        public double FrameRate { get { return frameRate.Value; } }

        private readonly ObservableAsPropertyHelper<int> hands;
        public int Hands { get { return hands.Value; } }

        public IReactiveCommand ReloadListener { get; private set; }
    }
}
