using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;
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
