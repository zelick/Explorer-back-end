using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class User : Entity
{
	public string Username { get; private set; }
    public string Password { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; set; }
    public List<Club> Clubs { get; set; }
    public bool IsVerified { get;  set; }


    public User(string username, string password, UserRole role, bool isActive, bool isVerified)
	{
		Username = username;
		Password = password;
		Role = role;
		IsActive = isActive;
        IsVerified = isVerified;
        Validate();
    }


	private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Invalid Surname");
    }

    public string GetPrimaryRoleName()
    {
        return Role.ToString().ToLower();
    }
}

public enum UserRole
{
    Administrator,
    Author,
    Tourist
}