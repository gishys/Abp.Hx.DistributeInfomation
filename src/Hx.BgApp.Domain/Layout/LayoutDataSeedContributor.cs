using hyjiacan.py4n;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace Hx.BgApp.Layout
{
    public class LayoutDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        protected IProjectRepository ProjectRepository { get; }
        protected IMenuRepository MenuRepository { get; }
        protected IPageRepository PageRepository { get; }
        protected IGuidGenerator GuidGenerator { get; }
        public LayoutDataSeedContributor(
            IProjectRepository projectRepository,
            IMenuRepository menuRepository,
            IPageRepository pageRepository,
            IGuidGenerator guidGenerator)
        {
            ProjectRepository = projectRepository;
            MenuRepository = menuRepository;
            PageRepository = pageRepository;
            GuidGenerator = guidGenerator;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if ((await ProjectRepository.GetListAsync()).Count <= 0)
            {
                var project = new Project(GuidGenerator.Create(), "后端管理系统", "https://gw.alipayobjects.com/zos/rmsportal/KDpgvguMpGfqaHPjicRK.svg", null, true);
                await ProjectRepository.InsertAsync(project);
                var home = new Page("home", "首页", "BgApp.Home", project.Id, false);
                var usermanagement = new Page("usermanagement", "用户管理", "AbpIdentity.Users", project.Id, false);
                var rolemanagement = new Page("rolemanagement", "角色管理", "AbpIdentity.Roles", project.Id, false);
                var publishManagement = new Page("publishfeadbackmanagement", "发布信息管理", "BgApp.PublishFeadback", project.Id, false);
                var pages = new List<Page> {
                    home,
                    usermanagement,
                    rolemanagement,
                    publishManagement,
                };
                await PageRepository.InsertManyAsync(pages);
                var menus = new List<Menu> {
                    new(GuidGenerator.Create(),home.Title , "home.svg", project.Id, home.Code, home.Id, home.Path,1),
                    new(GuidGenerator.Create(),usermanagement.Title , "user_management.svg", project.Id, usermanagement.Code, usermanagement.Id, usermanagement.Path,3),
                    new(GuidGenerator.Create(),rolemanagement.Title , "role_management.svg", project.Id, rolemanagement.Code, rolemanagement.Id, rolemanagement.Path,4),
                    new(GuidGenerator.Create(),publishManagement.Title , "publish_feadback_management.svg", project.Id, publishManagement.Code, publishManagement.Id, publishManagement.Path,5),
                };
                await MenuRepository.InsertManyAsync(menus);
            }
        }
        private string GetPinyin(string str)
        {
            var pyList = new List<string>();
            foreach (var hz in str)
            {
                pyList.AddRange(Pinyin4Net.GetPinyin(hz));
            }
            return string.Join("", pyList);
        }
    }
}