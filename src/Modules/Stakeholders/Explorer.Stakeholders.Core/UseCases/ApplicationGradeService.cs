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
    public class ApplicationGradeService : CrudService<ApplicationGradeDto, ApplicationGrade>, IApplicationGradeService
    {
        public ApplicationGradeService(ICrudRepository<ApplicationGrade> repository, IMapper mapper) : base(repository, mapper) 
        { 
        
        }

        public Result<ApplicationGradeDto> EvaluateApplication(ApplicationGradeDto applicationGrade)
        {
            applicationGrade.Created = DateTime.Now.ToUniversalTime();
            ApplicationGrade newGrade = new ApplicationGrade(applicationGrade.Rating, applicationGrade.Comment, applicationGrade.Created, applicationGrade.UserId);
            CrudRepository.Create(newGrade);
            return MapToDto(newGrade);
        }

        public Result<List<ApplicationGradeDto>> ReviewGrades(int page, int pageSize)
        {
            var grades = CrudRepository.GetPaged(page, pageSize).Results.ToList();
            return MapToDto(grades);
        }
    }
}
