using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private Movie[] movies;
        public MessagesController()
        {
            var task = Task.Run(async () => await getInfo());
            movies = task.GetAwaiter().GetResult();
        }

        // GET api/values
        [HttpGet]
        public ActionResult<Movie[]> Get()
        {
            return movies;
        }

        private Task<Movie[]> getInfo()
        {
            using (WebClient wc = new WebClient())
            {
                var json = new WebClient().DownloadString(new Uri("https://raw.githubusercontent.com/prust/wikipedia-movie-data/master/movies.json")); 
                return Task.FromResult(JsonConvert.DeserializeObject<Movie[]>(json));
            }      
        }

        // GET api/values/5
        [HttpGet("{year}")]
        public ActionResult<Movie> Get(int year)
        {
            return movies.FirstOrDefault(x=>x.year==year);
        }

        // GET api/values/5
        [HttpGet("title/{title}")]
        public ActionResult<Movie[]> Title(string title)
        {
            return movies.Where(x=>x.title.Contains(title)).ToArray();
        }

        // GET api/values/5
        [HttpGet("actor/{actor}")]
        public ActionResult<Movie[]> Actor(string actor)
        {
            return movies.Where(x=>x.cast.Any(z=>z.Contains(actor))).ToArray();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
