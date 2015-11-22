using System.Threading.Tasks;

namespace HelpDeskVNext.Data.Models
{
    public class ServiceBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ServiceBase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public virtual void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }

        public virtual async Task SaveChangesAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}