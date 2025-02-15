namespace CSharp.Models;

using System.ComponentModel.DataAnnotations;

public class AuthenticateUser
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
