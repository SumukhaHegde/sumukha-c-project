namespace Infrastructure.Authentication
{
    public class JWTGenerationSetting
    {
        public const string SectionName = "JWTGenerationSetting";
        public string Issuer { get; set; }
        public string SigningKey { get; set; }
        public string EncryptionKey { get; set; }
        public string ExpireInDays { get; set; }
    }
}
