using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Hx.BgApp.PublishInformation
{
    public class PublishFeadbackInfo : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public PublishFeadbackInfo() { }
        public PublishFeadbackInfo(
            Guid id,
            string title,
            DateTime? startTime,
            DateTime? endTime,
            bool? release,
            string? description = null)
        {
            Id = id;
            Title = title;
            StartTime = startTime;
            EndTime = endTime;
            Release = release.HasValue ? release.Value : false;
            Description = description;
        }
        /// <summary>
        /// 租户
        /// </summary>
        public Guid? TenantId { get; protected set; }
        /// <summary>
        /// 发布Id
        /// </summary>
        public Guid? ParentId { get; protected set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; protected set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; protected set; }
        /// <summary>
        /// 发布
        /// </summary>
        public bool Release { get; protected set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? ReleaseDatetime { get; protected set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; protected set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; protected set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewCount { get; protected set; }
        /// <summary>
        /// 发布信息
        /// </summary>
        public ICollection<ContentInfo> PublishInfos { get; protected set; } = new List<ContentInfo>();
        /// <summary>
        /// 反馈信息
        /// </summary>
        public ICollection<FeadbackInfo> FeadbackInfos { get; protected set; } = new List<FeadbackInfo>();
        public void Publish()
        {
            ReleaseDatetime = DateTime.Now;
            Release = true;
        }
        public void AddFeadbackInfo(FeadbackInfo feadbackInfo)
        {
            FeadbackInfos.Add(feadbackInfo);
        }
    }
}