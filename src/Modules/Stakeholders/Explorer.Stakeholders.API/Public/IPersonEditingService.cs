using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public;

public interface IPersonEditingService
{
        Result<PersonDto> Create(PersonDto personDto);
        Result<PersonDto> Update(PersonDto personDto);
        Result Delete(int id);
}

