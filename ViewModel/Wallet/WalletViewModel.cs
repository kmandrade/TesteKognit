using System.ComponentModel.DataAnnotations;

namespace ViewModel.Wallet;

public class WalletViewModel
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "O usuário é obrigatório.")]
    public int? UserId { get; set; }

    [Required(ErrorMessage = "O valor atual é obrigatório.")]
    public decimal? CurrentValue { get; set; }

    [Required(ErrorMessage = "O campo banco é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O banco deve ter no máximo 100 caracteres.")]
    public string Bank { get; set; } = null!;

    public bool? Active { get; set; }
}
