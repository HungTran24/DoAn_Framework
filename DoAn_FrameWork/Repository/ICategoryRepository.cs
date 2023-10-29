using DoAn_FrameWork.Models;
using JetBrains.Annotations;

namespace DoAn_FrameWork.Repository
{
    public interface ICategoryRepository
    {
        Category Add(Category dmsp);
        Category Update(Category dmsp);
        Category Delete(int id);
        Category Get(int id);
        IEnumerable<Category> GetAllCategories();
    }
}
