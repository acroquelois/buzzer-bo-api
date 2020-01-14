using buzzerApi.Models;
using buzzerApi.Repository.Abstraction;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly BuzzerApiContext db;

        public QuestionRepository(BuzzerApiContext dbContext)
        {
            db = dbContext;
        }

        public async Task CreateAsync(Question question)
        {
            db.Question.Add(question);
            await db.SaveChangesAsync();
        }


        public async Task<IEnumerable<Question>> ListAllQuestionTexte()
        {
            return await db.Question.ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var question = db.Question.FirstOrDefault(p => p.Id == id);
                db.Question.Remove(question);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Question> GetRandomQuestion()
        {
            try
            {
                var question = await db.Question.FirstAsync();
                return question;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //[HttpGet, Authorize]
        //public ActionResult SetSession()
        //{
        //    try
        //    {
        //        var context = HttpContext.Session;
        //        byte[] ret = Encoding.ASCII.GetBytes("true");
        //        context.Set("test",ret);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}

        //[HttpGet, Authorize]
        //public ActionResult GetSession()
        //{
        //    try
        //    {
        //        var context = HttpContext.Session;
        //        var ret = context.Get("test");
        //        return Ok(ret);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}
    }
}
