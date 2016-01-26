using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DonateMe.Common
{
    [Injected]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface KeyValueModelBinder
    {
        TM BindToKeyValues<TM, TK, TV>(Dictionary<TK, TV> keyVal);
    }
}