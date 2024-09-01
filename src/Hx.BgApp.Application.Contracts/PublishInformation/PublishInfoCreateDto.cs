using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.BgApp.PublishInformation
{
    public class PublishInfoCreateDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 发布
        /// </summary>
        public bool? Release { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? ReleaseDatetime { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        public ICollection<ContentInfoCreateDto>? PublishInfos { get; set; }
    }
}
