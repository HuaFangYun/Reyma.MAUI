using System;
using System.Text.Json.Serialization;

namespace NavbarAnimation.Maui.Models;

public class PagedResponse<TResponse>
{
    /*
      current_page": 1,
      "page_size": 50,
      "page_count": 1108,
      "row_count": 55397,
      "results": [
    */
    [JsonPropertyName("current_page")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }

    [JsonPropertyName("page_count")]
    public int PageCount { get; set; }

    [JsonPropertyName("row_count")]
    public int RowCount { get; set; }


    public IEnumerable<TResponse> Results { get; set; }
}