using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.BgApp.PublishInformation
{
    public class FeadbackInfoCreateDto
    {
        public Guid Id { get; set; }
        public required ICollection<ContentInfoCreateDto> FeadbackInfos { get; set; }
    }
}
