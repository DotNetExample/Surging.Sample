using System.Threading.Tasks;
using Hl.Core.Commons.Dtos;
using Hl.Core.ServiceApi;
using Hl.Core.Validates;
using Hl.Identity.Domain.Authorization.Menus;
using Hl.Identity.Domain.Authorization.Permissions;
using Hl.Identity.IApplication.Menus;
using Hl.Identity.IApplication.Menus.Dtos;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.Dapper.Repositories;
using Surging.Core.ProxyGenerator;

namespace Hl.Identity.Application.Menus
{
    [ModuleName(ApiConsts.Identity.ServiceKey, Version = "v1")]
    public class MenuApplication : ProxyServiceBase, IMenuApplication
    {
        private readonly IMenuManager _menuManager;
        private readonly IDapperRepository<Menu, long> _menuRepository;
        private readonly IDapperRepository<Permission, long> _permissionRepository;
        private readonly IDapperRepository<Function, long> _functionRepository;
        private readonly IFunctionManager _functionManager;
        public MenuApplication(IMenuManager menuManager,
            IDapperRepository<Menu, long> menuRepository,
            IDapperRepository<Permission, long> permissionRepository,
            IDapperRepository<Function, long> functionRepository,
            IFunctionManager functionManager)
        {
            _menuManager = menuManager;
            _menuRepository = menuRepository;
            _permissionRepository = permissionRepository;
            _functionRepository = functionRepository;
            _functionManager = functionManager;
        }

        public async Task<string> CreateFunction(CreateFunctionInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var existFunc = await _functionRepository.SingleOrDefaultAsync(p => p.Code == input.Code);
            if (existFunc != null)
            {
                throw new BusinessException($"系统中已经存在Code为{input.Code}的功能信息");
            }
            var menu = await _menuRepository.SingleOrDefaultAsync(p => p.Id == input.MenuId);
            if (menu == null)
            {
                throw new BusinessException($"系统中不存在Id为{input.MenuId}的菜单信息");
            }
            if (input.ParentId != 0)
            {
                var parentFunc = await _functionRepository.SingleOrDefaultAsync(p => p.Id == input.ParentId);
                if (parentFunc == null)
                {
                    throw new BusinessException($"系统中已经不存在id为{input.ParentId}的父功能信息");
                }
            }
            var function = input.MapTo<Function>();
            var permission = new Permission()
            {
                Code = input.Code,
                Name = input.Name,
                Memo = input.Memo,
                Mold = PermissionMold.Operate
            };
            await _functionManager.CreateFunction(function, permission, input.MenuId);
            return "新增功能成功";
        }

        public async Task<string> CreateMenu(CreateMenuInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var exsitMenu = await _menuRepository.SingleOrDefaultAsync(p => p.Code == input.Code);
            if (exsitMenu != null)
            {
                throw new BusinessException($"系统中已经存在Code为{input.Code}的菜单信息");
            }
            var exsitPermission = await _permissionRepository.SingleOrDefaultAsync(p => p.Code == input.Code);
            if (exsitPermission != null)
            {
                throw new BusinessException($"系统中已经存在Code为{input.Code}的权限信息");
            }
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

        public async Task<string> DeleteMenu(DeleteByIdInput input)
        {
            await _menuManager.DeleteMenu(input.Id);
            return "删除菜单成功";
        }

        public async Task<string> UpdateMenu(UpdateMenuInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var menu = await _menuRepository.SingleOrDefaultAsync(p => p.Id == input.Id);
            if (menu == null)
            {
                throw new BusinessException($"不存在Id为{input.Id}的菜单信息");
            }
            menu = input.MapTo(menu);
            var permission = await _permissionRepository.GetAsync(menu.PermissionId);
            permission.Memo = input.Memo;
            await _menuManager.UpdateMenu(menu, permission);
            return "更新菜单成功";
        }
    }
}
