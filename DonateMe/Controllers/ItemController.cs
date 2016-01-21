using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using DonateMe.BusinessDomain.Entities;
using DonateMe.Common;
using DonateMe.DataLayer.Repositories;
using WebGrease.Css.Extensions;

namespace DonateMe.Web.Controllers
{
    public class ItemController : ApiController
    {
        private readonly ItemDAO _itemDAO;
        private readonly IKeyValueModelBinder _binder;

        public ItemController(ItemDAO itemDAO, IKeyValueModelBinder binder)
        {
            if (itemDAO == null) throw new ArgumentNullException("itemDAO");
            if (binder == null) throw new ArgumentNullException("binder");

            _itemDAO = itemDAO;
            _binder = binder;
        }

        public IEnumerable<Item> Get(Guid id)
        {
            IEnumerable<Item> items = _itemDAO.GetByCategoryId(id);
            return items;
        }

        public void Post()
        {
            Request.Content.ReadAsMultipartAsync().ContinueWith(GetData);
        }

        private void GetData(Task<MultipartMemoryStreamProvider> streamProvider)
        {
            if (streamProvider.IsCompleted)
            {
                const int numberOfModelsProperties = 3;

                IList<HttpContent> allHttpContents = streamProvider.Result.Contents.ToList();

                IEnumerable<HttpContent> httpPropertyContents = allHttpContents.Take(numberOfModelsProperties);

                Dictionary<string, string> dictionary = httpPropertyContents.ToDictionary(hc => hc.Headers.ContentDisposition.Name, hc => hc.ReadAsStringAsync().Result);
                
                string itemCategory = GetDictValue(dictionary, "ItemCategory");
                string brand = GetDictValue(dictionary, "Brand");
                
                //TODO: get the brand and itemCategory entities from other DAO's to use for adding to the item entity

                IEnumerable<HttpContent> files = allHttpContents.Skip(numberOfModelsProperties);
                WriteFilesToLocal(files);

                //TODO: build image entity from files then add them to the model

                Item model = _binder.BindToKeyValues<Item, string, string>(dictionary);
                //_itemDAO.Save(model);
            }
        }

        private static string GetDictValue(Dictionary<string, string> dictionary, string key)
        {
            return dictionary.Single(kv => kv.Key.Replace("\"", string.Empty) == key).Value;
        }

        private static void WriteFilesToLocal(IEnumerable<HttpContent> files)
        {
            foreach (var file in files)
            {
                string fileName = file.Headers.ContentDisposition.FileName;
                byte[] fileData = file.ReadAsByteArrayAsync().Result;

                using (var fileStream = new FileStream("/Users/Public/" + fileName, FileMode.Create))
                    fileStream.WriteAsync(fileData, 0, fileData.Length);
            }
        }
    }
}
