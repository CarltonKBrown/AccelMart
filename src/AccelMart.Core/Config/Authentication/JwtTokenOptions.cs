using AccelMart.Core.Common;

namespace AccelMart.Core.Config.Authentication
{
    public class JwtTokenOptions
    {
        public static string ConfigurationSection => StringConstants.JWT_TOKEN_CONFIG_SECTION;
        public string Secret { get; set; } = string.Empty;
        public int ExpirtyInSeconds { get; set; } = 0;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
