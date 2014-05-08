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

            listener.Frames.Timestamp().Buffer(2).Subscribe(f =>
            {
                var span = f[1].Timestamp - f[0].Timestamp;
                FrameRate = 1.0/span.TotalSeconds;

                Hands = f[0].Value.Hands.Count;
            });
        }

        private double frameRate;
        public double FrameRate 
        { 
            get { return frameRate; } 
            private set { this.RaiseAndSetIfChanged(ref frameRate, value); } 
        }

        private int hands;
        public int Hands
        {
            get { return hands; }
            private set { this.RaiseAndSetIfChanged(ref hands, value); }
        }
    }
}
