using WimpMusic.Api.Client.Sections.Search;

namespace WimpMusic.Api.Client {
    public class WimpClient {
        // TODO generate method sections
        // TODO handle country code
        // TODO handle token

        // TODO API from js-file

        public WimpClient() {
            var countryCode = "RU";
            var token = "oIaGpqT_vQPnTr0Q";

            this.Search = new SearchSection(token, countryCode);
        }

        public SearchSection Search { get; }
    }
}
