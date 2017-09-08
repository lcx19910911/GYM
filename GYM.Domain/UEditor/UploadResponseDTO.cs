
using GYM.Core.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM.Domain.UEditor
{
    public class UploadResponseDTO
    {
        public UploadStateCode state { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string original { get; set; }
    }
}
