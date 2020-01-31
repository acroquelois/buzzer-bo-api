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
            try
            {
                db.Question.Add(question);
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task UpdateQuestion(Question question)
        {
            db.Question.Update(question);
            await db.SaveChangesAsync();
        }

        public async Task<Question> GetQuestion(Guid id)
        {
            var question = await db.Question
                .Include(p => p.Propositions)
                .Include(p => p.QuestionType)
                .Include(p => p.MediaQuestions)
                .FirstOrDefaultAsync(p => p.Id == id);
            return question;
        }


        public async Task<IEnumerable<Question>> ListAllQuestion()
        {
            return await db.Question
                .Include(p => p.Propositions)
                .Include(p => p.QuestionType)
                .Include(p => p.MediaQuestions)
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> ListAllQuestionTexte()
        {
            return await db.Question
                .Include(p => p.Propositions)
                .Include(p => p.QuestionType)
                .Include(p => p.MediaQuestions)
                .Where(p => p.QuestionTypeId == "TEXTE")
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> ListAllQuestionImage()
        {
            return await db.Question
                .Include(p => p.Propositions)
                .Include(p => p.QuestionType)
                .Include(p => p.MediaQuestions)
                .Where(p => p.QuestionTypeId == "IMAGE")
                .ToListAsync();
        }

        public async Task<IEnumerable<Question>> ListAllQuestionAudio()
        {
            return await db.Question
                .Include(p => p.Propositions)
                .Include(p => p.QuestionType)
                .Include(p => p.MediaQuestions)
                .Where(p => p.QuestionTypeId == "AUDIO")
                .ToListAsync();
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
