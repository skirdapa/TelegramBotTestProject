using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SQLite;
using Telegram.Bot.Types;

namespace TelegramBotTestProject.Data.Table;

public class Users
{
    public enum UserRoles
    {
        User, Admin
    }
    
    [PrimaryKey] 
    public long ID { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }

    [Required] 
    public string? UserName { get; set; }

    [DefaultValue(UserRoles.User)]
    public UserRoles Role { get; set; }

    public override string ToString()
    {
        return $"ID: {ID}, name: {FirstName}, lastname: {LastName}, tg: {UserName}, role: {Role}";
    }
}