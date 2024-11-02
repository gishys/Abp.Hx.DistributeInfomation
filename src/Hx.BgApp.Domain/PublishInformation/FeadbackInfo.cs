using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Domain.Values;

namespace Hx.BgApp.PublishInformation
{
    [Owned]
    public class FeadbackInfo : ValueObject
    {
        public FeadbackInfo() { }
        public FeadbackInfo(
            int sort,
            string name,
            int sex, string certificateNumber,
            int age,
            string phone,
            int livingEnvironmentScore,
            string livingEnvironmentPics,
            int observeLawScore,
            string observeLawPics,
            int neighborsScore,
            string neighborsPics,
            int familyTraditionScore,
            string familyTraditionPics,
            int gettingRichScore,
            string gettingRichPics,
            int policyImplementationScore,
            string policyImplementationPics,
            int publicSpiritedScore,
            string publicSpiritedPics,
            int civilizedScore,
            string civilizedPics,
            string extraBonus,
            string oneVoteVeto,
            int totalScore,
            DateTime createTime)
        {
            Sort = sort;
            Name = name;
            Sex = sex;
            CertificateNumber = certificateNumber;
            Age = age;
            Phone = phone;
            LivingEnvironmentScore = livingEnvironmentScore;
            LivingEnvironmentPics = livingEnvironmentPics;
            ObserveLawScore = observeLawScore;
            ObserveLawPics = observeLawPics;
            NeighborsScore = neighborsScore;
            NeighborsPics = neighborsPics;
            FamilyTraditionScore = familyTraditionScore;
            FamilyTraditionPics = familyTraditionPics;
            GettingRichScore = gettingRichScore;
            GettingRichPics = gettingRichPics;
            PolicyImplementationScore = policyImplementationScore;
            PolicyImplementationPics = policyImplementationPics;
            PublicSpiritedScore = publicSpiritedScore;
            PublicSpiritedPics = publicSpiritedPics;
            CivilizedScore = civilizedScore;
            CivilizedPics = civilizedPics;
            ExtraBonus = extraBonus;
            OneVoteVeto = oneVoteVeto;
            TotalScore = totalScore;
            CreateTime = createTime;
        }
        public int Sort { get; protected set; }
        public string Name { get; protected set; }
        public int Sex { get; protected set; }
        public string CertificateNumber { get; protected set; }
        public int Age { get; protected set; }
        public string Phone { get; protected set; }
        /// <summary>
        /// 人居环境
        /// </summary>
        public int LivingEnvironmentScore { get; protected set; }
        public string LivingEnvironmentPics { get; protected set; }
        /// <summary>
        /// 遵纪守法
        /// </summary>
        public int ObserveLawScore { get; protected set; }
        public string ObserveLawPics { get; protected set; }
        /// <summary>
        /// 家和邻亲
        /// </summary>
        public int NeighborsScore { get; protected set; }
        public string NeighborsPics { get; protected set; }
        /// <summary>
        /// 家风建设
        /// </summary>
        public int FamilyTraditionScore { get; protected set; }
        public string FamilyTraditionPics { get; protected set; }
        /// <summary>
        /// 兴业致富
        /// </summary>
        public int GettingRichScore { get; protected set; }
        public string GettingRichPics { get; protected set; }
        /// <summary>
        /// 政策执行
        /// </summary>
        public int PolicyImplementationScore { get; protected set; }
        public string PolicyImplementationPics { get; protected set; }
        /// <summary>
        /// 热心公益
        /// </summary>
        public int PublicSpiritedScore { get; protected set; }
        public string PublicSpiritedPics { get; protected set; }
        /// <summary>
        /// 文明新风
        /// </summary>
        public int CivilizedScore { get; protected set; }
        public string CivilizedPics { get; protected set; }
        /// <summary>
        /// 额外加分项
        /// </summary>
        public string ExtraBonus { get; protected set; }
        /// <summary>
        /// 一票否决项
        /// </summary>
        public string OneVoteVeto { get; protected set; }
        /// <summary>
        /// 总分
        /// </summary>
        public int TotalScore { get; protected set; }
        public DateTime CreateTime { get; protected set; }
        public void SetSort(int sort)
        {
            Sort = sort;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return new object[] {
            Sort ,
            Name ,
            Sex ,
            CertificateNumber ,
            Age ,
            Phone,
            LivingEnvironmentScore ,
            LivingEnvironmentPics ,
            ObserveLawScore ,
            ObserveLawPics ,
            NeighborsScore ,
            NeighborsPics ,
            FamilyTraditionScore ,
            FamilyTraditionPics ,
            GettingRichScore ,
            GettingRichPics ,
            PolicyImplementationScore ,
            PolicyImplementationPics ,
            PublicSpiritedScore ,
            PublicSpiritedPics ,
            CivilizedScore ,
            CivilizedPics ,
            ExtraBonus ,
            OneVoteVeto ,
            TotalScore ,
            CreateTime ,
            };
        }
    }
}