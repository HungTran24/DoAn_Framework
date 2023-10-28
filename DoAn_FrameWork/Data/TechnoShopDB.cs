using Microsoft.EntityFrameworkCore;
namespace DoAn_FrameWork.Data
{
    public class TechnoShopDB : DbContext
    {
        public TechnoShopDB(DbContextOptions<TechnoShopDB> options) : base(options)
        {
        }
    }
}
