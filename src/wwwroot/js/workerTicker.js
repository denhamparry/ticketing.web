let connection = new signalR.HubConnectionBuilder()
    .withUrl("/workers")
    .build();

connection.start().then(function () {
    var name = "WebUser";
    consoleText('Javascript here, reporting for duty!');
    consoleText('Attempting connection to SignalR hub...');
    connection.invoke("broadcastMessage", name, "New challenger approaching!").catch(err => console.error(err.toString()));
    var groupName = getCookie("ticketId");
    consoleText('Trying to join group ' + groupName);
    connection.invoke("JoinGroup", name, groupName).catch(err => console.error(err.toString()));
    consoleText('Now just waiting for a worker...');
});

connection.on("group", function (name, message) {
    groupMessage(name, message);
});

connection.on("broadcastMessage", function (name, message) {
    broadcastMessage(name, message);
});

connection.on("completed", function (url) {
    window.location.replace(url);
});


function groupMessage(name, message) {
    var printMessage = getDateTime() + ": groupMessage from " + name + ": " + message;
    console.log(printMessage);
    consoleText(printMessage);
}

function broadcastMessage(name, message) {
    var printMessage = getDateTime() + ": broadcastMessage from " + name + ": " + message;
    console.log(printMessage);
    consoleText(printMessage);
}

function consoleText(printMessage) {
    var ul = document.getElementById("shell-body");
    var li = document.createElement("li");
    li.appendChild(document.createTextNode(printMessage));
    ul.insertBefore(li, ul.childNodes[0]);
}

function getDateTime() {
    var currentdate = new Date();
    var datetime = "Last Sync: "
        + pad(currentdate.getDate()) + "/"
        + pad((currentdate.getMonth() + 1)) + "/"
        + currentdate.getFullYear() + " @ "
        + pad(currentdate.getHours()) + ":"
        + pad(currentdate.getMinutes()) + ":"
        + pad(currentdate.getSeconds());
    return datetime;
}

function pad(n) { return ("0" + n).slice(-2); }

function getCookie(name) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length == 2) return parts.pop().split(";").shift();
  }
