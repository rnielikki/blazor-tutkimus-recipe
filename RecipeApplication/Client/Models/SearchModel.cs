using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;
using RecipeApplication.Models;
using System.Text;

namespace RecipeApplication.Client
{
    public class SearchModel : ISearchModel
    {
        public IEnumerable<string> Keywords { get; private set; }
        public IEnumerable<RecipeDto> Results { get; private set; }
        public event Action RefreshActions;
        private HttpClient client;
        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        public SearchModel(HttpClient httpClient) {
            client = httpClient;
        }

        public async Task FindAsync(IList<string> data)
        {
            Keywords = data;
            Results = JsonSerializer.Deserialize<List<RecipeDto>>
                (await
                    (await client
                    .PostAsync("api/Recipes/Search", new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json")
                    )
                ).Content.ReadAsStringAsync(), jsonOptions);
            RefreshActions?.Invoke();
        }
    }
}
