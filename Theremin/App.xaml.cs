using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using Leap;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace Theremin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private App()
        {
            var waveOut = new DirectSoundOut(60);

            var osc = new Oscillator(44100, Math.Sin);

            var controller = LeapController.GetController();
            var listener = controller.Listener;
            listener.Frames.Select(f => (f.Hands.Count > 0) ? f.Hands[0].PalmPosition.y : 0.0).Subscribe(osc.Frequency);
            
            waveOut.Init(new StreamProvider(osc));
            waveOut.Play();
        }
    }

    internal class StreamProvider : WaveProvider32
    {
        private readonly Oscillator osc;

        public StreamProvider(Oscillator osc)
            : base(44100, 1)
        {
            this.osc = osc;
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            if (osc != null)
            {
                var data = osc.Data.Select(x => (float) x).Take(sampleCount).ToArray();
                int index = 0;
                foreach (var sample in data)
                {
                    buffer[offset + index++] = sample;
                }
            }

            return sampleCount;
        }
    }
}
