using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.BgApp.PublishInformation
{
    public class ContentInfoDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// 必填
        /// </summary>
        public bool Required { get; set; }
        /// <summary>
        /// 内容类型
        /// </summary>
        public ContentType ContentType { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 内容值
        /// </summary>
        public required string Value { get; set; }
    }
}
