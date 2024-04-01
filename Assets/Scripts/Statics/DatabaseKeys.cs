using System;
using System.Collections.Generic;
using Base;
using Core;

namespace Statics
{
    public static class DatabaseKeys
    {
        public static readonly string AccountDataKey = "AccountData";

        public static readonly Dictionary<(string, Type), Func<object>> DataKeyDictionary = new()
        {
            { (AccountDataKey, typeof(AccountData)), () => new AccountData(1, 100) }
        };
    }
}