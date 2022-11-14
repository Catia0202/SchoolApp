using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data;
using SchoolApp.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Controllers.API
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AlunosController : Controller
    {
        private readonly IAlunosRepository _alunosRepository;
        private readonly DataContext _context;
        private readonly ITurmasRepository _turmasRepository;

        public AlunosController(IAlunosRepository alunosRepository, DataContext context, ITurmasRepository turmasRepository)
        {
           _alunosRepository = alunosRepository;
           _context = context;
           _turmasRepository = turmasRepository;
        }

        [HttpGet]
        [Route("api/Alunos/GetAlunosPorTurma/{turmaid}")]
        public async Task<IActionResult> GetAlunosbyTurma(int turmaid)
        {
            var turma = await _turmasRepository.GetByIdAsync(turmaid);
            if(turma == null)
            {
                return NotFound("Não existe nenhuma turma com esse id");
            }

            var alunos = _alunosRepository.GetAll().Where(p => p.turmaid == turmaid);
            if (!alunos.Any())
            {
                return NotFound($"Esta turma não tem alunos");
            }

            return Ok(alunos);
        }
    }
}
