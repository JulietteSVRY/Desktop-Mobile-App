using Avalonia.Controls;
using Avalonia.Media;

namespace RickAndMorty.Views;

public partial class MainPage : UserControl
{
    public MainPage()
    {
        InitializeComponent();
#if ANDROID
        ImageBackground.RenderTransform = new RotateTransform
        {
            Angle = 90
        };
#endif
    }
}