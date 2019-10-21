using buzzerApi.Models;
using buzzerApi.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Repository
{
    public class QuestionTexteRepository : IQuestionTexteRepository
    {
        private readonly BuzzerApiContext db;

        public QuestionTexteRepository(BuzzerApiContext dbContext)
        {
            db = dbContext;
        }

        public async Task CreateAsync(QuestionTexte question)
        {
            await db.QuestionTexte.AddAsync(question);
            db.SaveChanges();
        }

        public async Task<IEnumerable<QuestionTexte>> ListAllQuestionTexte()
        {
            return await db.QuestionTexte.ToListAsync();
        }
    }
}
