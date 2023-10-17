using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/applicationGrade")]
    public class ReviewGradeController : BaseApiController
    {
        private readonly IApplicationGradeService _applicationGradeService;

        public ReviewGradeController(IApplicationGradeService applicationGradeService)
        {
            _applicationGradeService = applicationGradeService;
        }

        [HttpGet]
        public ActionResult<List<ApplicationGradeDto>> ReviewGrades()
        {
            var result = _applicationGradeService.ReviewGrades();
            return CreateResponse(result);
        }
    }
}
