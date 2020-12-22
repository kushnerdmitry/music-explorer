using System;

namespace WimpMusic.Api.Model.Artists {
    public class BioResponse {
        public string Source { get; } // TODO or enum?

        public DateTime LastUpdated { get; }

        public string Text { get; }

        public string Summary { get; }
    }
}
