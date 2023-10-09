namespace MetaPhoto.Tests.Services;

public class PhotoServiceTests
{
    private readonly PhotoService _photoService = new();
    private readonly ITestOutputHelper _testOutputHelper;

    public PhotoServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GetPhoto()
    {
        var photo = await _photoService.GetPhotoAsync(1);
        _testOutputHelper.WriteLine(photo!.Title);
        Assert.NotNull(photo);
    }

    [Fact]
    public async Task GetPhotos()
    {
        var photo = await _photoService.GetPhotosAsync();
        _testOutputHelper.WriteLine(photo!.First().Title);
        Assert.NotNull(photo);
    }

    [Fact]
    public async Task GetFilteredPhotos()
    {
        var photo = await _photoService.GetFilteredPhotosAsync("repudiandae iusto", null, null);
        _testOutputHelper.WriteLine(photo!.First().Title);
        Assert.NotNull(photo);
    }
}