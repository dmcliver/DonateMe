using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DonateMe.BusinessDomain.Entities;
using DonateMe.DataLayer.Repositories;

namespace DonateMe.Web.Controllers
{
    public class ItemController : ApiController
    {
        private readonly ItemDAO _itemDAO;

        public ItemController(ItemDAO itemDAO)
        {
            if (itemDAO == null) throw new ArgumentNullException("itemDAO");
            _itemDAO = itemDAO;
        }

        public IEnumerable<Item> Get(Guid id)
        {
            IEnumerable<Item> items = _itemDAO.GetByCategoryId(id);
            return items;
        }

        public void Post()
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            if (files.Count > 0)
            {
                HttpPostedFile file = files[0];
                using (var stream = file.InputStream)
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    using (FileStream fileStream = new FileStream("/Users/Public/" + file.FileName, FileMode.CreateNew))
                    {
                        fileStream.Write(bytes, 0, bytes.Length);
                    }
                }
            }
        }
    }
}
