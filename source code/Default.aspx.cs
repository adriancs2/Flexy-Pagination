using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pager
{
    public partial class Default : System.Web.UI.Page
    {
        int totalRecords = 1000;
        int totalRecordsPerPage = 10;
        int totalSlots = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            int curPage = 1;
            int.TryParse(Request.QueryString["page"] + "", out curPage);
            if (curPage == 0)
                curPage = 1;

            FlexyPager fp = new FlexyPager();
            fp.PageUrlFormat = "Default.aspx?page={p}";
            fp.TotalRecords = totalRecords;
            fp.TotalRecordsPerPage = totalRecordsPerPage;
            fp.TotalSlots = totalSlots;
            fp.CurrentPage = curPage;
            fp.CssClass = "pager_simple_orange";
            fp.CssClassCurrentPage = "active";

            string pagerHtml = fp.GetHtml();

            string info =
@"<pre>
Total Records          : " + totalRecords + @"
Total Records Per Page : " + totalRecordsPerPage + @"
Total Slots            : " + totalSlots + @"
Total Pages            : " + fp.TotalPages + @"
Current Page           : " + fp.CurrentPage + @"
</pre>";

            PlaceHolder1.Controls.Add(new LiteralControl(info));


            PlaceHolder1.Controls.Add(new LiteralControl("<br />Pager Style 1: pager_simple_orange"));
            PlaceHolder1.Controls.Add(new LiteralControl(pagerHtml));


            fp.CssClass = "pager_simple_purple";
            fp.CssClassCurrentPage = "active";
            fp.CssClassFirstPage = "firstpage";
            fp.CssClassLastPage = "lastpage";
            fp.FirstPageDisplayText = "";
            fp.LastPageDisplayText = "";
            PlaceHolder1.Controls.Add(new LiteralControl("<br />Pager Style 2: pager_simple_purple"));
            PlaceHolder1.Controls.Add(new LiteralControl(fp.GetHtml()));

            fp.CssClassFirstPage = "";
            fp.CssClassLastPage = "";
            fp.FirstPageDisplayText = "First";
            fp.LastPageDisplayText = "Last";

            fp.CssClass = "pager_black";
            fp.CssClassCurrentPage = "active";
            PlaceHolder1.Controls.Add(new LiteralControl("<br />Pager Style 3: pager_black"));
            PlaceHolder1.Controls.Add(new LiteralControl(fp.GetHtml()));

            fp.CssClass = "pager_gray_green";
            fp.CssClassCurrentPage = "active";
            PlaceHolder1.Controls.Add(new LiteralControl("<br />Pager Style 4: pager_gray_green"));
            PlaceHolder1.Controls.Add(new LiteralControl(fp.GetHtml()));

            fp.CssClass = "pager_glass";
            fp.CssClassCurrentPage = "active";
            PlaceHolder1.Controls.Add(new LiteralControl("<br />Pager Style 5: pager_glass"));
            PlaceHolder1.Controls.Add(new LiteralControl(fp.GetHtml()));

            fp.CssClass = "pager_shadow_block";
            fp.CssClassCurrentPage = "active";
            PlaceHolder1.Controls.Add(new LiteralControl("<br />Pager Style 6: pager_shadow_block"));
            PlaceHolder1.Controls.Add(new LiteralControl(fp.GetHtml()));

            fp.CssClass = "pager_dark_gray";
            fp.CssClassCurrentPage = "active";
            PlaceHolder1.Controls.Add(new LiteralControl("<br />Pager Style 7: pager_dark_gray"));
            PlaceHolder1.Controls.Add(new LiteralControl(fp.GetHtml()));

            PlaceHolder1.Controls.Add(new LiteralControl("<br /><br /><br />"));
        }
    }
}