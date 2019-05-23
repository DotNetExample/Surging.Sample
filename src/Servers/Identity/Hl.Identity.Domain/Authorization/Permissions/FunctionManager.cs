using Hl.Identity.Domain.Authorization.Menus;
using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;
using System;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Authorization.Permissions
{
    public class FunctionManager : ManagerBase, IFunctionManager
    {
        private readonly IDapperRepository<Function, long> _functionRepository;
        private readonly IDapperRepository<MenuFunction, long> _menuFunctionRepository;
        private readonly IDapperRepository<PermissionFunction, long> _permissionFunctionRepository;
        private readonly IDapperRepository<Permission, long> _permissionRepository;

        public FunctionManager(IDapperRepository<Function, long> functionRepository,
            IDapperRepository<MenuFunction, long> menuFunctionRepository,
            IDapperRepository<PermissionFunction, long> permissionFunctionRepository,
            IDapperRepository<Permission, long> permissionRepository)
        {
            _functionRepository = functionRepository;
            _menuFunctionRepository = menuFunctionRepository;
            _permissionFunctionRepository = permissionFunctionRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task CreateFunction(Function function, Permission permission, long menuId)
        {
            await UnitOfWorkAsync(async (conn, trans) => {
                var funcId = await _functionRepository.InsertAndGetIdAsync(function,conn,trans);
                var permissionId = await _permissionRepository.InsertAndGetIdAsync(permission,conn,trans);
                await _menuFunctionRepository.InsertAsync(new MenuFunction() {
                    FunctionId = funcId,
                    MenuId = menuId,                   
                },conn,trans);
                await _permissionFunctionRepository.InsertAsync(new PermissionFunction() {
                    FunctionId = funcId,
                    PermissionId = permissionId
                }, conn, trans);
            }, Connection);
        }
    }
}
