using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class InternalPersonService: BaseService<PersonDto, Person>, IInternalPersonService
    {
        private readonly IPersonRepository _personRepository;

        public InternalPersonService(IMapper mapper, IPersonRepository personRepository) : base(mapper)
        {
            _personRepository = personRepository;
        }
        public PersonDto GetByUserId(int id)
        {
            try
            {
                PersonDto entity = MapToDto(_personRepository.GetByUserId(id));
                return entity;
            }
            catch (KeyNotFoundException e)
            {
                return null;
            }
            catch (ArgumentException e)
            {
                return null;
            }
        }
    }
}
