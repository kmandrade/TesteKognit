namespace Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string NrCpf { get; set; } = null!;
    public List<Wallet> Wallets { get; set; } = [];
}
