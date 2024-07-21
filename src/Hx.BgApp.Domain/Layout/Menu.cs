using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hx.BgApp.Layout
{
    public class Menu : CreationAuditedEntity<Guid>
    {
        public Menu() { }
        public Menu(
            Guid id,
            string title,
            string icon,
            Guid projectId,
            string code,
            Guid pageId,
            string pagePath,
            int serialNumber,
            Guid? parentId = null,
            bool disabled = false,
            bool selected = true,
            bool display = true)
        {
            Id = id;
            Disabled = disabled;
            Selected = selected;
            Title = title;
            Icon = icon;
            ProjectId = projectId;
            Code = code;
            PageId = pageId;
            PagePath = pagePath;
            SerialNumber = serialNumber;
            Children = new Collection<Menu>();
            ParentId = parentId;
            Display = display;
        }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disabled { get; protected set; }
        /// <summary>
        /// 是否可选
        /// </summary>
        public bool Selected { get; protected set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Display { get; protected set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; protected set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; protected set; }
        /// <summary>
        /// 页Id
        /// </summary>
        public Guid PageId { get; protected set; }
        /// <summary>
        /// 页导航路径
        /// </summary>
        public string PagePath { get; protected set; }
        /// <summary>
        /// 项目Id
        /// </summary>
        public Guid ProjectId { get; protected set; }
        /// <summary>
        /// 菜单代码
        /// </summary>
        public string Code { get; protected set; }
        /// <summary>
        /// 父菜单Id
        /// </summary>
        public Guid? ParentId { get; protected set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int SerialNumber {  get; protected set; }
        /// <summary>
        /// 子菜单集合
        /// </summary>
        public ICollection<Menu> Children { get; protected set; }
        public void SetDisabled(bool disabled) { Disabled = disabled; }
        public void SetSelected(bool selected) { Selected = selected; }
        public void SetTitle(string title) { Title = title; }
        public void SetIcon(string icon) { Icon = icon; }
        public void SetPage(Guid pageId, string pagePath) { PageId = pageId; PagePath = pagePath; }
        public void SetChildren(Collection<Menu> children)
        {
            Children = children;
        }
    }
}