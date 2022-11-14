using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;

namespace SchoolApp.Data
{
    public class ConfiguracaoRepository : GenericRepository<Configuracao>, IConfiguracaoRepository
    {
        private readonly DataContext _context;
        public ConfiguracaoRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}
