namespace MetaPhoto.Shared.Services
{
    public class PhotoService
    {
        private readonly HttpClient _httpClient = new(); // Create an instance of HttpClient to make HTTP requests.

        // Get a single photo by its ID asynchronously.
        public async Task<Photo?> GetPhotoAsync(int photoId)
        {
            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/photos/{photoId}");
            response.EnsureSuccessStatusCode(); // Ensure the HTTP request was successful.

            var json = await response.Content.ReadAsStringAsync(); // Read the JSON response.
            var photo = JsonConvert.DeserializeObject<Photo>(json); // Deserialize the JSON to a Photo object.

            return photo; // Return the deserialized Photo object.
        }

        // Get a list of photos asynchronously with optional limit and offset parameters.
        public async Task<IEnumerable<Photo>?> GetPhotosAsync(int? limit = 25, int? offset = 0)
        {
            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/photos?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode(); // Ensure the HTTP request was successful.

            var json = await response.Content.ReadAsStringAsync(); // Read the JSON response.
            var photos = JsonConvert.DeserializeObject<IEnumerable<Photo>>(json); // Deserialize the JSON to a collection of Photo objects.

            return photos; // Return the deserialized collection of Photo objects.
        }

        // Get a list of photos asynchronously with optional filtering based on title, album title, and album user email.
        public async Task<IEnumerable<Photo>?> GetFilteredPhotosAsync(string? titleContains, string? albumTitleContains, string? albumUserEmailEquals)
        {
            var filters = new List<string>();

            if (!string.IsNullOrEmpty(titleContains)) filters.Add($"title={titleContains}");
            if (!string.IsNullOrEmpty(albumTitleContains)) filters.Add($"album.title={albumTitleContains}");
            if (!string.IsNullOrEmpty(albumUserEmailEquals)) filters.Add($"album.user.email={albumUserEmailEquals}");

            var filterString = string.Join("&", filters);

            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/photos?{filterString}");
            response.EnsureSuccessStatusCode(); // Ensure the HTTP request was successful.

            var json = await response.Content.ReadAsStringAsync(); // Read the JSON response.
            var photos = JsonConvert.DeserializeObject<IEnumerable<Photo>>(json); // Deserialize the JSON to a collection of Photo objects.

            return photos; // Return the deserialized collection of Photo objects based on applied filters.
        }
    }
}
