using LucidiaIT.Data;
using LucidiaIT.Models;
using LucidiaIT.Models.PartnerModels;
using Microsoft.EntityFrameworkCore;

namespace LucidiaIT.Services
{
    public class PartnerService : DataService<Partner>
    {
        public PartnerService (PartnerContext context) : base (context) { }
    }
}
