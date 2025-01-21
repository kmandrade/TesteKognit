using System.ComponentModel.DataAnnotations;

namespace ViewModel.User;

public class UserViewModel
{
    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "O campo data de nascimento é obrigatório.")]
    public DateTime BithDate { get; set; }

    [Required(ErrorMessage = "O campo número do CPF é obrigatório.")]
    public string NrCpf { get; set; } = null!;

    public int? UserId { get; set; }
}

