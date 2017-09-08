using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using GYM.Service;
using GYM.IService;
using GYM.Domain.UEditor;
using GYM.Core.Helper;
using GYM.Core.Util;
using GYM.Core.Code;

namespace GYM.Service
{
    public class UEditorService : IUEditorService
    {

        protected HttpContext ContextCurrent { get; set; }

        public UEditorService()
        {
            this.ContextCurrent = HttpContext.Current;
        }

        public ConfigDTO Get_Config()
        {
            try
            {
                return EditorHelper.GetConfig<ConfigDTO>();
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
            }
            return null;
        }

        private UploadResponseDTO Upload_File(OSSFileCode code)
        {
            UploadResponseDTO dto = null;
            if (ContextCurrent.Request.Files.Count == 0)
            {
                dto = new UploadResponseDTO();
                return dto;
            }
            var key = Guid.NewGuid().ToString("N");
            var file = ContextCurrent.Request.Files[0];
            var fileName = file.FileName;
            string imgUrl = UploadHelper.Save(file, code.ToString());
            if (imgUrl == null)
            {
                dto = new UploadResponseDTO();
                return dto;
            }
            dto = new UploadResponseDTO();
            dto.url = imgUrl;
            dto.original = fileName;
            dto.title = fileName;
            dto.state = UploadStateCode.Success;
            return dto;
        }


        public UploadResponseDTO Upload_Image()
        {
            UploadResponseDTO dto = null;
            try
            {
                dto = Upload_File(OSSFileCode.Image);
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                dto = new UploadResponseDTO();
            }
            return dto;
        }

        public UploadResponseDTO Upload_Scrawl()
        {
            var dto = new UploadResponseDTO();

            try
            {
                var config = EditorHelper.GetConfig<ConfigDTO>();
                var base64 = ContextCurrent.Request[config.scrawlFieldName];

                var key = Guid.NewGuid().ToString("N");
                var suffix = ".png";
                var fileName = key + suffix;
                var code = OSSFileCode.Scrawl;
                string imgUrl = string.Empty;
                using (var stream = new MemoryStream(Convert.FromBase64String(base64)))
                {
                    imgUrl = UploadHelper.SaveImageStream(stream, suffix);
                    if (imgUrl == null)
                    {
                        return dto;
                    }
                }

                dto.url = imgUrl;
                dto.original = fileName;
                dto.title = fileName;
                dto.state = UploadStateCode.Success;
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                dto = new UploadResponseDTO();
            }
            return dto;
        }

        public UploadResponseDTO Upload_Video()
        {
            UploadResponseDTO dto = null;
            try
            {
                dto = Upload_File(OSSFileCode.Video);
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                dto = new UploadResponseDTO();
            }
            return dto;
        }

        public UploadResponseDTO Upload_File()
        {
            UploadResponseDTO dto = null;
            try
            {
                dto = Upload_File(OSSFileCode.File);
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                dto = new UploadResponseDTO();
            }
            return dto;
        }

        private ListResponseDTO List_File(OSSFileCode code)
        {
            //ListResponseDTO dto = new ListResponseDTO();
            //var list = OSSHelper.List(code);
            //return new ListResponseDTO
            //{
            //    list = list.Select(x => new ImageUrlDTO
            //    {
            //        url = x
            //    }).ToList(),
            //    start = 0,
            //    total = list.Count,
            //    state = UploadStateCode.Success,
            //};
            return null;
        }

        public ListResponseDTO List_Image()
        {
            ListResponseDTO dto = null;
            try
            {
                dto = List_File(OSSFileCode.Image);
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                dto = new ListResponseDTO();
            }
            return dto;
        }

        public ListResponseDTO List_File()
        {
            ListResponseDTO dto = null;
            try
            {
                dto = List_File(OSSFileCode.File);
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                dto = new ListResponseDTO();
            }
            return dto;
        }

        public CatchImageResponseDTO Catch_Image()
        {
            return new CatchImageResponseDTO();
        }
    }
}
