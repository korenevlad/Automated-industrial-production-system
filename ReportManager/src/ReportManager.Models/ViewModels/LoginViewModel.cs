using System.ComponentModel.DataAnnotations;

namespace ReportManager.Models.ViewModels;
public class LoginViewModel
{
    [Required(ErrorMessage = "Введите логин")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}