using NewsPaper.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace NewsPaper.Repository.Author
{
    public class AuthorRepository:IAuthorRepository
    {
        NewspaperContext context;
        public AuthorRepository(NewspaperContext context) 
        {
            this.context = context;
        }

        public void Add(Models.Entities.Author newAuth)
        {
            Models.Entities.Author auth = new Models.Entities.Author();
            //var dept = context.departments.Include(d=>d.authors).FirstOrDefault(x => x.Id == 12);
            auth.Name = newAuth.Name;
            auth.BirthDate = newAuth.BirthDate?? DateTime.Now;
            auth.TitleJob = newAuth.TitleJob;
            auth.Gender = newAuth.Gender ?? 0;
            auth.Salary = newAuth.Salary ?? 0;
            auth.Address = newAuth.Address ?? "Not found";
            auth.departmentId = newAuth.departmentId;
            auth.posts = newAuth.posts;
            context.authors.Add(auth);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var auth = GetById(id);
            context.authors.Remove(auth);
            context.SaveChanges();
        }

        public List<Models.Entities.Author> GetAll()
        {
            return context.authors.Include(d => d.posts).Include(d => d.department).ToList();
        }

        public Models.Entities.Author GetById(int id)
        {
            return context.authors.FirstOrDefault(a=>a.Id == id);
        }

        public void Update(Models.Entities.Author newAuth, int id)
        {
            Models.Entities.Author auth = context.authors.FirstOrDefault(a=>a.Id == id);
            //var dept = context.departments.Include(d => d.authors).FirstOrDefault(x => x.Id == 12);
            auth.Name = newAuth.Name;
            auth.BirthDate = newAuth.BirthDate?? DateTime.Now;
            auth.TitleJob = newAuth.TitleJob;
            auth.Gender = newAuth.Gender ?? 0;
            auth.Salary = newAuth.Salary ?? 0;
            auth.Address = newAuth.Address ?? "Not found";
            auth.departmentId = newAuth.departmentId;
            auth.posts = newAuth.posts;
            context.authors.Update(auth);
            context.SaveChanges();
        }
    }
}
