using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using LeaderboardAPI.Models;

namespace LeaderboardAPI.Controllers
{
    [Route("api/[controller]")]
    public class RecordsController : Controller
    {
        private LeaderboardAPIContext _context;

        public RecordsController(LeaderboardAPIContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Record> Get()
        {
            return _context.Records.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Record result;
            
            try {
                result = _context.Records.Single(m => m.ID == id);
                
                if (result == null)
                {
                    return NotFound();
                }
                return new ObjectResult(result);
            } catch (InvalidOperationException ) {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Record value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            _context.Records.Add(value);
            return CreatedAtRoute("Get", new { controller = "Records", id = value.ID }, value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Record value)
        {
            // This method should be idempotent
            if (value == null || value.ID != id)
            {
                return BadRequest();
            }
            
            if (ModelState.IsValid)
            {
                _context.Update(value);
                _context.SaveChanges();
                
                return new NoContentResult();
            } else {
                return BadRequest();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Record resource = _context.Records.Single(m => m.ID == id);
            _context.Records.Remove(resource);
            _context.SaveChanges();
        }
    }
}
