using Hx.BgApp.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.PermissionManagement;

namespace Hx.BgApp.Layout
{
    [Authorize]
    public class ProjectAppService : BgAppAppService
    {
        protected IProjectRepository ProjectRepository { get; }
        protected IPermissionAppService PermissionAppService { get; }
        public ProjectAppService(
            IProjectRepository projectRepository, IPermissionAppService permissionAppService)
        {
            ProjectRepository = projectRepository;
            PermissionAppService = permissionAppService;
        }
        public async Task CreateAsync(ProjectCreateOrUpdateDto input)
        {
            var project = new Project(GuidGenerator.Create(), input.Title, input.Logo, input.DefaultMenuExpandedList, input.Current);
            await ProjectRepository.InsertAsync(project);
        }
        public async Task<ProjectDto> GetDetailsAsync(Guid? key = null)
        {
            var project = await ProjectRepository.GetCurrentProjectAsync(key);
            var result = ObjectMapper.Map<Project, ProjectDto>(project);
            result.Menus = ObjectMapper.Map<List<Menu>, List<MenuDto>>(project.Menus.Where(d => d.Code == "BgApp.Home").ToList());
            if (CurrentUser.Id.HasValue)
            {
                var permission = await PermissionAppService.GetAsync("U", CurrentUser.Id.Value.ToString());
                var list = permission.Groups.SelectMany(group => group.Permissions.Where(p => p.IsGranted)).ToList();
                foreach (var role in CurrentUser.Roles)
                {
                    var rolePermission = await PermissionAppService.GetAsync("R", role);
                    list = list.Union(rolePermission.Groups.SelectMany(roleGroup => roleGroup.Permissions.Where(p => p.IsGranted))).ToList();
                }
                result.Menus.AddRange(
                    project.Menus.Where(
                        menu => list.Any(d => d.Name.Equals(menu.Code)))
                    .Select(ObjectMapper.Map<Menu, MenuDto>));
            }
            return result;
        }
        public async Task<PagedResultDto<ProjectDto>> GetListAsync(GetProjectsInput input)
        {
            var list = await ProjectRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);
            var count = await ProjectRepository.GetCountAsync(input.Filter);
            return new PagedResultDto<ProjectDto>(
                count,
                ObjectMapper.Map<List<Project>, List<ProjectDto>>(list));
        }
        public async Task UpdateAsync(ProjectCreateOrUpdateDto input)
        {
            var project = await ProjectRepository.GetAsync(input.Id.Value);
            if (string.Equals(input.Title, project.Title, StringComparison.OrdinalIgnoreCase))
            {
                project.SetTitle(input.Title);
            }
            if (string.Equals(input.Logo, project.Logo, StringComparison.OrdinalIgnoreCase))
            {
                project.SetLogo(input.Logo);
            }
            if (string.Equals(input.DefaultMenuExpandedList, project.DefaultMenuExpandedList, StringComparison.OrdinalIgnoreCase))
            {
                project.SetDefaultMenuExpandedList(input.DefaultMenuExpandedList);
            }
            await ProjectRepository.UpdateAsync(project);
        }
        public async Task DeleteAsync(Guid id)
        {
            await ProjectRepository.DeleteAsync(id);
        }
        public async Task CurrentProjectAsync(Guid id)
        {
            var project = await ProjectRepository.GetCurrentProjectAsync();
            var currentProject = await ProjectRepository.GetAsync(id);
            if (currentProject.Current)
                return;
            if (project != null)
            {
                await ProjectRepository.UpdateAsync(project);
            }
            await ProjectRepository.UpdateAsync(currentProject);
        }
    }
}