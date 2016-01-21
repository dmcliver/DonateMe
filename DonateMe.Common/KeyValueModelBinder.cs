using System;
using System.Collections.Generic;

namespace DonateMe.Common
{
    public class KeyValueModelBinder : IKeyValueModelBinder
    {
        public TM BindToKeyValues<TM, TK, TV>(Dictionary<TK, TV> keyVal)
        {
            TM model = Activator.CreateInstance<TM>();
            return model;
        }
    }
}
