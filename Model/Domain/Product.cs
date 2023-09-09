namespace SnS.API.Model.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }

        public Guid CategorytId { get; set; }

        //Navigation Property
        public Category Category { get; set; }


    }
}
