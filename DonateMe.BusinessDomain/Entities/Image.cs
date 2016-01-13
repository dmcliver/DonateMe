using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonateMe.BusinessDomain.Entities
{
    public class Image
    {
        public Image(Guid imageId, UriType uriPath, Item item)
        {
            if (uriPath == null) throw new ArgumentNullException("uriPath");
            if (item == null) throw new ArgumentNullException("item");
            if (imageId == null) throw new ArgumentNullException("imageId");

            ImageId = imageId;
            UriPath = uriPath;
            Item = item;
            ItemId = item.ItemId;
        }

        protected Image()
        {
        }

        public Guid ImageId { get; private set; }
        
        public UriType UriPath { get; private set; }
        
        public int ItemId { get; private set; }
        public Item Item { get; private set; }
    }
}
