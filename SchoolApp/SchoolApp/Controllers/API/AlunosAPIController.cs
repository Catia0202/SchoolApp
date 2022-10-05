using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Data;

namespace SchoolApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosAPIController : Controller
    {
        private readonly IAlunosRepository _alunosRepository;

        public AlunosAPIController(IAlunosRepository alunosRepository)
        {
           _alunosRepository = alunosRepository;
        }

        [HttpGet]
        public IActionResult GetAlunos()
        {
            return Ok(_alunosRepository.GetAll());
        }
    }
}
