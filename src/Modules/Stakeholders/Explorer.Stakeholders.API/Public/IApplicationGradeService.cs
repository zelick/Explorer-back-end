using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IApplicationGradeService 
    {
        Result<ApplicationGradeDto> EvaluateApplication(ApplicationGradeDto applicationGrade);
        Result<List<ApplicationGradeDto>> ReviewGrades(int page, int pageSize);
    }
}
