namespace ProjectStoreBlazor.Server.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int DateTime { get; set; }

        public int MyProperty { get; set; }

        public virtual int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
