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

        public IEnumerable<WorkerTask> GetAllStocks()
        {
            return _workerTicker.GetAllStocks();
        }

        public ChannelReader<WorkerTask> StreamStocks()
        {
            return _workerTicker.StreamStocks().AsChannelReader(10);
        }

        public string GetMarketState()
        {
            return _workerTicker.MarketState.ToString();
        }

        public async Task OpenMarket()
        {
            await _workerTicker.OpenMarket();
        }

        public async Task CloseMarket()
        {
            await _workerTicker.CloseMarket();
        }

        public async Task Reset()
        {
            await _workerTicker.Reset();
        }
    }
}