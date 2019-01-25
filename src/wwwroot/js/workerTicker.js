let connection = new signalR.HubConnectionBuilder()
    .withUrl("/workers")
    .build();

connection.start().then(function () {
    // connection.invoke("GetAllStocks").then(function (stocks) {
    //     for (let i = 0; i < stocks.length; i++) {
    //         displayStock(stocks[i]);
    //     }
    // });

    // connection.invoke("GetMarketState").then(function (state) {
    //     if (state === 'Open') {
    //         marketOpened();
    //         startStreaming();
    //     } else {
    //         marketClosed();
    //     }
    // });

    // document.getElementById('open').onclick = function () {
    //     connection.invoke("OpenMarket");
    // }

    // document.getElementById('close').onclick = function () {
    //     connection.invoke("CloseMarket");
    // }

    // document.getElementById('reset').onclick = function () {
    //     connection.invoke("Reset").then(function () {
    //         connection.invoke("GetAllStocks").then(function (stocks) {
    //             for (let i = 0; i < stocks.length; ++i) {
    //                 displayStock(stocks[i]);
    //             }
    //         });
    //     });
    // }
});

connection.on("group", function (name, message) {
    groupMessage(name, message);
});

connection.on("broadcastMessage", function (name, message) {
    broadcastMessage(name, message);
});

function groupMessage(name, message) {
    console.log("groupMessage from " + name, message);
}

function broadcastMessage(name, message) {
    console.log("broadcastMessage from " + name, message);
}