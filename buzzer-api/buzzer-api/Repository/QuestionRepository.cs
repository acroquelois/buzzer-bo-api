using buzzerApi.Models;
using buzzerApi.Repository.Abstraction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public IEnumerable<Question> ListAllQuestionTexte()
        {
            return db.Question.ToList();
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
            catch(Exception ex)
            {
                return false;
            }
        }

        public Question ListOneQuestionTexte(Guid id)
        {
            var question = db.Question.FirstOrDefault(p => p.Id == id);
            return question;
        }

        public Question ListRandomQuestionTexte()
        {
            List<Question> question_list = new List<Question>();
            question_list.AddRange(db.Question);
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
    }
}
