namespace WildBeard.Orders.Model
{
    public abstract class BaseAddress : BaseEntity
    {
        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }
    }
}
