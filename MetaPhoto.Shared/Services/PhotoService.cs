namespace MetaPhoto.Shared.Services;

public class PhotoService
{
    private readonly HttpClient _httpClient = new();

    public async Task<Photo?> GetPhotoAsync(int photoId)
    {
        var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/photos/{photoId}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var photo = JsonConvert.DeserializeObject<Photo>(json);

        return photo;
    }

    public async Task<IEnumerable<Photo>?> GetPhotosAsync(int? limit = 25, int? offset = 0)
    {
        var response =
            await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/photos?limit={limit}&offset={offset}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var photos = JsonConvert.DeserializeObject<IEnumerable<Photo>>(json);

        return photos;
    }

    public async Task<IEnumerable<Photo>?> GetFilteredPhotosAsync(string? titleContains, string? albumTitleContains,
        string? albumUserEmailEquals)
    {
        var filters = new List<string>();

        if (!string.IsNullOrEmpty(titleContains)) filters.Add($"title={titleContains}");

        if (!string.IsNullOrEmpty(albumTitleContains)) filters.Add($"album.title={albumTitleContains}");

        if (!string.IsNullOrEmpty(albumUserEmailEquals)) filters.Add($"album.user.email={albumUserEmailEquals}");

        var filterString = string.Join("&", filters);

        var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/photos?{filterString}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var photos = JsonConvert.DeserializeObject<IEnumerable<Photo>>(json);

        return photos;
    }
}