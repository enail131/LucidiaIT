using LucidiaIT.Interfaces;

namespace LucidiaIT.Models
{
    public abstract class Identifiable : IIdentifiable
    {
        public abstract int ID { get; set; }
    }
}
