"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var timestamp = Date.now();
    var span = document.createElement("span");
    span.className = "timestamp";
    spna.textContent = timestamp;

    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + ": " + msg;

    var div = document.createElement("div");
    div.className = "chat-container";
    div.textContent = encodedMsg;
    div.appendChild(span);

    document.getElementById("messagesList").appendChild(div);
    document.getElementById("messageInput").value = "";
    
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
    if (message != "") {
        connection.invoke("SendMessage", userId, user, message).catch(function (err) {
            return console.error(err.toString());
        });
        
        //$.post("/Administrator/Administrator/SnimiPoruku?userId=" + userId + "&poruka=" + message);
    }
    
    event.preventDefault();
});