using Hl.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hl.Identity.IApplication.Menus.Dtos
{
    public class CreateFunctionInput : FunctionDtoBase
    {
        public long MenuId { get; set; }

        [Required(ErrorMessage = "功能编码不允许为空")]
        [RegularExpression("^[a-zA-Z0-9_-]{4,16}$", ErrorMessage = "菜单编码格式不正确")]
        public string Code { get; set; }
    }
}
