﻿namespace Mosumbi.Dx.Shared.Models;

public class ChatMessage
{
    public ChatMessage() { }

    public ChatMessage(string senderName, string message, bool disconnected = false)
    {
        SenderName = senderName;
        Message = message;
        Disconnected = disconnected;
    }

    public bool Disconnected { get; set; }
    public string Message { get; set; } = string.Empty;
    public string SenderName { get; set; } = string.Empty;
}
