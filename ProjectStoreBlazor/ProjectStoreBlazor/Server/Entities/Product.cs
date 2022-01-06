using System.ComponentModel.DataAnnotations;

namespace ProjectStoreBlazor.Server.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description  { get; set; }
        public byte[] Image { get; set; }
        public bool IsAvailable { get; set; }
        public int CreatedByUserId { get; set; }
        public decimal Price { get; set; }

       // public virtual int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
