namespace Infrastructure.Authentication
{
    public class JwtSetting
    {
        public const string SectionName = "JWTSetting";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public string ExpireInMinutes { get; set; }
    }
}
