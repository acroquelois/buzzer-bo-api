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

        public async Task<Question> GetQuestion(Guid id)
        {
            var question = await db.Question.FirstOrDefaultAsync(p => p.Id == id);
            return question;
        }


        public async Task<IEnumerable<Question>> ListAllQuestionTexte()
        {
            return await db.Question
                .Include(p => p.Propositions)
                .Include(p => p.QuestionType)
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

        public async Task<Question> GetRandomQuestion()
        {
            try
            {
                List<Question> question_list = new List<Question>();
                question_list.AddRange(await this.ListAllQuestionTexte());
                Random rand = new Random();
                int random_number = rand.Next(0, question_list.Count);
                Question question = question_list[random_number];
                //Je définis une variable qui viendra segmenter le jms de ma question
                List<Guid> guid_list = new List<Guid>();
                //Je remplis cette liste avec la question récupérée mais ça merde... comment faire ?
                guid_list.Add(question.Id);
                //A l'aide de la fonction Find, je récupère l'Id de ma question dans une variable
                //Guid id_question = list.Find("Id");
                //On controle que l'id n'est pas présent dans la liste de question déjà envoyée
                //...
                //Si non, on met l'id dans la liste et on retourne la question
                //...
                //Si oui, on boucle sur les autre questions jusqu'à en trouvé une bonne
                //...
                //S'il n'y a plus de question on sort de la boucle et envoi un jms de fin, on ne retourne pas de question
                //...
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
