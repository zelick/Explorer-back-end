using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System.Net.Mail;

namespace Explorer.Stakeholders.Core.UseCases;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly ICrudRepository<Person> _personRepository;

    public AuthenticationService(IUserRepository userRepository, ICrudRepository<Person> personRepository, ITokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
        _personRepository = personRepository;
    }

    public Result<AuthenticationTokensDto> Login(CredentialsDto credentials)
    {
        var user = _userRepository.GetActiveByName(credentials.Username);
        if (user == null || credentials.Password != user.Password) return Result.Fail(FailureCode.NotFound);

        long personId;
        try
        {
            personId = _userRepository.GetPersonId(user.Id);
        }
        catch (KeyNotFoundException)
        {
            personId = 0;
        }
        return _tokenGenerator.GenerateAccessToken(user, personId);
    }

    public Result<AuthenticationTokensDto> RegisterTourist(AccountRegistrationDto account)
    {
        if(_userRepository.Exists(account.Username)) return Result.Fail(FailureCode.NonUniqueUsername);

        try
        {
            if (!IsValidName(account.Name) || !IsValidName(account.Surname))
                return Result.Fail("Name and Surname must not be empty and must start with an uppercase letter");

            if (!IsValidEmail(account.Email))
                return Result.Fail("Invalid email format");

            Domain.UserRole userRole;

            if (account.Role.Equals("Administrator"))
            {
                userRole = Domain.UserRole.Administrator;
            }
            else if (account.Role.Equals("Author"))
            {
                userRole = Domain.UserRole.Author;
            }
            else userRole = Domain.UserRole.Tourist;

            var user = _userRepository.Create(new User(account.Username, account.Password, userRole , true));
            var person = _personRepository.Create(new Person(user.Id, account.Name, account.Surname, account.Email, account.ProfilePictureUrl, account.Biography, account.Motto));
            /*if(userRole.Equals(UserRole.Tourist))
            {
                var customer = new Customer(user.Id);
                _customerRepository.Create(customer);
            }*/
            return _tokenGenerator.GenerateAccessToken(user, person.Id);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    private bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && char.IsUpper(name[0]);
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}