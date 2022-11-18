using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;

namespace SchoolApp.Data
{
    public class AlertaRepository : GenericRepository<Alerta>, IAlertaRepository
    {
        public AlertaRepository(DataContext context):base(context)
        {

        }
    }
}
