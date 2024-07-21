using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Hx.BgApp.PublishInformation
{
    public class GetPulishFeadbackInfoInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
