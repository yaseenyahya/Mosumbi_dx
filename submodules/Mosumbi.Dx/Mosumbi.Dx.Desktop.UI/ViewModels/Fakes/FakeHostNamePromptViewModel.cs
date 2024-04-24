using Mosumbi.Dx.Desktop.Shared.Reactive;
using System.Windows.Input;

namespace Mosumbi.Dx.Desktop.UI.ViewModels.Fakes;

public class FakeHostNamePromptViewModel : FakeBrandedViewModelBase, IHostNamePromptViewModel
{
    public string Host { get; set; } = "https://localhost:7024";

    public ICommand OKCommand => new RelayCommand(() => { });
}
