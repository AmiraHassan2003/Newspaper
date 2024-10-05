using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NewsPaper.Models.Context;
using NewsPaper.Models.Entities;
using NewsPaper.Repository.Post;
using NewsPaper.ViewModel;

namespace NewsPaper.Controllers
{
    public class PostController : Controller
    {
        IPostRepository PostRepository;
        NewspaperContext context;
        public PostController(IPostRepository PostRepository, NewspaperContext context)
        {
            this.PostRepository = PostRepository;
            this.context = context;
        }
        public IActionResult GetAll()
        {
            var Posts = PostRepository.GetAll();

            return View("GetAll", Posts);
        }



        [HttpGet]
        public IActionResult UpdatePost(int id)
        {
            var post = context.posts.FirstOrDefault(p=>p.Id == id);
            //ViewBag.posts = context.posts.ToList();
            //ImageViewModel vm = new ImageViewModel();
            //vm.Id = post.Id;
            //vm.Image = CreateIFormFile(post.Image);
            //vm.PostType = post.PostType;
            //vm.CreateTime = post.CreateTime;
            //vm.AuthorId = post.AuthorId;
            ViewBag.Authors = context.authors.ToList();
            //ViewBag.Images = context.images.Include(i => i.Post).ToList();
            return View("UpdatePost", post);
        }

        [HttpPost]
        public IActionResult SavePost(Post post, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PostRepository.Update(post, id);
                    return RedirectToAction("GetAll");
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", ex.Message);
            }
            var oldPost = PostRepository.GetById(id);
            return RedirectToAction("UpdatePost");
        }

        public IActionResult CreatePost()
        {
            //Post newPost = new Post();
            ImageViewModel vm = new ImageViewModel();
          
            ViewBag.Authors = context.authors.ToList(); // Ensure authors are passed to the view
            if(context.authors.ToList() != null)
            {

            }
            //ViewBag.Images = context.images.ToList();

            return View("CreatePost", vm);
        }

        public IActionResult AddPost(ImageViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PostRepository.Add(vm);
                    var Posts = PostRepository.GetAll();
                    return RedirectToAction("GetAll", Posts);
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", ex.Message);
            }

            ViewBag.authors = context.authors.ToList();
            return RedirectToAction("CreatePost", vm);
        }

        public IActionResult DeletePost(int id) 
        {
            PostRepository.Delete(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult DeleteAllPosts() 
        {
            foreach (var post in context.posts.ToList())
            {
                PostRepository.Delete(post.Id);
            }
            return RedirectToAction("GetAll");
        }

        public IActionResult GetPostsById(int id) 
        {
            var allPosts = context.posts.Include(p=>p.author).Where(p=>p.AuthorId == id).ToList();
            return View(allPosts);
        }

    }
}
