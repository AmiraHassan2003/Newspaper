
using Microsoft.EntityFrameworkCore;
using NewsPaper.Models.Context;
//using NewsPaper.Models.Entities;
namespace NewsPaper.Repository.Department
{
    public class DepartmentRepository : IDepartmentRepository
    {
        NewspaperContext context;
        public DepartmentRepository(NewspaperContext context) 
        {
            this.context = context;
        }
        

        public List<Models.Entities.Department> GetAll()
        {
            return context.departments.Include(d=>d.authors).ToList(); 
        }

        public Models.Entities.Department GetById(int id)
        {
            return context.departments.FirstOrDefault(d=>d.Id == id);
        }

        public void Update(Models.Entities.Department newDept, int id)
        {
            var dept = this.GetById(id);
            dept.Name = newDept.Name;
            dept.BirthDate = newDept.BirthDate ?? DateTime.Now;
            dept.authors = newDept.authors;
            context.departments.Update(dept);
            context.SaveChanges();
        }

        public void add(Models.Entities.Department newDept)
        {
            Models.Entities.Department dept = new Models.Entities.Department();
            dept.Name = newDept.Name;
            dept.BirthDate = newDept.BirthDate ?? DateTime.Now;
            dept.authors = newDept.authors;
            context.departments.Add(dept);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var dept = this.GetById(id);
            context.departments.Remove(dept);
            context.SaveChanges();
        }
    }
}
