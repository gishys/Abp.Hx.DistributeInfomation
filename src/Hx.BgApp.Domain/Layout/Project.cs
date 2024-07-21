using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hx.BgApp.Layout
{
    public class Project : CreationAuditedEntity<Guid>
    {
        public Project() { }
        public Project(Guid id, string title, string? logo, string? defaultMenuExpandedList, bool current)
        {
            Id = id;
            Title = title;
            Logo = logo;
            DefaultMenuExpandedList = defaultMenuExpandedList;
            Menus = new ObservableCollection<Menu>();
            Pages = new ObservableCollection<Page>();
            Current = current;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; protected set; }
        /// <summary>
        /// Logo Url
        /// </summary>
        public string? Logo { get; protected set; }
        /// <summary>
        /// 默认菜单展开列表
        /// </summary>
        public string? DefaultMenuExpandedList { get; protected set; }
        public bool Current { get; protected set; }
        public ICollection<Menu> Menus { get; protected set; }
        public ICollection<Page> Pages { get; protected set; }
        public void SetTitle(string title) { Title = title; }
        public void SetLogo(string logo) { Logo = logo; }
        public void SetDefaultMenuExpandedList(string defaultMenuExpandedList) { DefaultMenuExpandedList = defaultMenuExpandedList; }
        public void SetCurrentProject()
        {
            Current = !Current;
        }
    }
}