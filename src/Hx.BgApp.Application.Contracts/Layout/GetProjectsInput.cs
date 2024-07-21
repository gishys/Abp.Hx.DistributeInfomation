using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Hx.BgApp.Layout
{
    public class GetProjectsInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}