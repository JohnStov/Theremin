using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;

namespace Theremin
{
    public class Oscillator
    {
        public IEnumerable<double> Data { get; private set; }

        private double delta;
        private double frequency = 440;
        private double phase = 0.0;

        public Oscillator(int sampleRate, Func<double, double> generator)
        {
            delta = 2.0*Math.PI*frequency/sampleRate;
            Data = Samples(generator);
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
