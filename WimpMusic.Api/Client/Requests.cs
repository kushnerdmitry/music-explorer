using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WimpMusic.Api.Client {
    internal class Requests {
        private readonly HttpClient _httpClient = new HttpClient {
            BaseAddress = new Uri("play.wimpmusic.com")
        };

        internal Requests(
            string token,
            string countryCode
        ) {

        }

        public async Task<TResponse> GetV1<TResponse>(
            string url,
            IDictionary<string, string> parameters
        ) {
            // /v1/albums/110508065/tracks?token=oIaGpqT_vQPnTr0Q&countryCode=RU
            var url = new Uri(); // TODO queryparams

            QueryHelpers.AddQueryString()

            var stream = await this._httpClient.GetStreamAsync(url);

            return await JsonSerializer.DeserializeAsync<TResponse>(stream);
        }
    }
}
