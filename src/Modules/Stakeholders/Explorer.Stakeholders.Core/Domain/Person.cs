using Explorer.BuildingBlocks.Core.Domain;
using System.Net.Mail;
using System.Security.Principal;

namespace Explorer.Stakeholders.Core.Domain;

public class Person : Entity
{
    public long UserId { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string ProfilePictureUrl { get; init; }
    public string Biography { get; init; }
    public string Motto { get; init; }
    public string Email { get; init; }

    public Person(long userId, string name, string surname, string email, string profilePictureUrl, string biography, string motto)
    {
        UserId = userId;
        Name = name;
        Surname = surname;
        Email = email;
        ProfilePictureUrl = profilePictureUrl;
        Biography = biography;
        Motto = motto;
        Validate();
    }

    private void Validate()
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId");
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Surname)) throw new ArgumentException("Invalid Surname");
        if (!IsValidName(Name) || !IsValidName(Surname)) throw new ArgumentException("Name and Surname should start with upper letter");
        if (!MailAddress.TryCreate(Email, out _)) throw new ArgumentException("Invalid Email");
    }

    private bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && char.IsUpper(name[0]);
    }

}