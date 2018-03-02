using LucidiaIT.Models;
using LucidiaIT.Models.PartnerModels;

namespace LucidiaIT.Services
{
    public class PartnerService : DataService<Partner>
    {
        public PartnerService (PartnerContext context) : base (context) { }
    }
}
