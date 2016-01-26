using System;
using System.Collections.Generic;

namespace DonateMe.Common
{
    public class KeyValueModelBinderImpl : KeyValueModelBinder
    {
        public TM BindToKeyValues<TM, TK, TV>(Dictionary<TK, TV> keyVal)
        {
            var model = Activator.CreateInstance<TM>();
            return model;
        }
    }
}
