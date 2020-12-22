namespace WimpMusic.Api.Client.Sections {
    public abstract class ApiSectionBase {
        public ApiSectionBase(string token, string countryCode) {
            this.Token = token;
            this.CountryCode = countryCode;
        }

        protected string Token { get; }
        protected string CountryCode { get; }
    }
}
