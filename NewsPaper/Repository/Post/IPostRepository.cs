using NewsPaper.ViewModel;

namespace NewsPaper.Repository.Post
{
    public interface IPostRepository
    {
        public List<NewsPaper.Models.Entities.Post> GetAll();
        public NewsPaper.Models.Entities.Post GetById(int id);
        
        public void Add(ImageViewModel vm);
        public void Update(Models.Entities.Post newPost, int id);
        public void Delete(int id);
        public void DeleteAll();

    }
}
