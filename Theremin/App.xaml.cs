using System.Windows;
using Leap;

namespace Theremin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ILeapListener Listener { get; private set; }

        private readonly Controller controller;

        private App()
        {
            var listener = new LeapListener();
            controller = new Controller(listener);
            Listener = listener;
        }
    }
}
