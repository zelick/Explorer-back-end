using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class PersonEditingService : CrudService<PersonDto, Person>, IPersonEditingService
    {
        private readonly IPersonRepository _personEditingRepository;
        public PersonEditingService(ICrudRepository<Person> repository, IPersonRepository personEditingRepository, IMapper mapper) : base(repository, mapper)
        {
            _personEditingRepository = personEditingRepository;   
        }

        public Result<string> GetEmail(long userId)
        {
            return _personEditingRepository.GetEmail(userId);
        }
    }
}
