namespace PostSync.Core.Models;

public class UserModel
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public UserStatus Status { get; set; }
    
}

public enum UserStatus
{
    VERIFIED,
    PENDING_VERIFICATION,
    DELETED
}