using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Data;

namespace SchoolApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AlunosController : Controller
    {
        private readonly IAlunosRepository _alunosRepository;
      

        public AlunosController(IAlunosRepository alunosRepository)
        {
           _alunosRepository = alunosRepository;
          
        }

        [HttpGet]
        public IActionResult GetAlunos()
        {
            return Ok(_alunosRepository.GetAllWithUsers());
        }
    }
}
