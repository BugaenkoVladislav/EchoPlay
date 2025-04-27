const chatConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

const streamConnection = new signalR.HubConnectionBuilder()
    .withUrl("/streamhub")
    .build();

chatConnection.on("ReceiveMessage", (user, message) => {
    const chatBox = document.getElementById("chatWindow");
    const msg = `<div><strong>${user}:</strong> ${message}</div>`;
    chatBox.innerHTML += msg;
    chatBox.scrollTop = chatBox.scrollHeight;
});

streamConnection.on("ReceiveStream", (message) => {
    const streamBox = document.getElementById("streamWindow");
    const msg = `<div>${message}</div>`;
    streamBox.innerHTML += msg;
    streamBox.scrollTop = streamBox.scrollHeight;
});

async function sendChatMessage() {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("chatMessage").value;
    if (user && message) {
        await chatConnection.invoke("SendMessage", user, message);
        document.getElementById("chatMessage").value = "";
    }
}

async function sendStreamMessage() {
    const message = document.getElementById("streamMessage").value;
    if (message) {
        await streamConnection.invoke("Broadcast", message);
        document.getElementById("streamMessage").value = "";
    }
}

chatConnection.start().catch(err => console.error("Chat Hub Error:", err));
streamConnection.start().catch(err => console.error("Stream Hub Error:", err));
