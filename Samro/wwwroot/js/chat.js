$(document).ready(function () {
    const roomId = $("#roomId").val();
    const userId = $("#userId").val();

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();

    connection.start()
        .then(() => {
            console.log("✅ اتصال به SignalR برقرار شد");
            return connection.invoke("JoinRoom", roomId);
        })
        .then(() => {
            console.log("🟢 به گروه " + roomId + " پیوستی");
        })
        .catch(err => {
            console.error("❌ خطا در اتصال یا پیوستن به گروه", err);
        });

    connection.on("ReceiveMessage", function (senderName, content, timestamp, fromUserId) {
        console.log("📥 پیام جدید:", { senderName, content, timestamp, fromUserId });

        const currentUserId = $("#userId").val();
        const isMine = fromUserId === currentUserId;
        const messageClass = isMine ? "my-message" : "other-message";

        // نمایش مستقیم timestamp بدون تبدیل آن
        console.log("Timestamp received: ", timestamp);

        const html = `
        <div class="chat-message-container mb-2" style="animation: fadeIn 0.5s ease-in;">
            <div class="chat-message ${messageClass}">
                <div class="chat-message-header">
                    <strong>${senderName || 'کاربر ناشناس'}:</strong>
                    <small style="font-size: 8px;" class="text-muted">${timestamp}</small>
                </div>
                <div class="chat-message-body">
                    ${content}
                </div>
            </div>
        </div>`;

        $("#messagesContainer").append(html);
        scrollToBottom();
    });

    $("#sendMessageForm").submit(function (e) {
        e.preventDefault();
        const content = $("#messageContent").val().trim();

        if (!content) return;

        connection.invoke("SendMessage", roomId, content)
            .then(() => {
                console.log("✅ پیام ارسال شد");
                scrollToBottom();
                $("#messageContent").val("");
            })
            .catch(err => {
                console.error("❌ خطا در ارسال پیام با SignalR", err);
            });
    });


    function scrollToBottom() {
        const container = document.getElementById("messagesContainer");
        container.scrollTop = container.scrollHeight;
    }

    connection.onclose(async () => {
        try {
            await connection.invoke("LeaveRoom", roomId);
            console.log("❌ کاربر از گروه خارج شد");
        } catch (err) {
            console.error("❌ خطا در خروج از گروه", err);
        }
    });
});
