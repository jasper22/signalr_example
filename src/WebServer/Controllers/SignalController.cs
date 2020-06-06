using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebServer.Controllers
{
    /// <summary>
    /// <c>SignalController</c>
    /// 
    /// This is actuall controller that will use SignalR
    /// It use it with conjustion with Timer component
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class SignalController : ControllerBase
    {
        /// <summary>
        /// The actuall object of SignalR. 
        /// Do not use the <see cref="SignalRHub"/> or <see cref="ISignalRCommands"/> objects - somehow it does not work
        /// </summary>
        private IHubContext<SignalRHub, ISignalRCommands> commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalController"/> class.
        /// </summary>
        /// <param name="signalCommand">The actual SignalR object</param>
        public SignalController(IHubContext<SignalRHub, ISignalRCommands> signalCommand)
        {
            this.commands = signalCommand;
        }

        /// <summary>
        /// Function will respond to default GET to this site
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<string> Get()
        {
            // Make a simel timer object that will send messages to SignalR client long after this GET function completes
            var timerComponent = new TimerComponent((param1, param2) => this.commands.Clients.All.NotifyAll(param1, param2));

            // Actuall GET response completes here
            return Task.FromResult("Function GET is done! Now you should receive SignalR events");
        }
    }
}
