using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
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
        // See https://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/supporting-odata-query-options
        // .NET Core 1.1 doesn't have actual OData controller support, so we're going to wing it for now...
        // Note, I'm not using "select", nor "inlinecount" at this time.
        /**
         * BIG WARNING: Never simply inject user input into a query. Sanitize it first. This is a test example, so whatevs.
         */
        [HttpGet]
        public IEnumerable<Record> Get(int skip, int top, string select, string orderby, bool inlinecount, string filter)
        {
            if (top < 1) {
                top = 25; // Default to 25 records if no top value is specified. We don't want to dump the entire DB!
            }
            var result = _context.Records.Skip(skip).Take(top);
            
            if (!String.IsNullOrEmpty(orderby)) {
                Console.WriteLine("...orderby is " + orderby);
                // ...I don't have time for inflection...gonna just hard code this for now, clean it up later.
                switch (orderby)
                {
                    case "CompetitionName":
                        result = (System.Linq.IOrderedQueryable<LeaderboardAPI.Models.Record>)result.OrderByDescending(s => s.CompetitionName);
                        break;
                    case "TeamName":
                        result = (System.Linq.IOrderedQueryable<LeaderboardAPI.Models.Record>)result.OrderByDescending(s => s.TeamName);
                        break;
                    case "UserNames":
                        result = (System.Linq.IOrderedQueryable<LeaderboardAPI.Models.Record>)result.OrderByDescending(s => s.UserNames);
                        break;
                    case "Score":
                        result = (System.Linq.IOrderedQueryable<LeaderboardAPI.Models.Record>)result.OrderByDescending(s => s.Score);
                        break;
                    case "ScoreFirstSubmittedDate":
                        result = (System.Linq.IOrderedQueryable<LeaderboardAPI.Models.Record>)result.OrderByDescending(s => s.ScoreFirstSubmittedDate);
                        break;
                    case "NumSubmissions":
                        result = (System.Linq.IOrderedQueryable<LeaderboardAPI.Models.Record>)result.OrderByDescending(s => s.NumSubmissions);
                        break;
                    default:
                        Console.WriteLine("EEP! Unknown orderby: " + orderby);
                        break;
                }
            }
            
            if (!String.IsNullOrEmpty(filter)) {
                Regex reg = new Regex(@"(\w+)\:([a-zA-Z0-9\.\- \&\,]+)(?:,?)", RegexOptions.IgnoreCase);
                Match m = reg.Match(filter);

                while (m.Success) {

                    string key = m.Groups[1].Value;
                    string value = m.Groups[2].Value;

                    Console.WriteLine(key + " is " + value);
                    // ...I don't have time for inflection...gonna just hard code this for now, clean it up later.
                    switch (key)
                    {
                        case "CompetitionName":
                            result = (System.Linq.IOrderedQueryable<LeaderboardAPI.Models.Record>)result.Where(s => s.CompetitionName.Contains(value));
                            break;
                        case "TeamName":
                            result = (System.Linq.IOrderedQueryable<LeaderboardAPI.Models.Record>)result.Where(s => s.TeamName.Contains(value));
                            break;
                        case "UserNames":
                            result = (System.Linq.IOrderedQueryable<LeaderboardAPI.Models.Record>)result.Where(s => s.UserNames.Contains(value));
                            break;
                        case "Score":
                            result = (System.Linq.IOrderedQueryable<LeaderboardAPI.Models.Record>)result.Where(s => s.Score.Equals(value));
                            break;

                        // Not implementing for now, as this requires special date/time handling features...
                        // case "ScoreFirstSubmittedDate":
                            // result = (System.Linq.IOrderedEnumerable<LeaderboardAPI.Models.Record>)result.Where(s => s.ScoreFirstSubmittedDate.Equals(value));
                            // break;
                        case "NumSubmissions":
                            result = (System.Linq.IOrderedQueryable<LeaderboardAPI.Models.Record>)result.Where(s => s.NumSubmissions.Equals(value));
                            break;
                        default:
                            Console.WriteLine("EEP! Unknown filter: " + key);
                            break;
                    }
                    
                    m = m.NextMatch();
                }
            }
            
            return result.ToList();
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
