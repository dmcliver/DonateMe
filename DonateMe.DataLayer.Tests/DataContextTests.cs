using System.Collections.Generic;
using System.Linq;
using DonateMe.BusinessDomain;
using DonateMe.BusinessDomain.Entities;
using NUnit.Framework;

namespace DonateMe.DataLayer.Tests
{
    [TestFixture]
    public class DataContextTests
    {
        [Test]
        public void GeneratesDataOk()
        {
            DataContext context = new DataContext();
            
            IList<ItemCategory> categories = context.Set<ItemCategory>().ToList();

            Assert.That(categories, Is.Not.Null);
        }
    }
}
