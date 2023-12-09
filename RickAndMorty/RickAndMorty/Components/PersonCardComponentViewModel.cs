using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.ViewModels;

namespace RickAndMorty.Components;

public partial class PersonCardComponentViewModel : BaseLikedViewModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    [ObservableProperty]
    private string _webSourceImage = string.Empty;

    [ObservableProperty]
    private string _fullName = string.Empty;

    [ObservableProperty]
    private string _lastLocation = string.Empty;

    [ObservableProperty]
    private string _status = string.Empty;

    [ObservableProperty]
    private ulong _id;


    [ObservableProperty]
    private IBrush _backgroundCard;

    [ObservableProperty]
    private Bitmap? _image;
    
    public PersonCardComponentViewModel() => 
        _httpClientFactory = Helpers.GetAppServiceProvider().GetService<IHttpClientFactory>()!;

    [RelayCommand]
    private async Task DownloadImageFromUrl(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(WebSourceImage) || !Uri.TryCreate(WebSourceImage, UriKind.Absolute, out var downloadUri))
            return;

        using var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = downloadUri;
        await using var imageStream = await httpClient.GetStreamAsync(string.Empty, cancellationToken);
        using var memoryStream = new MemoryStream();
        await imageStream.CopyToAsync(memoryStream, cancellationToken);
        memoryStream.Seek(0, SeekOrigin.Begin);
        Image = new Bitmap(memoryStream);
        NewBackground();
    }

    private void NewBackground()
    {
        var height = (int)Image.Size.Height;
        var width = (int)Image.Size.Width;
        var writeableBitmap = new WriteableBitmap(
            new PixelSize(width, height),
            new Vector(96, 96),
            PixelFormat.Bgra8888,
            AlphaFormat.Premul);
        using var fb = writeableBitmap.Lock();
        var buffer = fb.Address;
        var stride = fb.RowBytes / sizeof(uint);
    
        // Замените следующие две строки координатами интересующего вас пикселя.
        var targetX = Random.Shared.Next(10,250); 
        var targetY = Random.Shared.Next(10,250);

        var pixel = (uint)(buffer + targetY * stride + targetX);
           
        var a = 175;
        var r = (int)(pixel >> 16) & 255;
        var g = (int)(pixel >> 8) & 255;
        var b = (int)pixel & 255;
       
        // Формируем цвет 
        var color = Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
        BackgroundCard = new SolidColorBrush(color);
    }
}