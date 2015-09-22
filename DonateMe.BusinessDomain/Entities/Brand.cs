namespace DonateMe.BusinessDomain.Entities
{
    public class Brand
    {
        public Brand(string name)
        {
            Name = name;
        }

        protected Brand()
        {
        }

        public int BrandId { get; private set; }
        public string Name { get; private set; }
    }
}
