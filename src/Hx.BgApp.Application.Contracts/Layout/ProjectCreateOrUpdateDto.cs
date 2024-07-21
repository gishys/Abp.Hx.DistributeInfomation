using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hx.BgApp.Layout
{
    public class ProjectCreateOrUpdateDto
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(ProjectConsts.MaxTitleLength)]
        public string Title { get; set; }
        /// <summary>
        /// Logo Url
        /// </summary>
        [StringLength(ProjectConsts.MaxLogoLength)]
        public string? Logo { get; set; }
        /// <summary>
        /// 默认菜单展开列表
        /// </summary>
        [StringLength(ProjectConsts.MaxDefaultMenuExpandedListLength)]
        public string? DefaultMenuExpandedList { get; set; }
        /// <summary>
        /// 当前项目
        /// </summary>
        public bool Current { get; set; } = false;
    }
}