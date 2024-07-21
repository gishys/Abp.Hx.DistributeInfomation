using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hx.BgApp.Layout
{
    public class Page : CreationAuditedEntity<Guid>
    {
        public Page() { }
        public Page(string path, string title, string code, Guid projectId, bool disabled)
        {
            Path = path;
            Title = title;
            Code = code;
            ProjectId = projectId;
            Disabled = disabled;
        }
        /// <summary>
        /// 页面的业务标识
        /// </summary>
        public string Code { get; protected set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; protected set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; protected set; }
        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disabled { get; protected set; }
        /// <summary>
        /// 项目Id
        /// </summary>
        public Guid ProjectId { get; protected set; }
        public void SetTitle(string title) { Title = title; }
        public void SetPath(string path) { Path = path; }
    }
}