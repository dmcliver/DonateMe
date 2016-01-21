using System.Collections.Generic;

namespace DonateMe.Common
{
    [Injected]
    public interface IKeyValueModelBinder
    {
        TM BindToKeyValues<TM, TK, TV>(Dictionary<TK, TV> keyVal);
    }
}