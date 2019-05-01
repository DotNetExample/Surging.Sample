using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;

namespace Hl.Identity.Domain.Authorization.Users
{
    public class UserManager : ManagerBase, IUserManager
    {
        private readonly IDapperRepository<UserInfo,long> _userRepository;
        public UserManager(IDapperRepository<UserInfo, long> userRepository)
        {
            _userRepository = userRepository;
        }


    }
}
