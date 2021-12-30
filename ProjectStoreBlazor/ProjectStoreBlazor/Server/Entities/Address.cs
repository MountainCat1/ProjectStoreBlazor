namespace ProjectStoreBlazor.Server.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int NoOfBuilding { get; set; }
        public int NoOfFlat { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }


    }
}
