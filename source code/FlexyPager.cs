using System;
using System.Collections.Generic;
using System.Web;
using System.Text;

namespace System.Web
{
    public class FlexyPager
    {
        private StringBuilder sb = null;
        private string tab1 = "    ";
        private string tab2 = "        ";
        private string tab3 = "            ";
        private string tab4 = "                ";

        private string _cssClass = "";
        private string _cssClassCurrentPage = "";
        private string _cssClassFirstPage = "";
        private string _cssClassLastPage = "";

        private int _totalRecords = 0;
        private int _totalSlots = 10;
        private int _currentPage = 1;
        private int _totalRecordsPerPage = 20;

        private string _firstPageDisplayText = "First";
        private string _lastPageDisplayText = "Last";

        private string _pageUrlFormat = "";

        private List<int> slots = new List<int>();
        private int currentPageSlotIndex = 0;

        public string CssClass { get { return _cssClass; } set { _cssClass = value; } }
        public string CssClassCurrentPage { get { return _cssClassCurrentPage; } set { _cssClassCurrentPage = value; } }
        public string CssClassFirstPage { get { return _cssClassFirstPage; } set { _cssClassFirstPage = value; } }
        public string CssClassLastPage { get { return _cssClassLastPage; } set { _cssClassLastPage = value; } }

        public int TotalRecords { get { return _totalRecords; } set { _totalRecords = value; } }
        public int TotalSlots { get { return _totalSlots; } set { _totalSlots = value; } }
        public int CurrentPage { get { return _currentPage; } set { _currentPage = value; } }
        public int TotalRecordsPerPage { get { return _totalRecordsPerPage; } set { _totalRecordsPerPage = value; } }

        /// <summary>
        /// Gets the total pages.
        /// </summary>
        public int TotalPages
        {
            get
            {
                if (_totalRecordsPerPage == 0 || _totalRecords == 0)
                    return 0;

                if (_totalRecords <= _totalRecordsPerPage)
                    return 1;

                int totalPages = _totalRecords / _totalRecordsPerPage;
                int remainingRecords = _totalRecords % _totalRecordsPerPage;
                if (remainingRecords > 0)
                    totalPages = totalPages + 1;
                return totalPages;
            }
        }

        public string FirstPageDisplayText { get { return _firstPageDisplayText; } set { _firstPageDisplayText = value; } }
        public string LastPageDisplayText { get { return _lastPageDisplayText; } set { _lastPageDisplayText = value; } }

        /// <summary>
        /// Gets or Sets the URL format on each page number. Example: "Default.aspx?page={p}". {p} will be replaced by the correspondence page number.
        /// </summary>
        public string PageUrlFormat
        {
            get
            {
                if (_pageUrlFormat == "")
                {
                    string url = HttpContext.Current.Request.Url.PathAndQuery;
                    string url2 = (url.Split('?'))[0];
                    return url2 + "?page={p}";
                }
                return _pageUrlFormat;
            }
            set { _pageUrlFormat = value; }
        }

        public FlexyPager()
        { }

        public string GetHtml()
        {
            sb = new StringBuilder();

            WriteHeader();

            GetSlots();
            BuildSlots();

            WriteFooter();

            return sb.ToString();
        }

        private void GetSlots()
        {
            slots = new List<int>();

            int totalPage = TotalPages;

            if (_currentPage > totalPage)
                _currentPage = totalPage;
            if (_currentPage < 1)
                _currentPage = 1;

            // Condition 1
            if (totalPage < _totalSlots)
            {
                for (int i = 0; i < totalPage; i++)
                {
                    int pageNumber = i + 1;
                    slots.Add(pageNumber);
                    if (pageNumber == _currentPage)
                        currentPageSlotIndex = slots.Count - 1;
                }
                return;
            }



            int centerSlot = 0;
            int leftSlots = 0;
            int rightSlots = 0;

            centerSlot = _totalSlots / 2;
            int remainingSlot = _totalSlots % 2;
            if (remainingSlot > 0)
                centerSlot = centerSlot + 1;

            leftSlots = centerSlot - 1;
            rightSlots = _totalSlots - centerSlot;

            int leftFirstSlotPageNumber = _currentPage - leftSlots;
            int rightFirstSlotPageNumber = _currentPage + rightSlots;

            // Condition 2
            if (leftFirstSlotPageNumber > 0 && rightFirstSlotPageNumber <= totalPage)
            {
                for (int i = leftFirstSlotPageNumber; i <= rightFirstSlotPageNumber; i++)
                {
                    slots.Add(i);
                    if (i == _currentPage)
                        currentPageSlotIndex = slots.Count - 1;
                }
                return;
            }

            // Condition 3
            if (leftFirstSlotPageNumber <= 1)
            {
                for (int i = 0; i < _totalSlots; i++)
                {
                    int pageNumber = i + 1;
                    slots.Add(pageNumber);
                    if (pageNumber == _currentPage)
                        currentPageSlotIndex = slots.Count - 1;
                }

                return;
            }

            // Condition 4
            leftFirstSlotPageNumber = totalPage - _totalSlots + 1;
            for (int i = 0; i < _totalSlots; i++)
            {
                int pageNumber = leftFirstSlotPageNumber + i;
                slots.Add(pageNumber);
                if (pageNumber == _currentPage)
                    currentPageSlotIndex = slots.Count - 1;
            }
        }

        private void BuildSlots()
        {
            sb.Append(tab3);
            sb.AppendLine("<tr>");

            sb.Append(tab4);
            sb.Append("<td class=\"");
            sb.Append(_cssClassFirstPage);
            sb.Append("\"><a href=\"");

            // Get URL
            sb.Append(GenerateUrl(1));

            sb.Append("\">");
            sb.Append(_firstPageDisplayText);
            sb.AppendLine("</a></td>");

            for (int i = 0; i < slots.Count; i++)
            {
                sb.Append(tab4);
                sb.Append("<td");

                if (i == currentPageSlotIndex)
                {
                    sb.Append(" class=\"");
                    sb.Append(_cssClassCurrentPage);
                    sb.Append("\"");
                }

                sb.Append("><a href=\"");

                // Get URL
                sb.Append(GenerateUrl(slots[i]));

                sb.Append("\">");
                sb.Append(slots[i]);
                sb.Append("</a>");

                sb.AppendLine("</td>");
            }

            sb.Append(tab4);
            sb.Append("<td class=\"");
            sb.Append(_cssClassLastPage);
            sb.Append("\"><a href=\"");

            // Get URL
            sb.Append(GenerateUrl(TotalPages));

            sb.Append("\">");
            sb.Append(_lastPageDisplayText);
            sb.AppendLine("</a></td>");

            sb.Append(tab3);
            sb.AppendLine("</tr>");
        }

        private string GenerateUrl(int pageNumber)
        {
            return PageUrlFormat.Replace("{p}", pageNumber.ToString());
        }

        private void WriteHeader()
        {
            sb.AppendLine();
            sb.Append(tab1);
            sb.Append("<div class=\"");
            sb.Append(_cssClass);
            sb.AppendLine("\">");

            sb.Append(tab2);
            sb.AppendLine("<table>");
        }

        private void WriteFooter()
        {
            sb.Append(tab2);
            sb.AppendLine("</table>");

            sb.Append(tab1);
            sb.AppendLine("</div>");
            sb.AppendLine();
        }
    }
}