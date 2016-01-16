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

        public virtual int BrandId { get; protected set; }
        public virtual string Name { get; protected set; }
    }
}
