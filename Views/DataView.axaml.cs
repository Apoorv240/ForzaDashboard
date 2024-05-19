using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Data;

namespace ForzaDashboard.Views;

public partial class DataView : TemplatedControl
{
    public static readonly StyledProperty<string> DataTextProperty = AvaloniaProperty.Register<DataView, string>(nameof(DataText));
    public static readonly StyledProperty<string> SubTextProperty = AvaloniaProperty.Register<DataView, string>(nameof(SubText));

    public string DataText
    {
        get => GetValue(DataTextProperty); 
        set => SetValue(DataTextProperty, value);
    }

    public string SubText
    {
        get => GetValue(SubTextProperty);
        set => SetValue(SubTextProperty, value);
    }
}