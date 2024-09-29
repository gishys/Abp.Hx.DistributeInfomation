using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hx.BgApp.PublishInformation
{
    public class PersonnelInfo : FullAuditedEntity<Guid>
    {
        public PersonnelInfo() { }
        public PersonnelInfo(Guid id, string name, string sex, string certificateNumber, int age, string phone)
        {
            Id = id;
            Name = name;
            Sex = sex;
            CertificateNumber = certificateNumber;
            Age = age;
            Phone = phone;
        }
        public string Name { get; protected set; }
        public string Sex { get; protected set; }
        public string CertificateNumber { get; protected set; }
        public int Age { get; protected set; }
        public string Phone { get; protected set; }
        public void SetName(string name)
        {
            Name = name;
        }
        public void SetSex(string sex)
        {
            Sex = sex;
        }
        public void SetCertificateNumber(string certificateNumber)
        {
            CertificateNumber = certificateNumber;
        }
        public void SetAge(int age)
        {
            Age = age;
        }
        public void SetPhone(string phone)
        {
            Phone = phone;
        }
    }
}
