using Fabrikam_Food.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabrikam_Food
{
    public class MenuListData : List<MenuItem>
    {
        public MenuListData()
        {
            this.Add(new MenuItem()
            {
                Title = "Reservations",
                IconSource = "reservations.png",
                TargetType = typeof(ReservationsPage)
            });
            this.Add(new MenuItem()
            {
                Title = "Maps",
                IconSource = "maps.png",
                TargetType = typeof(MapPage)
            });


            this.Add(new MenuItem()
            {
                Title = "Smart Helper",
                IconSource = "smarthelper.png",
                TargetType = typeof(SmartHelperPage)
            });

            this.Add(new MenuItem()
            {
                Title = "Menu List",
                IconSource = "settings.png",
                TargetType = typeof(MenuListPage)
            });
        }
    }
}
