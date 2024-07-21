using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.BgApp.PublishInformation
{
    public enum TermType
    {
        /// <summary>
        /// 单选
        /// </summary>
        Radio = 1,
        /// <summary>
        /// 多选
        /// </summary>
        MultipleChoise = 2,
        /// <summary>
        /// 问答
        /// </summary>
        SingleCloze = 3,
        /// <summary>
        /// 多项填空
        /// </summary>
        MultipleCloze = 4,
        /// <summary>
        /// 排序
        /// </summary>
        Sort = 5,
    }
}
