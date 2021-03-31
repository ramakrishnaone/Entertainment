using Entertainment.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace Entertainment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IConfiguration _configuration;

        public MoviesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Movies
        [HttpGet]
        public ActionResult<List<Movie>> Get()
        {
            string appName = _configuration.GetSection("AppSettings")["MovieListPath"];
            var serializer = new DataContractSerializer(typeof(MovieList));
            var movieList = new List<Movie>();

            using (var stringReader = new StringReader(GetContent(appName)))
            using (var xmlReader = XmlReader.Create(stringReader))
                movieList = ((MovieList)serializer.ReadObject(xmlReader)).Movies;

            return Ok(movieList);
        }

        public string GetContent(string fileName)
        {
            var executingDirectory = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath);

            return ReadContent(Path.Combine(executingDirectory, fileName));
        }

        public string ReadContent(string filePath)
        {
            string content;
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var streamReader = new StreamReader(fileStream))
            {
                content = streamReader.ReadToEnd();
                streamReader.Close();
                fileStream.Close();
            }
            return content;
        }

        // GET: api/Movies/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Movies
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
