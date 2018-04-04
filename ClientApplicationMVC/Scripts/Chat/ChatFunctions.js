﻿
var currentSelectedChat = null;
var myConnection = null;
var name;
/**
*   This function will set the on click functions for the send button and chat instances.
*/
$(function () {//This function is executed after the entire page is loaded
    CreateChatHub();
    $("#SendButton").click(sendMessage);
    $("#ChatInstancesList").children().each(function () {
        $(this).click(chatInstanceSelected);
    });
    var firstChatInstanceBox = $("#ChatInstancesList").children().first();

    firstChatInstanceBox.css("background", "rgba(255, 255, 255, 0.1)");
    currentSelectedChat = firstChatInstanceBox.attr("id");
});

/**
 *  Validates the message the user is trying to send, and sends it.
*   This function will reset the message box and append the message the user sent to the message display area
 */
function sendMessage() {
    var userData = $("#textUserMessage").val();
    if ($.trim(userData) == "") {
        return;
    }
    $("#textUserMessage").val("");//Clear the chat box

    addTextToChatBox(userData, "You");
    var recipient = currentSelectedChat;
    var timestamp = Math.round((new Date()).getTime() / 1000);

    myConnection.server.send(recipient, userData);

    $.post("/Chat/SendMessage", {
        receiver: recipient,
        timestamp: timestamp,
        message: userData
    });
}

/**
 * This function adds the given text to the user and indicates the sender of the text.
 * @param {string} text - The content of the message
 * @param {string} sender - The username of the sender. If it is "You" it will be a different colour.
 */
function addTextToChatBox(text, sender) {
    var newMessageHtml =
        "<p class='message'>" +
        "<span class='username'";

    if (sender === "You") {
        newMessageHtml += " style='color:#1185f9;'>You: ";
    }
    else {
        newMessageHtml += " style='color:#785ff4;'>" + sender + ": ";
    }

    newMessageHtml += "</span>" + text + "</p>";

    $("#ConversationDisplayArea").html(// Add the new message to the message display area.
        $("#ConversationDisplayArea").html() + newMessageHtml);

    $("#ConversationDisplayArea").scrollTop($("#ConversationDisplayArea").prop("scrollHeight"));//Make the scrollbar scroll to the bottom
}

/**
 * When a user selects their chat history with a specific user, this function will load and display the chat history.
 */
function chatInstanceSelected() {
    if ($(this).attr("id") == currentSelectedChat) {
        return;
    }

    $("#" + currentSelectedChat).css("background", "initial");

    currentSelectedChat = $(this).attr("id");

    $("#" + currentSelectedChat).css("background", "rgba(255, 255, 255, 0.1)");

    $.ajax({
        method: "GET",
        url: "/Chat/Conversation",
        data: {
            otherUser: currentSelectedChat
        },
        success: function (data) {
            $("#ConversationDisplayArea").html(data);
        }
    });

}

function CreateChatHub() {
    myConnection = $.connection.hub.createHubProxy("ChatHub");

    myConnection.client.instantMessage = function (sender, message) {
        if (currentSelectedChat === sender) {
            addTextToChatBox(message, sender);
        }
    };


    $.connection.hub.start().done(function () {
        //Here you may place javascript code that will run once the server confirms a 
        //successful connection
        name = $("#ClientUsername").val();
        myConnection.server.userOnline(name);
    });
}