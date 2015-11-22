using System.Collections.Generic;
using Microsoft.AspNet.Mvc.Rendering;

namespace HelpDeskVNext.Data.Models
{
    public interface IService<T,I>
    {
        IEnumerable<T> Get();
        IEnumerable<SelectListItem> GetList(I id = default(I));
        void Create(T item);
        T Get(I id);
        void Update(T item);
        void Delete(I id);
    }
}