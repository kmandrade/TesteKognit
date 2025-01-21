namespace ViewModel.Wallet;

public class WalletViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal CurrentValue { get; set; }
    public string Bank { get; set; } = null!;
    public bool? Active { get; set; }
}
