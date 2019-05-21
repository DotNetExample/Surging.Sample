using Hl.Core.ClassMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.Domain.Authorization.Menus.ClassMappers
{
    public class MenuFunctionMapper : HlClassMapper<MenuFunction>   
    {
        public MenuFunctionMapper()
        {
            Table("auth_menu_function");
            AutoMap();
        }
    }
}
