﻿using Hx.BgApp.PublishInformation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hx.BgApp.EntityFrameworkCore.PublishInformation
{
    public class EfCorePersonnelInfoRepository
        : EfCoreRepository<BgAppDbContext, PersonnelInfo, Guid>, IEfCorePersonnelInfoRepository
    {
        public EfCorePersonnelInfoRepository(IDbContextProvider<BgAppDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public virtual async Task<PersonnelInfo?> ExaminePersonnelExistAsync(string name, string certificateNumber)
        {
            return await (await GetDbSetAsync()).FirstOrDefaultAsync(d => d.Name == name && d.CertificateNumber == certificateNumber);
        }
        public virtual async Task<List<PersonnelInfo>> GetListAsync(string? name, string? phone)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!string.IsNullOrEmpty(name), d => d.Name.Contains(name))
                .WhereIf(!string.IsNullOrEmpty(phone), d => d.Name.Contains(phone))
                .ToListAsync();
        }
    }
}