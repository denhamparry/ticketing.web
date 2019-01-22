using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Ticketing.Web.Hubs;

namespace Ticketing.Web.WorkerClient
{
    public class WorkerTicker
    {
        private readonly SemaphoreSlim _stateLock = new SemaphoreSlim(1, 1);

        public WorkerTicker(IHubContext<WorkerHub> hub)
        {
            Hub = hub;
        }

        private IHubContext<WorkerHub> Hub
        {
            get;
            set;
        }

        public string Echo(string message)
        {
            message = $"Echo: {message}";
            Console.WriteLine(message);
            return message;
        }

        public string BroadcastMessage(string username, string message)
        {
            message = $"{username}: {message}";
            Console.WriteLine(message);
            return message;
        }
    }
}