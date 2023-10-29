using DoAn_FrameWork.Models;

namespace DoAn_FrameWork.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TechnoShop_DBContext _context;
        public CategoryRepository(TechnoShop_DBContext context)
        {
            _context = context;
        }
        public Category Add(Category dmsp)
        {
            _context.Categories.Add(dmsp);
            _context.SaveChanges();
            return dmsp;
        }

        public Category Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Category Get(int id)
        {
            return _context.Categories.Find(id);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public Category Update(Category dmsp)
        {
            _context.Update(dmsp);
            _context.SaveChanges();
            return dmsp;
        }
    }
}
