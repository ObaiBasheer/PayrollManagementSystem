namespace PayrollManagementSystem.Models
{
    public record JwtOptions
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
     
    }
}
