using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.BgApp.Layout
{
    public class MenuDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        /// 是否可选
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Display { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 页Id
        /// </summary>
        public Guid PageId { get; set; }
        /// <summary>
        /// 页导航路径
        /// </summary>
        public string PagePath { get; set; }
        /// <summary>
        /// 项目Id
        /// </summary>
        public Guid ProjectId { get; set; }
        /// <summary>
        /// 菜单代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 父菜单Id
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 子菜单集合
        /// </summary>
        public ICollection<MenuDto> Children { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int SerialNumber { get; set; }
    }
}
