using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.BgApp.PublishInformation
{
    public class FeadbackCreateDto
    {
        public required string Name { get; set; }
        public required string Sex { get; set; }
        public required string CertificateNumber { get; set; }
        public int Age { get; set; }
        public required string Phone { get; set; }
        /// <summary>
        /// 人居环境
        /// </summary>
        public int LivingEnvironmentScore { get; set; }
        public string? LivingEnvironmentPics { get; set; }
        /// <summary>
        /// 遵纪守法
        /// </summary>
        public int ObserveLawScore { get; set; }
        public string? ObserveLawPics { get; set; }
        /// <summary>
        /// 家和邻亲
        /// </summary>
        public int NeighborsScore { get;  set; }
        public string? NeighborsPics { get;  set; }
        /// <summary>
        /// 家风建设
        /// </summary>
        public int FamilyTraditionScore { get;  set; }
        public string? FamilyTraditionPics { get;  set; }
        /// <summary>
        /// 兴业致富
        /// </summary>
        public int GettingRichScore { get;  set; }
        public string? GettingRichPics { get;  set; }
        /// <summary>
        /// 政策执行
        /// </summary>
        public int PolicyImplementationScore { get;  set; }
        public string? PolicyImplementationPics { get;  set; }
        /// <summary>
        /// 热心公益
        /// </summary>
        public int PublicSpiritedScore { get;  set; }
        public string? PublicSpiritedPics { get;  set; }
        /// <summary>
        /// 文明新风
        /// </summary>
        public int CivilizedScore { get;  set; }
        public string? CivilizedPics { get;  set; }
        /// <summary>
        /// 额外加分项
        /// </summary>
        public required string ExtraBonus { get;  set; }
        /// <summary>
        /// 一票否决项
        /// </summary>
        public required string OneVoteVeto { get;  set; }
        /// <summary>
        /// 总分
        /// </summary>
        public int TotalScore { get;  set; }
    }
}
