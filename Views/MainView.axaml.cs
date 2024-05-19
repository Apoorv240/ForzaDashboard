using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ForzaDashboard.Views;

public partial class MainView : UserControl
{

    public MainView()
    {
        DataContext = new ViewModels.MainViewViewModel();
        InitializeComponent();
    }
}