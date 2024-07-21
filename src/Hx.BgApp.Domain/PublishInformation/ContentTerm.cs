using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.BgApp.PublishInformation
{
    public class ContentTerm
    {
        public ContentTerm(string title, string explain, bool? isCorrect, int sort, string value, ContentType contentType)
        {
            Title = title;
            Explain = explain;
            IsCorrect = isCorrect;
            Sort = sort;
            Value = value;
            ContentType = contentType;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; protected set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; protected set; }
        /// <summary>
        /// 正确答案
        /// </summary>
        public bool? IsCorrect { get; protected set; }
        /// <summary>
        /// 内容类型
        /// </summary>
        public ContentType ContentType { get; protected set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; protected set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; protected set; }
        public void SetTitle(string title)
        {
            Title = title;
        }
        public void SetExplain(string explain)
        {
            Explain = explain;
        }
        public void SetIsCorrect(bool? isCorrect)
        {
            IsCorrect = isCorrect;
        }
        public void SetSort(int sort)
        {
            Sort = sort;
        }
    }
}