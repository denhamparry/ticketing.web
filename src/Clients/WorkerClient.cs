using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Ticketing.Web.Hubs;

namespace Ticketing.Web.Clients
{
    public class WorkerClient
    {
        private readonly SemaphoreSlim _stateLock = new SemaphoreSlim(1, 1);

        public WorkerClient(IHubContext<WorkerHub> hub)
        {
            Hub = hub;
        }

        private IHubContext<WorkerHub> Hub
        {
            get;
            set;
        }
        
        public string BroadcastMessage(string name, string message)
        {
            message = $"{name}: {message}";
            return message;
        }
    }
}