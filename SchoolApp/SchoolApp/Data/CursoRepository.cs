using Microsoft.AspNetCore.Mvc.Rendering;
using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public class CursoRepository : GenericRepository<Curso>, ICursoRepository
    {
        private readonly DataContext _context;

        public CursoRepository(DataContext context):base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboCursos()
        {
            var listacursos = new List<SelectListItem>();
            
                listacursos = _context.Cursos.Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            listacursos.Insert(0, new SelectListItem
            {
                Text = "Selecione um Curso",
                Value = "0"
            });
            return listacursos;
        }

        public async Task<IQueryable<Curso>> GetIndexCursoAsync()
        {
            var cursos = Enumerable.Empty<Curso>().AsQueryable();

            await Task.Run(() =>
            {
                cursos = _context.Cursos.OrderBy(x => x.Nome).Select(x => new Curso
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Descricao =x.Descricao,
                    Duracao=x.Duracao,
                    Fotourl=x.Fotourl

                });
            });
            return cursos;
        }
    }
}
