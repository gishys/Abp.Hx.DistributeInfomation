using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.BgApp.Layout
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Logo Url
        /// </summary>
        public string? Logo { get; set; }
        /// <summary>
        /// 默认菜单展开列表
        /// </summary>
        public string? DefaultMenuExpandedList { get; set; }
        /// <summary>
        /// 当前项目
        /// </summary>
        public bool Current { get; set; }
        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<MenuDto> Menus { get; set; }
        /// <summary>
        /// 页面列表
        /// </summary>
        public ICollection<PageDto> Pages { get; set; }
    }
}