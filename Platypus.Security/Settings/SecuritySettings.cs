namespace Platypus.Security.Settings {

    public class SecuritySettings {
        public string Key { get; set; }
        public int? TokenExpiryMins { get; set; }
        public int? RefreshTokenExpiryMins { get; set; }
        public int? MaxLoginAttempts { get; set; }
    }
}