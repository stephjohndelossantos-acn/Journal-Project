using System.Net;
using System.Net.Http.Json;
using Journal.ApiModels;

namespace Journal.Services;

public class JournalApiClient
{
    private readonly HttpClient _http;
    public JournalApiClient(HttpClient http) => _http = http;

    public async Task<List<ProductTypeDto>> GetTypesAsync(int section) =>
        await _http.GetFromJsonAsync<List<ProductTypeDto>>($"api/v1/producttypes?section={section}") ?? new();

    public Task<ProductTypeDto?> GetTypeAsync(int id) =>
        GetOrNullAsync<ProductTypeDto>($"api/v1/producttypes/{id}");

    public Task<ItemDto?> GetItemAsync(int id) =>
        GetOrNullAsync<ItemDto>($"api/v1/items/{id}");

    public async Task<PagedResult<ItemDto>> GetItemsAsync(int? type = null, bool? featured = null, int pageSize = 50)
    {
        var url = $"api/v1/items?pageSize={pageSize}";
        if (type.HasValue) url += $"&type={type}";
        if (featured.HasValue) url += $"&featured={featured.Value.ToString().ToLower()}";
        return await _http.GetFromJsonAsync<PagedResult<ItemDto>>(url) ?? new();
    }

    private async Task<T?> GetOrNullAsync<T>(string url) where T : class
    {
        var res = await _http.GetAsync(url);
        if (res.StatusCode == HttpStatusCode.NotFound) return null;
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<T>();
    }
}