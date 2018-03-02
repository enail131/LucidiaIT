using LucidiaIT.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LucidiaIT.Models
{
    public abstract class Identifiable : IIdentifiable
    {
        public abstract int ID { get; set; }
    }
}
