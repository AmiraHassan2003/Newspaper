
namespace NewsPaper.Repository.Author
{
    public interface IAuthorRepository
    {
        public List<NewsPaper.Models.Entities.Author> GetAll();

        public NewsPaper.Models.Entities.Author GetById(int id);

        public void Delete(int id);
        public void Update(Models.Entities.Author auth, int id);
        public void Add(Models.Entities.Author auth);
    }
}
