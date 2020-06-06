using System;
using System.Threading;

namespace WebServer
{
    /// <summary>
    /// <c>TimerComponent</c>
    /// 
    /// Simple component that will call provided <c>Action</c> with parmeters each second for 1 minute total
    /// </summary>
    public class TimerComponent
    {
        /// <summary>
        /// The timer
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// The automatic reset event
        /// </summary>
        private AutoResetEvent _autoResetEvent;

        /// <summary>
        /// The actual action that will be called each second
        /// </summary>
        private Action<string, string> _action;

        /// <summary>
        /// Gets the timer started.
        /// </summary>
        /// <value>
        /// The timer started.
        /// </value>
        public DateTime TimerStarted { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerComponent"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public TimerComponent(Action<string, string> action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, 1000, 2000);
            TimerStarted = DateTime.Now;
        }

        /// <summary>
        /// Callback function for Timer 'elapse' event
        /// </summary>
        /// <param name="stateInfo">The state</param>
        public void Execute(object stateInfo)
        {
            _action("Message from SignalR server at:", DateTime.Now.ToLongTimeString());

            if ((DateTime.Now - TimerStarted).Seconds > 60)
            {
                // Enought
                _timer.Dispose();
            }
        }
    }
}


