using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class PersonEditingService : CrudService<PersonDto, Person>, IPersonEditingService
    {
        public PersonEditingService(ICrudRepository<Person> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
