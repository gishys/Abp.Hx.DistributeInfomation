using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hx.BgApp.Layout
{
    public class PageCreateDto
    {
        /// <summary>
        /// 页面的业务标识
        /// </summary>
        [MaxLength(PageConsts.MaxCodeLength)]
        public string Code { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [MaxLength(PageConsts.MaxTitleLength)]
        public string Title { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        [Required]
        [MaxLength(PageConsts.MaxPathLength)]
        public string Path { get; set; }
        public bool Disabled { get; set; } = false;
        public Guid ProjectId { get; set; }
    }
}