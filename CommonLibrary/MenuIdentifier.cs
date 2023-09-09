namespace ST.EplAddin.CommonLibrary
{

  
        public class MenuIdentifier
        {

            public MenuIdentifier(uint menuId)
            {
                MenuId = menuId;
                AddinsInjectedQuantity++;
            }
            public MenuIdentifier(uint menuId, int AddinsInjectedQuantity)
            {
                MenuId = menuId;
                this.AddinsInjectedQuantity = AddinsInjectedQuantity;
            }
            public MenuIdentifier()
            {

            }
            public uint MenuId { get; set; }
            public int AddinsInjectedQuantity { get; set; }
        }
       

    
}
