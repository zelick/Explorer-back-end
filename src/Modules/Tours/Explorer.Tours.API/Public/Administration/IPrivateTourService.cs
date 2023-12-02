using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IPrivateTourService
    {
        Result<List<PrivateTourDto>> GetAllByTourist(long id);
        Result<PrivateTourDto> Add(PrivateTourDto dto);
    }
}
