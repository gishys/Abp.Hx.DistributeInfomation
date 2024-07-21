using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Hx.BgApp.Layout
{
    public class MenuAppService : BgAppAppService
    {
        protected IMenuRepository MenuRepository { get; }
        public MenuAppService(IMenuRepository menuRepository)
        {
            MenuRepository = menuRepository;
        }
        public async Task CreateAsync(MenuCreateDto input)
        {
            var menu = new Menu(
                GuidGenerator.Create(),
                input.Title, input.Icon,
                input.ProjectId,
                "",
                input.PageId,
                input.PagePath,
                0,
                input.ParentId,
                input.Disabled,
                input.Selected,
                input.Disabled);
            if (input.Children?.Count > 0)
            {
                menu.SetChildren(GetChildren(input.Children, menu.Id));
            }
            await MenuRepository.InsertAsync(menu);
        }
        private Collection<Menu> GetChildren(ICollection<MenuCreateDto> list, Guid parentId)
        {
            var result = new Collection<Menu>();
            foreach (var input in list)
            {
                var item = new Menu(
                    GuidGenerator.Create(),
                    input.Title,
                    input.Icon,
                    input.ProjectId,
                    "",
                    input.PageId,
                    input.PagePath,
                    0, parentId,
                    input.Disabled,
                    input.Selected,
                    input.Disabled);
                if (input.Children != null && input.Children?.Count > 0)
                {
                    item.SetChildren(GetChildren(input.Children, item.Id));
                }
                result.Add(item);
            }
            return result;
        }
    }
}