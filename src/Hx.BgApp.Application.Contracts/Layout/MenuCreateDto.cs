using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hx.BgApp.Layout
{
    public class MenuCreateDto
    {
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disabled { get; set; } = false;
        /// <summary>
        /// 是否可选
        /// </summary>
        public bool Selected { get; set; } = true;
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Display { get; set; } = true;
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [MaxLength(MenuConsts.MaxTitleLength)]
        public string Title { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [Required]
        [MaxLength(MenuConsts.MaxIconLength)]
        public string Icon { get; set; }
        /// <summary>
        /// 页Id
        /// </summary>
        [Required]
        public Guid PageId { get; set; }
        /// <summary>
        /// 页导航路径
        /// </summary>
        [Required]
        [MaxLength(MenuConsts.MaxPagePathLength)]
        public string PagePath { get; set; }
        /// <summary>
        /// 项目Id
        /// </summary>
        [Required]
        public Guid ProjectId { get; set; }
        /// <summary>
        /// 父菜单Id
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 子菜单集合
        /// </summary>
        public Collection<MenuCreateDto> Children { get; set; }
    }
}