﻿using Hl.Core.Commons.Dtos;
using Hl.Core.ServiceApi;
using Hl.Core.Validates;
using Hl.Identity.Domain.Authorization.UserGroups;
using Hl.Identity.IApplication.UserGroups;
using Hl.Identity.IApplication.UserGroups.Dtos;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.Dapper.Repositories;
using Surging.Core.ProxyGenerator;
using System;
using System.Threading.Tasks;

namespace Hl.Identity.Application.UserGroups
{
    [ModuleName(ApiConsts.Identity.ServiceKey, Version = "v1")]
    public class UserGroupApplication : ProxyServiceBase, IUserGroupApplication
    {
        private readonly IDapperRepository<UserGroup, long> _userGroupRepository;
        private readonly IUserGroupManager _userGroupManager;
        public UserGroupApplication(IDapperRepository<UserGroup, long> userGroupRepository,
            IUserGroupManager userGroupManager)
        {
            _userGroupRepository = userGroupRepository;
            _userGroupManager = userGroupManager;
        }
        public async Task<string> Create(CreateUserGroupInput input)
        {
            await CheckUserGroupInput(input);
            var existUserGroup = await _userGroupRepository.FirstOrDefaultAsync(p => p.GroupName == input.GroupName);
            if (existUserGroup != null)
            {
                throw new BusinessException($"已经存在{input.GroupName}的用户组");
            }
            var userGroupEntity = input.MapTo<UserGroup>();
            await _userGroupRepository.InsertAsync(userGroupEntity);
            return "新增用户组成功";
        }

        public async Task<string> Update(UpdateUserGroupInput input)
        {
            if (input.Id == input.ParentId)
            {
                throw new BusinessException($"Id与ParentId不允许相等");
            }

            await CheckUserGroupInput(input);
            var userGroup = await _userGroupRepository.SingleOrDefaultAsync(p => p.Id == input.Id);
            if (userGroup == null)
            {
                throw new BusinessException($"不存在Id为{input.Id}的用户组");
            }
       
            if (userGroup.GroupName != input.GroupName)
            {
                var existUserGroup = await _userGroupRepository.FirstOrDefaultAsync(p => p.GroupName == input.GroupName);
                if (existUserGroup != null)
                {
                    throw new BusinessException($"已经存在{input.GroupName}的用户组");
                }
            }
            userGroup = input.MapTo(userGroup);
            await _userGroupRepository.UpdateAsync(userGroup);
            return "更新用户组成功";
        }

        public async Task<string> Delete(DeleteByIdInput input)
        {
            await _userGroupManager.DeleteUserGroupById(input.Id);
            return "删除用户组成功";
        }


        private async Task CheckUserGroupInput(UserGroupDtoBase input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            if (input.ParentId != 0)
            {
                var parentUserGroup = await _userGroupRepository.SingleOrDefaultAsync(p => p.Id == input.ParentId);
                if (parentUserGroup == null)
                {
                    throw new BusinessException($"不存在父Id为{input.ParentId}的用户组");
                }
            }
           
        }
    }
}
