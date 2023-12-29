<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/TestSite/Default.aspx) (VB: [Default.aspx](./VB/TestSite/Default.aspx))
* [Default.aspx.cs](./CS/TestSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/TestSite/Default.aspx.vb))
<!-- default file list end -->
# Custom client-side logic implementation in the grid with multi-row editing


<p><strong>UPDATED:</strong><br /><br />Starting with version 13.2, the ASPxGridView control offers the basic "Batch Editing Mode" functionality that allows accomplishing a similar task with less effort and does not require so much extra code. See the <a href="https://community.devexpress.com/blogs/aspnet/archive/2013/12/16/asp-net-webforms-amp-mvc-gridview-batch-edit-what-39-s-new-in-13-2.aspx">ASP.NET WebForms & MVC: GridView Batch Edit </a>blog post to learn more about this new functionality.<br /><br />Starting with version 14.1, the ASPxGridView control offers advanced "Batch Editing Mode" programming options.<br /><br />You can find a standalone DB-independent solution in our Code Examples base at:<br /><a href="https://www.devexpress.com/Support/Center/p/E5045">ASPxGridView - A simple Batch Editing implementation</a><br /><br />If you have version v14.1+ available, consider using the built-in functionality instead of the approach detailed below.<br />If you need further assistance with this functionality, please create a new ticket in our Support Center.<br /><br />Custom logic implemented in this example allows the end-user to select a limited number of named columns via comboboxes. The maximum number of columns the user can select is defined by a numeric column value, which he can also change. The only callback is used to post the entire grid page data.<br /> To work in multi-row-edit mode, scripts and ClientInstanceName property values are generated programmatically.</p>
<p><strong>See Also:</strong><br /> <a href="https://www.devexpress.com/Support/Center/p/E158">ASPxGridView - Multi-Row Editing</a><br /> <a href="https://www.devexpress.com/Support/Center/p/E324">How to implement the multi-row editing feature in the ASPxGridView</a><br /> <a href="https://www.devexpress.com/Support/Center/p/E2333">How to perform ASPxGridView instant updating using different editors in the DataItem template</a></p>

<br/>


