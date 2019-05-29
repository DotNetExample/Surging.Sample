using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.IApplication.Menus.Dtos
{
    public class CreateOperateInput
    {

        public CreateOperateInput()
        {
            Functions = new List<CreateFunctionInput>();
        }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Meno { get; set; }

        public long MenuId { get; set; }

        public ICollection<CreateFunctionInput> Functions { get; set; }
    }
}
