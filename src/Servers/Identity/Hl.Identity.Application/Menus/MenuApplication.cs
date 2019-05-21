using System.Threading.Tasks;
using Hl.Core.ServiceApi;
using Hl.Core.Validates;
using Hl.Identity.Domain.Authorization.Menus;
using Hl.Identity.Domain.Authorization.Permissions;
using Hl.Identity.IApplication.Menus;
using Hl.Identity.IApplication.Menus.Dtos;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.ProxyGenerator;

namespace Hl.Identity.Application.Menus
{
    [ModuleName(ApiConsts.Identity.ServiceKey, Version = "v1")]
    public class MenuApplication : ProxyServiceBase, IMenuApplication
    {
        private readonly IMenuManager _menuManager;

        public MenuApplication(IMenuManager menuManager)
        {
            _menuManager = menuManager;
        }

        public async Task<string> CreateMenu(CreateMenuInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var menu = input.MapTo<Menu>();
            var permission = new Permission()
            {
                Code = input.Code,
                Name = input.Name,
                Memo = input.Memo,
                Mold = PermissionMold.Menu
            };
            await _menuManager.CreateMenu(menu,permission);
            return "新增菜单成功";
        }
    }
}
