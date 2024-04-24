using Avalonia.Controls;
using Mosumbi.Dx.Desktop.Shared;
using Mosumbi.Dx.Desktop.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Mosumbi.Dx.Desktop.UI.Views;
public partial class MainView : UserControl
{
    public MainView()
    {
        if (!Design.IsDesignMode)
        {
            DataContext = StaticServiceProvider.Instance?.GetService<IMainViewViewModel>();
        }

        InitializeComponent();
        ViewerListBox.SelectionChanged += ViewerListBox_SelectionChanged;
        Loaded += MainView_Loaded;
    }

    private void ViewerListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is MainViewViewModel viewModel &&
            sender is ListBox viewerListBox &&
            viewerListBox.SelectedItems is not null)
        {
            viewModel.SelectedViewers = viewerListBox.SelectedItems.OfType<IViewer>().ToList();
        }
    }

    private async void MainView_Loaded(object? sender, System.EventArgs e)
    {
        if (DataContext is MainViewViewModel viewModel)
        {
            await viewModel.Init();
        }
    }
}
