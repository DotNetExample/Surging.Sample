using System.Threading.Tasks;
using Hl.BasicData.Domain;
using Hl.BasicData.IApplication;
using Hl.BasicData.IApplication.Dictionary.Dtos;
using Hl.Core.Validates;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Extensions;
using Surging.Core.Dapper.Repositories;
using Surging.Core.ProxyGenerator;

namespace Hl.BasicData.Application
{
    public class DictionaryApplication : ProxyServiceBase, IDictionaryApplication
    {
        public async Task<string> CreateDict(CreateDictInput input)
        {
            input.DataAnnotationsCheck().CheckValidResult();

            if (input.ParentId != 0)
            {
                var parentDict = await GetService<IDapperRepository<HlDictionary, long>>().FirstOrDefaultAsync(p => p.Id == input.ParentId);
                if (parentDict == null)
                {
                    throw new BusinessException($"不存在ParentId为{input.ParentId}的字典记录");
                }
                if (!parentDict.HasChild)
                {
                    throw new BusinessException($"{parentDict.Value}不允许设置子类型");
                }
                if (input.TypeName.IsNullOrEmpty())
                {
                    input.TypeName = parentDict.TypeName;
                }
            }

            var exsitDict = await GetService<IDapperRepository<HlDictionary, long>>().FirstOrDefaultAsync(p => p.Code == input.Code);
            if (exsitDict != null)
            {
                throw new BusinessException($"已经存在code为{input.Code}的字典记录");
            }
            var dict = input.MapTo<HlDictionary>();

            await GetService<IDapperRepository<HlDictionary, long>>().InsertAsync(dict);
            return "新增字典值成功";
        }

    }
}
