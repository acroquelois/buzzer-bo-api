﻿using buzzerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Repository.Abstraction
{
    public interface IQuestionRepository
    {
        Task CreateAsync(Question question);
        Task<IEnumerable<Question>> ListAllQuestionTexte();
        Task<bool> DeleteAsync(Guid id);
    }
}
