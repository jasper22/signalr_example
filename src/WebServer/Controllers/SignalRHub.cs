using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebServer.Controllers
{
    /// <summary>
    /// <c>SignalRHub</c>
    /// This is actuall implementation of SignalR abilities in this application
    /// This is "strongly typed" Hub:   https://docs.microsoft.com/en-us/aspnet/core/signalr/hubs?view=aspnetcore-3.1#strongly-typed-hubs
    /// 
    /// It is not used directly !! In Controllers it used as interface: <c>IHubContext<SignalRHub, ISignalRCommands></c> !
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.SignalR.Hub{WebServer.Controllers.ISignalRCommands}" />
    public class SignalRHub : Hub<ISignalRCommands>
    {
        /// <summary>
        /// The logger
        /// </summary>
        private ILogger<SignalRHub> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalRHub"/> class.
        /// </summary>
        /// <param name="globalLogger">The global logger.</param>
        public SignalRHub(ILogger<SignalRHub> globalLogger)
        {
            this.logger = globalLogger;
        }

        /// <summary>
        /// Called when a new connection is established with the hub.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous connect.
        /// </returns>
        public override Task OnConnectedAsync()
        {
            this.logger.LogInformation($"New client connected !! with Cotext ID {this.Context.ConnectionId} with user identifier: {this.Context.UserIdentifier}");

            // Just a test
            //    await Clients.All.SendAsync("NewMessage", "this is param1", "Hello", CancellationToken.None).ConfigureAwait(false);

            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Notifies all clients
        /// 
        /// Just an example function that receive 2 parameters and actually use SignalR web-sockets to send message to Angular client
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="message">The message.</param>
        public async Task NotifyAll(string param, string message)
        {

            if (Clients != null)
            {
                await Clients.All.NotifyAll(param, message).ConfigureAwait(false);

                this.logger.LogInformation("Send notification to all clients");
            }
            else
            {
                this.logger.LogError("No clients connected");
            }
        }
    }
}
