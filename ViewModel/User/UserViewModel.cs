using System.ComponentModel.DataAnnotations;

namespace ViewModel.User;

public class UserViewModel
{
    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "O campo data de nascimento é obrigatório.")]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "A data de nascimento deve estar no formato yyyy-MM-dd.")]
    public string BirthDate { get; set; } = null!;

    [Required(ErrorMessage = "O campo número do CPF é obrigatório.")]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato 123.456.789-00.")]
    public string NrCpf { get; set; } = null!;

    public bool? Active { get; set; }
    public int? UserId { get; set; }
}

