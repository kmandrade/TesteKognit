namespace Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = null!;
    public DateTime BithDate { get; set; }
    public string NrCpf { get; set; } = null!;
    public List<Wallet> Wallets { get; set; } = [];
}
