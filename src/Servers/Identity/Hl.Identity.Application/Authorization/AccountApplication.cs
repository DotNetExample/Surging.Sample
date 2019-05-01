using Hl.Core.Validates;
using Hl.Identity.Domain.Authorization.Users;
using Hl.Identity.IApplication.Authorization;
using Hl.Identity.IApplication.Authorization.Dtos;
using Hl.Identity.IApplication.Authorization.Validators;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.ProxyGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hl.Identity.Application.Authorization
{
    [ModuleName("v1identity",Version = "v1")]
    public class AccountApplication : ProxyServiceBase, IAccountApplication
    {
        private readonly UserManager _userManager;


        public AccountApplication(UserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<LoginResult> Login(LoginInput input)
        {
            return new LoginResult() {
                ResultType = LoginResultType.Success,
                PayLoad = new Dictionary<string, object>() { { "userId", 1} },
            };
        }

    }
}
