using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using NewsPaper.Models.Context;
using NewsPaper.Repository.Department;
using NewsPaper.ViewModel;

namespace NewsPaper.Repository.Post
{
    public class PostRepository:IPostRepository
    {
        NewspaperContext context;
        IWebHostEnvironment webHostEnvironment;
        public PostRepository(NewspaperContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public List<Models.Entities.Post> GetAll()
        {
            return context.posts.Include(p => p.author).OrderByDescending(p=>p.CreateTime).ToList();
        }

        public void Add(ImageViewModel vm)
        {
            var post = new NewsPaper.Models.Entities.Post();
            //var auth = context.authors.FirstOrDefault();
            string fileName = UploadFile(vm);
            
            post.PostType = vm.PostType ?? "Not Found";
            post.CreateTime = vm.CreateTime ?? DateTime.Now;
            post.Description = vm.Description;
            post.Image = fileName;
            post.AuthorId = vm.AuthorId;
            post.author = vm.author;
            context.posts.Add(post);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var post = this.GetById(id);
            context.posts.Remove(post);
            context.SaveChanges();
        }

        public void DeleteAll()
        {
            var allPosts = context.posts.Include(p=>p.author).ToList();
            foreach (var post in allPosts) 
            {
                this.Delete(post.Id);
            }
        }

        

        public Models.Entities.Post GetById(int id)
        {
            return context.posts.FirstOrDefault(p => p.Id == id);
        }

        private IFormFile CreateIFormFile(string imageName)
        {
            // Get the path to the image file
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/posts", imageName);

            // Read the file bytes into a memory stream
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // Create a new IFormFile object
            IFormFile formFile = new FormFile(fileStream, 0, fileStream.Length, "name", Path.GetFileName(filePath));

            return formFile;
        }

        public void Update(Models.Entities.Post newPost, int id)
        {
            Models.Entities.Post post = this.GetById(id);
            //IFormFile formFile = CreateIFormFile(post.Image);


            post.PostType = newPost.PostType ?? "Not Found";
            //post.CreateTime = newPost.CreateTime;
            post.Description = newPost.Description;
            post.Image = newPost.Image;
            post.AuthorId = newPost.AuthorId;
            post.author = newPost.author;
            context.posts.Update(post);
            context.SaveChanges();

        }

        private string UploadFile(ImageViewModel vm)
        {
            string fileName = null;

            try
            {
                if (vm.Image != null)
                {
                    // Ensure directory exists or create it
                    string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "images/posts");
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    fileName = Guid.NewGuid().ToString() + "-" + vm.Image.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        vm.Image.CopyTo(fileStream);
                    }
                    return fileName;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("File upload failed", ex);
            }

            return "";
        }

    }
}
