﻿using Mosumbi.Dx.Desktop.Shared.Reactive;
using Mosumbi.Dx.Shared.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mosumbi.Dx.Desktop.UI.ViewModels.Fakes;

public class FakeChatWindowViewModel : FakeBrandedViewModelBase, IChatWindowViewModel
{
    public ObservableCollection<ChatMessage> ChatMessages { get; } = new()
    {
        new ChatMessage("Designer", "This is a design-time test message.")
    };

    public string InputText
    {
        get => "Some text I'm going to send.";
        set { }
    }
    public string OrganizationName
    {
        get => "Design-Time Technicians";
        set { }
    }
    public string SenderName
    {
        get => "Test Tech";
        set { }
    }

    public string ChatSessionHeader => "Test Chat";

    public ICommand CloseCommand => new RelayCommand(() => { });

    public ICommand MinimizeCommand => new RelayCommand(() => { });

    public Task SendChatMessage()
    {
        return Task.CompletedTask;
    }
}
