
using NewsPaper.Models.Entities;
namespace NewsPaper.Repository.Department
{
    public interface IDepartmentRepository
    {
        public List<NewsPaper.Models.Entities.Department> GetAll();

        public NewsPaper.Models.Entities.Department GetById(int id);

        public void Delete(int id);
        public void Update(Models.Entities.Department dept, int id);
        public void add(Models.Entities.Department dept);
    }
}
