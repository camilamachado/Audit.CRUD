namespace Audit.CRUD.Sample.Infra.Settings
{
    public class AuthSettings
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Endpoint { get; set; }
        public int Expiration { get; set; }
    }
}