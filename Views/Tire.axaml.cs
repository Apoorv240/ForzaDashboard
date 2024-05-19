using Avalonia;
using Avalonia.Controls.Primitives;
using System.Data;

namespace ForzaDashboard.Views;

public class Tire : TemplatedControl
{
    public static readonly StyledProperty<string> ColorProperty = AvaloniaProperty.Register<DataView, string>(nameof(Color));

    public string Color
    {
        get => GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }
}