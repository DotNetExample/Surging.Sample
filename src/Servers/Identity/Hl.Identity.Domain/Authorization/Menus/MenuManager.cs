using Hl.Identity.Domain.Authorization.Permissions;
using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Authorization.Menus
{
    public class MenuManager : ManagerBase, IMenuManager
    {
        private readonly IDapperRepository<Menu, long> _menuRepository;
        private readonly IDapperRepository<Permission, long> _permissionRepository;
        private readonly IDapperRepository<PermissionMenu, long> _permissionMenuRepository;

        public MenuManager(IDapperRepository<Menu, long> menuRepository,
            IDapperRepository<Permission, long> permissionRepository,
            IDapperRepository<PermissionMenu, long> permissionMenuRepository)
        {
            _menuRepository = menuRepository;
            _permissionMenuRepository = permissionMenuRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task CreateMenu(Menu menu, Permission permission)
        {
            await UnitOfWorkAsync(async (conn, trans) => {
                var menuId = await _menuRepository.InsertAndGetIdAsync(menu, conn, trans);
                var permissionId = await _permissionRepository.InsertAndGetIdAsync(permission, conn, trans);
                await _permissionMenuRepository.InsertAsync(new PermissionMenu() {
                    MenuId = menuId,
                    PermissionId = permissionId
                },conn,trans);
            },Connection);
        }
    }
}
