using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.BgApp.Common
{
    public class AttachmentDownloandDto
    {
        public string Name { get; set; }
        public string StoreName { get; set; }
        public string Url { get; set; }
        public string StaticFileUrl { get; set; }
        public Guid Id { get; set; }
    }
}
