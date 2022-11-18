# FlexyPager.net

A C# class library for generating Pager for ASP.NET

![](https://raw.githubusercontent.com/adriancs2/FlexyPager.net/master/wiki/01.png)

## Introduction
This is a pager generator for ASP.NET WebForm & MVC. It's basically generates a HTML Table and it is styled/colored by CSS.

## Using the Code

![](https://raw.githubusercontent.com/adriancs2/FlexyPager.net/master/wiki/config.png)

C# Code behind examples:

```
FlexyPager fp = new FlexyPager();
fp.PageUrlFormat = "Default.aspx?page={p}";
fp.TotalRecords = 1000;
fp.TotalRecordsPerPage = 100;
fp.TotalSlots = 10;
fp.CurrentPage = 1;
fp.CssClass = "pager_simple_orange";
fp.CssClassCurrentPage = "active";
fp.FirstPageDisplayText = "First";
fp.LastPageDisplayText = "Last";

string pagerHtml = fp.GetHtml();
```
Example of rendering result:

![](https://raw.githubusercontent.com/adriancs2/FlexyPager.net/master/wiki/sample2.png)

This is the generated HTML Table from above code:

```
<div class="pager_simple_orange">
    <table>
        <tr>
            <td class=""><a href="Default.aspx?page=1">First</a></td>
            <td class="active"><a href="Default.aspx?page=1">1</a></td>
            <td><a href="Default.aspx?page=2">2</a></td>
            <td><a href="Default.aspx?page=3">3</a></td>
            <td><a href="Default.aspx?page=4">4</a></td>
            <td><a href="Default.aspx?page=5">5</a></td>
            <td><a href="Default.aspx?page=6">6</a></td>
            <td><a href="Default.aspx?page=7">7</a></td>
            <td><a href="Default.aspx?page=8">8</a></td>
            <td><a href="Default.aspx?page=9">9</a></td>
            <td><a href="Default.aspx?page=10">10</a></td>
            <td class=""><a href="Default.aspx?page=100">Last</a></td>
        </tr>
    </table>
</div>
```
You'll noticed that the table is wrapped inside a DIV with the CSS Class "pager_simple_orange". This is the content of the CSS Class:

```
.pager_simple_orange td{
    padding: 1px;
}

.pager_simple_orange a{
    text-decoration: none;
    font: 12px Arial;
    font-weight: normal;
    color: black;
    display: block;
    padding: 5px 10px;
    border: 1px solid #fe8c00;
    background-color: #ffd9b4;
}

.pager_simple_orange .active a, 
.pager_simple_orange a:hover{
    border: 1px solid black;
    background-color: #fe8c00;
}
```

## Page URL Formatting
This refers to the URL address (Link) of the page numbers.

{p} will be replaced by page numbers.

Example URL 1: http://www.mywebsite.com/product/page-1

Code:

```
fp.PageUrlFormat = "product/page-{p}";
// or
fp.PageUrlFormat = "http://www.mywebsite.com/product/page-{p}";
```

Example URL 2: http://www.mywebsite.com/searchmember.aspx?page=1

Code:

```
fp.PageUrlFormat = "searchmember.aspx?page={p}";
// or
fp.PageUrlFormat = "http://www.mywebsite.com/searchmember.aspx?page={p}";
```

Example URL 3: http://www.mywebsite.com/Search.aspx?page=1&teamid=2&locationid=3

Code:

```
fp.PageUrlFormat = "Search.aspx?page={p}&teamid=2&locationid=3";
// or
fp.PageUrlFormat = "http://www.mywebsite.com/Search.aspx?page={p}&teamid=2&locationid=3";
```

If you want to redefine the format of the generated URL (you have your own formula or maybe you want to encrypt the query string), you can modify the source code. Look for this block:

```
private string GenerateUrl(int pageNumber)
{
    return PageUrlFormat.Replace("{p}", pageNumber.ToString());
}
```
and modify it to your flavor.

## Simple CSS Customization
Here, I will introduce some simple customization of CSS for this pager. There are four entries (public string properties) in the pager that can insert CSS Class:

- CssClass
- CssClassCurrentPage
- CssClassFirstPage
- CssClassLastPage

Let's take this as example of code with FlexyPager:

```
FlexyPager fp = new FlexyPager();
fp.CssClass = "aaa";
fp.CssClassCurrentPage = "bbb";
fp.CssClassFirstPage = "ccc";
fp.CssClassLastPage = "ddd";
```

Location of the class in generated HTML (look for aaa, bbb, ccc and ddd):

```
<div class="aaa">
    <table>
        <tr>
            <td class="ccc"><a href="Default.aspx?page=1">First</a></td>
            <td class="bbb"><a href="Default.aspx?page=1">1</a></td>
            <td><a href="Default.aspx?page=2">2</a></td>
            <td><a href="Default.aspx?page=3">3</a></td>
            <td><a href="Default.aspx?page=4">4</a></td>
            <td><a href="Default.aspx?page=5">5</a></td>
            <td class="ddd"><a href="Default.aspx?page=100">Last</a></td>
        </tr>
    </table>
</div>
```
Now, let us make the link (html tag of "a") becomes a button. This is how the CSS looks like:

```
a {
    text-decoration: none;
    font: 12px Arial;
    font-weight: normal;
    color: black;
    display: block;
    padding: 5px 10px;
    border: 1px solid #fe8c00;
    background-color: #ffd9b4;
}
```
text-decoration: none - This will hide the underscore of the a (the link)

font-weight: normal - Normal, means don't bold the text (You can bold it, of course).

color: black - Color of text

display: block - Make the link (the "a") becomes a rectangular block. It becomes a button now.

padding: 5px 10px - Used to define the size of the block.

![](https://raw.githubusercontent.com/adriancs2/FlexyPager.net/master/wiki/padding.png)

border - Defines the color of the border.

background-color - Defines the color of the background of the block

If we use the above CSS code, it will change all the links (html tag of a) in the whole page to meet this behavior. This is not what we want. We only want the links (the "a") in the pager to behave like this. Therefore we need to add the CSS Class of the DIV (which wrap the table) -"aaa" before the "a".

Then, the CSS will look like this:
```
.aaa a{
    text-decoration: none;
    font: 12px Arial;
    font-weight: normal;
    color: black;
    display: block;
    padding: 5px 10px;
    border: 1px solid #fe8c00;
    background-color: #ffd9b4;
}
```
This line:
```
.aaa a
```
means: All the a within the block of aaa will behave like this, else won't.

For the mouse hover effect, add the keyword of :hover to the specific CSS Class:

```
.aaa a:hover{
    ....
    /* define some behaviour */
    ....
}
```
The CSS Class of bbb indicates the current active page. Lets make it has the same visual effect as a:hover. Multiple class can be joined together by separating it with comma:

```
.aaa a:hover, .aaa .bbb{
    ....
    /* define some behaviour */
    ....
}
```
For the FirstPage and LastPage, you can display image in stead of text. You just define it in CSS. Example:

The C# coding:
```
// assign blank value
fp.FirstPageDisplayText = "";
fp.LastPageDisplayText = "";
```
The CSS
```
.aaa .ccc a{
    display:block;
    padding: 5px 10px;
    background-image: url('/images/leftArrow.gif');
    background-repeat: no-repeat;
    background-color: white;
}

.aaa .ddd a{
    display:block;
    padding: 5px 10px;
    background-image: url('/images/rightArrow.gif');
    background-repeat: no-repeat;
    background-color: white;
}
```
With advance CSS3 elements, you can make the pager looks very fancy and beautiful. It can even animate. There are some ready made CSS examples available in the demo app. You can have a look at it.
