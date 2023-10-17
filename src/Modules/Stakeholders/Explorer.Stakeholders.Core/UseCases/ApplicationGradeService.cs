using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ApplicationGradeService : BaseService<ApplicationGradeDto, ApplicationGrade>, IApplicationGradeService
    {
        public ApplicationGradeService(IMapper mapper) : base(mapper)
        {
        }

        public Result<ApplicationGradeDto> EvaluateApplication(ApplicationGradeDto applicationGrade)
        {
            throw new NotImplementedException();
        }

        public Result<List<ApplicationGradeDto>> ReviewGrades()
        {
            throw new NotImplementedException();
        }
    }
}
