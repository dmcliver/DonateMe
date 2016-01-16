using System;
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

        public virtual Guid ImageId { get; protected set; }

        public virtual UriType UriPath { get; protected set; }

        [NotMapped]
        public virtual string Path
        {
            get { return UriPath.Path; }
            set
            {
                if(UriPath == null)
                    UriPath = new UriType(value);
            }
        }

        public virtual int ItemId { get; protected set; }
        public virtual Item Item { get; protected set; }
    }
}
