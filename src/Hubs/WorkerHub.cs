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
        
        public string Echo(string message)
        {
            return _workerTicker.Echo(message);
        }

        public string BroadcastMessage(string username, string message)
        {
            // Clients.All.SendAsync("broadcastMessage", name, message);
            return _workerTicker.BroadcastMessage(username, message);
        }

        public async void JoinGroup(string name, string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("echo", "_SYSTEM_", $"{name} joined {groupName} with connectionId {Context.ConnectionId}");
        }

        public async void LeaveGroup(string name, string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Client(Context.ConnectionId).SendAsync("echo", "_SYSTEM_", $"{name} leaved {groupName}");
            await Clients.Group(groupName).SendAsync("echo", "_SYSTEM_", $"{name} leaved {groupName}");
        }

        public void SendGroup(string name, string groupName, string message)
        {
            Clients.Group(groupName).SendAsync("echo", name, message);
        }

        public void SendGroups(string name, IReadOnlyList<string> groups, string message)
        {
            Clients.Groups(groups).SendAsync("echo", name, message);
        }
    }
}