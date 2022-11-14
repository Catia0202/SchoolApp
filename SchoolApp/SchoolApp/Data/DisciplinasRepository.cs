using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public class DisciplinasRepository : GenericRepository<Disciplina>, IDisciplinasRepository
    {
        private readonly DataContext _context;
        public DisciplinasRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboDisciplinasporTurmaAsync(int turmaid)
        {
            var list = _context.turmaDisciplina.Include(p => p.Disciplina).Where(a => a.TurmaId == turmaid).Select(p => new SelectListItem
            {
                Text = p.Disciplina.Nome,
                Value = p.Disciplina.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Selecione uma Disciplina...)",
                Value = "0"
            });
            return list;
        }

        public async Task<IQueryable<Disciplina>> GetIndexTurmasDisciplinasAsync(int turmaid)
        {
            var disciplinas = Enumerable.Empty<Disciplina>().AsQueryable();

            await Task.Run(() =>
            {

                disciplinas = _context.turmaDisciplina.Include(p => p.Disciplina).Where(a => a.TurmaId == turmaid).Select(p => new Disciplina
                {
                    Nome = p.Disciplina.Nome,
                    Descrição = p.Disciplina.Descrição,
                    Duracao = p.Disciplina.Duracao
                });
              
                });
                return disciplinas;


        }

        public List<SelectListItem> GetListDisciplinas()
        {
            var list = _context.Disciplina.ToList();
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach (var item in list)
            {
                lista.Add(new SelectListItem
                {
                    Text = item.Nome,
                    Value = item.Id.ToString()

                });
            }
            return lista;
        }


 
        //public object GetListDisciplinas()
        //{
        //    var list = _context.Disciplina.Select(p => new List<Disciplina>
        //    {
        //       new Disciplina
        //       {
        //           Nome = p.Nome,
        //           Descrição = p.Descrição,
        //           Id = p.Id,
        //           Duration = p.Duration

        //       }

        //    }).ToList();
        //    return list;

        //}
    }
}
