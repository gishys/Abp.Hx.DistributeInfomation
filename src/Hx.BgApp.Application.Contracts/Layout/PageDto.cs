using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.BgApp.Layout
{
    public class PageDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 页面的业务标识
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        /// 项目Id
        /// </summary>
        public Guid ProjectId { get; set; }
    }
}