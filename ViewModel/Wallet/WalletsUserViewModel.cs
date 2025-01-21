using ViewModel.User;

namespace ViewModel.Wallet;

public class WalletsUserViewModel
{
    public bool? Active { get; set; }
    public decimal CurrentValue { get; set; }
    public string Bank { get; set; } = null!;
    public DateTime? DateModified { get; set; }

}
