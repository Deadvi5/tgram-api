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

        [HttpGet]
        public ActionResult<Movie[]> Get()
        {
            return movies;
        }

        [HttpGet("{year}")]
        public ActionResult<Movie[]> Get(int year)
        {
            return movies.Where(x=>x.year==year).ToArray();
        }

        [HttpGet("title/{title}")]
        public ActionResult<Movie[]> Title(string title)
        {
            return movies.Where(x=>x.title.ToLower().Contains(title.ToLower())).ToArray();
        }
        
        //api/Messages/actor/
        [HttpGet("actor/{actor}")]
        public ActionResult<Movie[]> Actor(string actor)
        {
            return movies.Where(x=>x.cast.Any(z=>z.ToLower().Contains(actor.ToLower()))).ToArray();
        }

        //api/Messages/actor/
        [HttpGet("appears/{actor}")]
        public ActionResult<string[]> Appears(string actor)
        {
            return movies.Where(x=>x.cast.Any(z=>z.ToLower().Contains(actor.ToLower()))).Select(m=>m.title).ToArray();
        }

        [HttpGet("genre/{genre}")]
        public ActionResult<Movie[]> Genre(string genre)
        {
            return movies.Where(x=>x.genres.Any(z=>z.ToLower().Contains(genre.ToLower()))).ToArray();
        }

        // GET api/values/5
        [HttpGet("count")]
        public ActionResult<int> Count()
        {
            return movies.Length;
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

        #region Private Members
        private Task<Movie[]> getInfo()
        {
            using (WebClient wc = new WebClient())
            {
                var json = new WebClient().DownloadString(new Uri("https://raw.githubusercontent.com/prust/wikipedia-movie-data/master/movies.json")); 
                return Task.FromResult(JsonConvert.DeserializeObject<Movie[]>(json));
            }      
        }
        #endregion
    }
}
