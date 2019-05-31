using Hl.Core.Commons.Dtos;
using Hl.Identity.IApplication.Menus.Dtos;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using System.Threading.Tasks;

namespace Hl.Identity.IApplication.Menus
{
    [ServiceBundle("v1/api/permission/{service}")]
    public interface IMenuApplication : IServiceKey
    {
        Task<string> CreateMenu(CreateMenuInput input);

        Task<string> UpdateMenu(UpdateMenuInput input);

        Task<string> DeleteMenu(DeleteByIdInput input);

        Task<CreateFunctionOutput> CreateFunction(CreateFunctionInput input);

        Task<string> UpdateFunction(UpdateFunctionInput input);

        Task<string> CreateOperation(CreateOperationInput input);

        Task<string> UpdateOperation(UpdateOperationInput input);
    }
}
