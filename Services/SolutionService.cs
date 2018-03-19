using LucidiaIT.Models;
using LucidiaIT.Models.SolutionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LucidiaIT.Services
{
    public class SolutionService : DataService<Solution>
    {
        public SolutionService(SolutionContext context) : base (context) { }
    }
}
