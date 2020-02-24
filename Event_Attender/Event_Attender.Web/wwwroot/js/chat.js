"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (messageId, user, message, timestamp) {
    var timestamp = document.createTextNode(timestamp);
    var span = document.createElement("span");
    span.appendChild(timestamp);
    span.className = "timestamp";
    
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = document.createTextNode(user + ": " + msg);
    var div = document.createElement("div");

    div.appendChild(span);
    div.appendChild(encodedMsg);
    div.className = "chat-container";
    div.id = messageId;
    
    document.getElementById("messagesList").appendChild(div);
    
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var userId = document.getElementById("userId").value;
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", userId, user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    document.getElementById("messageInput").value = "";
});
