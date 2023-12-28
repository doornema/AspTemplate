using template.Data;
using template.Models;
using LitJWT.Algorithms;
using LitJWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text;
using template.Interface;
using template.Utility;
using Microsoft.AspNetCore.Authorization;

namespace template.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        AppDbContext context;
        IConfiguration _configuration;
        private interface IUserResDto {
            string nam { get; set; }
            List<Post> postha {  get; set; }
            }
        public UserController(AppDbContext _context,IConfiguration configuration) { context = _context;_configuration = configuration; }
        // GET: UserController
        [HttpGet]
        [Auth("user")]
        public async  Task<IEnumerable<User>> Index()
        {
            IJwtUser? user = HttpContext.Items["User"] as IJwtUser;
          //  Console.WriteLine(user.Name);
            //var result= 
            return await context.Users.ToListAsync(); ;
            //return new JsonResult(results);
            //return a.ToList<User>();
        }

        [HttpGet("generate-Token")]
        public  string GenerateToken()
        {

            var encoder = new JwtEncoder(new HS256Algorithm(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtKey"))));

            var token = encoder.Encode(new IJwtUser() { Name = "javad", Family = "Asghari", Id = 1 }, TimeSpan.FromMinutes(30));

            return token;
            //return  "ok";
        }
        [HttpPost]
        public int Create()
        {
            User user = new User()
            {
                Name = "dasdasd",
                Family = "adada",
                Posts = new List<Post>(){
                new Post(){ Title="adada"},
                new Post(){ Title="adada"},
                new Post(){ Title="adada"},
                new Post(){ Title="adada"}
                },
            };
            user.Posts.Add(new Post() { Title = "adada" });
            user.Posts.Add(new Post() { Title = "adada" });

            //ICollection<Post> posts = new List<Post>()
            //{
            //    new Post(){ Title="adada"},
            //    new Post(){ Title="adada"},
            //    new Post(){ Title="adada"},
            //    new Post(){ Title="adada"},
            //};
            //user.Posts= posts;
            context.Users.Add(user);
            return context.SaveChanges() ;
        }
      
    }
}
