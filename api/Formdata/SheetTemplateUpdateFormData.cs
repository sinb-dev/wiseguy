using System.Collections.Generic;

namespace wiseguy {
    public class SheetTemplateUpdateFormData : SheetTemplateNewFormData {
        public int Id {get;set;}
        public new SheetTemplate GetSheetTemplate() 
        {
            var tpl = base.GetSheetTemplate();
            tpl.Id = Id;
            return tpl;
        }
    }

    
}