using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM.Core.Model
{
    /// <summary>
    /// 映射用的查询键值对
    /// </summary>
    public class QueryKeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    /// <summary>
    /// 映射用的查询flag值对
    /// </summary>
    public class QueryFlagValue
    {
        public long Flag { get; set; }
        public string Value { get; set; }
    }
}
