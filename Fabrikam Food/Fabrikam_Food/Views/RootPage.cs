using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fabrikam_Food
{
    public class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            var menuPage = new MenuPage();
            // Menu ページの ListView Menu を選択した時に NavigateTo メソッドに SelectedItem を渡します。
            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);

            Master = menuPage;

            // Detail は NavigationPage で呼び出した方が良いです。(左からのスワイプで Master を出せるが操作しづらい)
            // バーの色を変えています。
            var detail = new NavigationPage(new MapsPage());
            detail.BarBackgroundColor = Color.FromHex("3498DB");
            detail.BarTextColor = Color.White;
            Detail = detail;

        }

        /// <summary>
        /// ページ遷移のメソッドです。
        /// </summary>
        /// <param name="menu">MenuItem</param>
        void NavigateTo(MenuItem menu)
        {
            // menuPage の List<MenuItem> の選択値を MenuItem で受け取っているので
            // 予め定義されたページに移動できるってことは分かるんですが、凄いですね。
            Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);

            // 同じく各ページに移動する時にもバーの色を再設定 (このやり方では必須)
            var detail = new NavigationPage(displayPage);
            detail.BarBackgroundColor = Color.FromHex("3498DB");
            detail.BarTextColor = Color.White;
            Detail = detail;

            IsPresented = false;
        }
    }
}
