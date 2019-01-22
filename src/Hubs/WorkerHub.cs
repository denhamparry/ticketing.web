using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Ticketing.Web.WorkerClient;

namespace Ticketing.Web.Hubs
{
    public class WorkerHub : Hub
    {
        private readonly WorkerTicker _workerTicker;

        public WorkerHub(WorkerTicker stockTicker)
        {
            _workerTicker = stockTicker;
        }
        
        public string Alive(string message)
        {
            return _workerTicker.Alive(message);
        }
    }
}