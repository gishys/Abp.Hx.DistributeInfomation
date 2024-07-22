using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.BgApp.Layout
{
    public class PageAppService : BgAppAppService
    {
        protected IPageRepository PageRepository { get; }
        public PageAppService(IPageRepository pageRepository)
        {
            PageRepository = pageRepository;
        }
        public async Task CreateAsync(PageCreateDto input)
        {
            var page = new Page(input.Path, input.Title, input.Code, input.ProjectId, input.Disabled);
            await PageRepository.InsertAsync(page);
        }
    }
}