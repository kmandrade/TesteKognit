namespace Domain.Entities;

public class Wallet : BaseEntity
{
    public int UserId { get; set; }
    public decimal CurrentValue { get; set; }
    public string Bank { get; set; } = null!;
    public User User { get; set; } = null!;
}
