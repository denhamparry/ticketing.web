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

        public string BroadcastMessage(string name, string message)
        {
            Clients.All.SendAsync("broadcastMessage", name, message);
            Console.WriteLine($"[{DateTime.Now.ToString()}] BroadcastMessage | {name} : {message}");
            return _workerTicker.BroadcastMessage(name, message);
        }

        public async void JoinGroup(string name, string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("group", "_SYSTEM_", $"{name} joined {groupName} with connectionId {Context.ConnectionId}");
            Console.WriteLine($"[{DateTime.Now.ToString()}] JoinGroup | {groupName} : {name}");
        }

        public async void LeaveGroup(string name, string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Client(Context.ConnectionId).SendAsync("group", "_SYSTEM_", $"{name} leaved {groupName}");
            await Clients.Group(groupName).SendAsync("group", "_SYSTEM_", $"{name} leaved {groupName}");
            Console.WriteLine($"[{DateTime.Now.ToString()}] LeaveGroup | {groupName} : {name}");
        }

        public void SendGroup(string name, string groupName, string message)
        {
            Clients.Group(groupName).SendAsync("broadcastMessage", name, message);
            Console.WriteLine($"[{DateTime.Now.ToString()}] SendGroup | {groupName} : {name} : {message}");
        }
    }
}