namespace PayrollManagementSystem.Models
{
    public record JwtOptions
    {
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
     
    }
}
