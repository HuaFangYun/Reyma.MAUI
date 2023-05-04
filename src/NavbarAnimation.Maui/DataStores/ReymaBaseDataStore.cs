using System;
using System.Net.Http.Json;

using Microsoft.Extensions.Http;
using NavbarAnimation.Maui.Models;

namespace NavbarAnimation.Maui.DataStores;

/// <summary>
/// 
/// </summary>
public abstract class ReymaBaseDataStore<TEntity, TResponse> : IReymaBaseDataStore
{
	protected abstract string ApiUrl { get; }

	private readonly IHttpClientFactory _httpClientFactory;

    protected ReymaBaseDataStore(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TResponse> GetEntity(Guid id) => await ProcessHttpRequest<TResponse>($"{ApiUrl}/{id}");

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<PagedResponse<TResponse>> GetList() => await ProcessHttpRequest<PagedResponse<TResponse>>(ApiUrl);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="baseUrl"></param>
    /// <returns></returns>
    protected virtual async Task<TResult> ProcessHttpRequest<TResult>(string baseUrl)
    {
        var httpClient = _httpClientFactory.CreateClient("ReymaMobileHttpClient");
        var httpResponseMessage = await httpClient.GetAsync(baseUrl);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            return await httpResponseMessage.Content.ReadFromJsonAsync<TResult>();
        }

        var message = await httpResponseMessage.Content.ReadAsStringAsync();

        return default;
    }
}

