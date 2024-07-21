using System.Collections.Generic;

namespace Hx.BgApp.PublishInformation
{
    public class ContentInfo
    {
        public ContentInfo(string title, bool required, ContentType contentType, int sort, string value, TermType? termType = null)
        {
            Title = title;
            Required = required;
            ContentType = contentType;
            Sort = sort;
            Value = value;
            TermType = termType;
            Terms = new List<ContentTerm>();
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; protected set; }
        /// <summary>
        /// 选项集合
        /// </summary>
        public ICollection<ContentTerm> Terms { get; protected set; }
        /// <summary>
        /// 必填
        /// </summary>
        public bool Required { get; protected set; }
        /// <summary>
        /// 内容类型
        /// </summary>
        public ContentType ContentType { get; protected set; }
        /// <summary>
        /// 选项类型
        /// </summary>
        public TermType? TermType { get; protected set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; protected set; }
        /// <summary>
        /// 内容值
        /// </summary>
        public string Value { get; protected set; }
        public void SetTitle(string title)
        {
            Title = title;
        }
        public void SetRequired(bool required)
        {
            Required = required;
        }
        public void SetType(ContentType contentType)
        {
            ContentType = contentType;
        }
        public void SetSort(int sort)
        {
            Sort = sort;
        }
    }
}