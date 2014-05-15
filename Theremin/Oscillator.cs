using System;
using System.Collections.Generic;
using System.Reactive;

namespace Theremin
{
    public class Oscillator
    {
        public IEnumerable<double> Data { get; private set; }

        private double delta;
        private readonly int sampleRate;
        private double phase;

        public IObserver<double> Frequency { get; private set; }

        public Oscillator(int sampleRate, Func<double, double> generator)
        {
            this.sampleRate = sampleRate;
            Data = Samples(generator);

            Frequency = new AnonymousObserver<double>(SetFrequency);
        }

        private void SetFrequency(double frequency)
        {
            delta = 2.0 * Math.PI * frequency / sampleRate;
        }

        private IEnumerable<double> Samples(Func<double, double> generator)
        {
            while (true)
            {
                yield return generator(phase);
                phase = (phase + delta)%(2.0*Math.PI);
            }
        }
    }
}
