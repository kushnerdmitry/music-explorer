using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using RateLimiter;

namespace MusicExplorer.Old.WimpMusic.InfoProviders.GetHtmlProvider {
    public static class WebRequests {
        private static readonly string UserAgent =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";

        private static readonly TimeLimiter TimeConstraint =
            TimeLimiter.GetFromMaxCountByInterval(3, TimeSpan.FromSeconds(1));

        private static readonly Lazy<HttpClient> ClientInstance = new Lazy<HttpClient>(() => {
            var client = new HttpClient {
                Timeout = TimeSpan.FromSeconds(5),
                DefaultRequestHeaders = {
                    AcceptLanguage = {
                        new StringWithQualityHeaderValue("ru-RU"),
                        new StringWithQualityHeaderValue("ru")
                    },
                    Accept = {new MediaTypeWithQualityHeaderValue("text/html")},
                    UserAgent = { ProductInfoHeaderValue.Parse(UserAgent) }
                }
            };

            // client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            client.DefaultRequestHeaders.Add("Cookie", "TrackJS=d0865353-c564-4f8e-8bdf-83b6442b2a4b"); //TODO

            return client;
        });

        public static async Task<string> GetHtml(Uri searchUri, CancellationToken token) {
            async Task<string> HttpResponse() {
                var response = await ClientInstance.Value.GetAsync(searchUri, token);

                if (!ResponseIsValid(response))
                    throw new Exception("Invalid HTML response");

                return await response.Content.ReadAsStringAsync();
            }

            return await TimeConstraint.Perform(HttpResponse, token);
        }

        private static bool ResponseIsValid(HttpResponseMessage response) {
            if (response.Content.Headers.ContentType?.MediaType != "text/html") {
                return false;
            }

            return response.StatusCode == HttpStatusCode.OK ||
                   response.StatusCode == HttpStatusCode.Found ||
                   response.StatusCode == HttpStatusCode.NotModified;
        }
    }
}