using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ElmahCore.Mvc.MoreLinq;

#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ElmahCore.Mvc
{
//#line 2 "..\..\ErrorLogPage.cshtml"
//#line default
    //#line hidden
    
    //#line 3 "..\..\ErrorLogPage.cshtml"
//#line default
    //#line hidden
//#line 4 "..\..\ErrorLogPage.cshtml"
//#line default
    //#line hidden
    
    //#line 5 "..\..\ErrorLogPage.cshtml"
    
    
    //#line default
    //#line hidden
    
    //#line 6 "..\..\ErrorLogPage.cshtml"

//#line default
    //#line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal partial class ErrorLogPage : WebTemplateBase
    {
//#line hidden

        //#line 218 "..\..\ErrorLogPage.cshtml"

    IHtmlString LinkHere(string basePageName, string type, string text, int pageIndex, int pageSize)
    {
        return new HtmlString(
            "<a"
            + new HtmlString(!string.IsNullOrEmpty(type) ? " rel=\"" + type + "\"" : null)
            + " href=\""
            + Encode(
                string.Format(CultureInfo.InvariantCulture, 
                    @"{0}?page={1}&size={2}",
                    basePageName,
                    pageIndex + 1,
                    pageSize))
            + "\">"
            + Encode(text)
            + "</a>"
        );
    } 

        //#line default
        //#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");








            
            //#line 8 "..\..\ErrorLogPage.cshtml"
  
    const int defaultPageSize = 15;
    const int maximumPageSize = 100;

    var basePageName = ElmahRoot;
    
    //
    // Get the page index and size parameters within their bounds.
    //

    var pageSize = Convert.ToInt32(Request.Query["size"], CultureInfo.InvariantCulture);
    pageSize = Math.Min(maximumPageSize, Math.Max(0, pageSize));

    if (pageSize == 0)
    {
        pageSize = defaultPageSize;
    }

    var pageIndex = Convert.ToInt32(Request.Query["page"], CultureInfo.InvariantCulture);
    pageIndex = Math.Max(1, pageIndex) - 1;

    //
    // Read the error records.
    //

    var log = this.ErrorLog;
    var errorEntryList = new List<ErrorLogEntry>(pageSize);
    var totalCount = log.GetErrors(pageIndex, pageSize, errorEntryList);

    //
    // Set the title of the page.
    //

    var hostName = Environment.MachineName;
    var title = string.Format(@"Error log for {0} on {1} (Page #{2})",
                              log.ApplicationName, hostName, 
                              (pageIndex + 1).ToString("N0"));

    Layout = new MasterPage.MasterPage
    {
        Context  = Context, /* TODO Consider not requiring this */
        Title    = title,
        Footnote = string.Format("This log is provided by the {0}.", log.Name),
        ElmahRoot = _elmahRoot,
        ErrorLog = ErrorLog,
        SpeedBarItems = new[] 
        {
            SpeedBar.RssFeed.Format(basePageName),
            SpeedBar.RssDigestFeed.Format(basePageName),
            SpeedBar.DownloadLog.Format(basePageName),
            SpeedBar.Help,
            SpeedBar.About.Format(basePageName),
        },
    };
    
    // If the application name matches the APPL_MD_PATH then its
    // of the form /LM/W3SVC/.../<name>. In this case, use only the 
    // <name> part to reduce the noise. The full application name is 
    // still made available through a tooltip.

    var simpleName = log.ApplicationName;

    if (string.Compare(simpleName, Request.Path,
        true, CultureInfo.InvariantCulture) == 0)
    {
        var lastSlashIndex = simpleName.LastIndexOf('/');

        if (lastSlashIndex > 0)
        {
            simpleName = simpleName.Substring(lastSlashIndex + 1);
        }
    }
        

            
            //#line default
            //#line hidden
WriteLiteral("        <h1 id=\"PageTitle\">\r\n            Error Log for <span id=\"ApplicationName\"" +
" title=\"");


            
            //#line 80 "..\..\ErrorLogPage.cshtml"
                                                       Write(log.ApplicationName);

            
            //#line default
            //#line hidden
WriteLiteral("\">");


            
            //#line 80 "..\..\ErrorLogPage.cshtml"
                                                                             Write(simpleName);

            
            //#line default
            //#line hidden
WriteLiteral(" on ");


            
            //#line 80 "..\..\ErrorLogPage.cshtml"
                                                                                            Write(hostName);

            
            //#line default
            //#line hidden
WriteLiteral("</span>\r\n        </h1>\r\n        \r\n");


            
            //#line 83 "..\..\ErrorLogPage.cshtml"
         if (errorEntryList.Count > 0)
        {
            // Write error number range displayed on this page and the
            // total available in the log, followed by stock
            // page sizes.

            var firstErrorNumber = pageIndex * pageSize + 1;
            var lastErrorNumber = firstErrorNumber + errorEntryList.Count - 1;
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            // Write out a set of stock page size choices. Note that
            // selecting a stock page size re-starts the log 
            // display from the first page to get the right paging.

            var stockSizes =
                from sizes in new[]
                {
                    new[] { 10, 15, 20, 25, 30, 50, 100, },
                }
                from size in sizes.Index()
                let separator = 
                    size.Key + 1 == sizes.Length 
                    ? null 
                    : size.Key + 2 == sizes.Length 
                    ? " or " 
                    : ", "
                select LinkHere(basePageName, HtmlLinkType.Start, size.Value.ToString("N0"), 0, size.Value)
                     + separator;
            

            
            //#line default
            //#line hidden
WriteLiteral("            <p>Errors ");


            
            //#line 112 "..\..\ErrorLogPage.cshtml"
                 Write(firstErrorNumber.ToString("N0"));

            
            //#line default
            //#line hidden
WriteLiteral(" to ");


            
            //#line 112 "..\..\ErrorLogPage.cshtml"
                                                     Write(lastErrorNumber.ToString("N0"));

            
            //#line default
            //#line hidden
WriteLiteral(" \r\n                of total ");


            
            //#line 113 "..\..\ErrorLogPage.cshtml"
                    Write(totalCount.ToString("N0"));

            
            //#line default
            //#line hidden
WriteLiteral(" \r\n                (page ");


            
            //#line 114 "..\..\ErrorLogPage.cshtml"
                  Write((pageIndex + 1).ToString("N0"));

            
            //#line default
            //#line hidden
WriteLiteral(" of ");


            
            //#line 114 "..\..\ErrorLogPage.cshtml"
                                                      Write(totalPages.ToString("N0"));

            
            //#line default
            //#line hidden
WriteLiteral("). \r\n                Start with ");


            
            //#line 115 "..\..\ErrorLogPage.cshtml"
                      Write(Html(stockSizes.ToDelimitedString(string.Empty)));

            
            //#line default
            //#line hidden
WriteLiteral(" errors per page.</p>\r\n");


            
            //#line 116 "..\..\ErrorLogPage.cshtml"

            // Write out the main table to display the errors.


            
            //#line default
            //#line hidden
WriteLiteral(@"            <table id=""ErrorLog"" cellspacing=""0"" style=""border-collapse:collapse;"" class=""table table-condensed table-striped"">
                <tr>
                    <th class=""host-col"" style=""white-space:nowrap;"">Host</th>
                    <th class=""code-col"" style=""white-space:nowrap;"">Code</th>
                    <th class=""type-col"" style=""white-space:nowrap;"">Type</th>
                    <th class=""error-col"" style=""white-space:nowrap;"">Error</th>
                    <th class=""user-col"" style=""white-space:nowrap;"">User</th>
                    <th class=""time-col"" style=""white-space:nowrap;"">When</th>
                </tr>

");


            
            //#line 129 "..\..\ErrorLogPage.cshtml"
             foreach (var error in from item in errorEntryList.Index()
                                   let e = item.Value.Error
                                   select new
                                   {
                                       Index = item.Key,
                                       item.Value.Id,
                                       e.HostName,                                                 
                                       e.StatusCode,
                                       StatusDescription = 
                                          e.StatusCode > 0
                                          ?  HttpStatus.FromCode(e.StatusCode).Reason
                                          : null,
                                       e.Type,
                                       HumaneType = ErrorDisplay.HumaneExceptionErrorType(e),
                                       e.Message,
                                       e.User,
                                       When = FuzzyTime.FormatInEnglish(e.Time),
                                       Iso8601Time = e.Time.ToString("o"),
                                   })
            {

            
            //#line default
            //#line hidden
WriteLiteral("                <tr class=\"");


            
            //#line 149 "..\..\ErrorLogPage.cshtml"
                       Write(error.Index % 2 == 0 ? "even" : "odd");

            
            //#line default
            //#line hidden
WriteLiteral("\">\r\n                    \r\n                    <td class=\"host-col\" style=\"white-s" +
"pace:nowrap;\">");


            
            //#line 151 "..\..\ErrorLogPage.cshtml"
                                                                Write(error.HostName);

            
            //#line default
            //#line hidden
WriteLiteral("</td>\r\n                    <td class=\"code-col\" style=\"white-space:nowrap;\">\r\n");


            
            //#line 153 "..\..\ErrorLogPage.cshtml"
                         if (!string.IsNullOrEmpty(error.StatusDescription))
                        {

            
            //#line default
            //#line hidden
WriteLiteral("                            <span title=\"");


            
            //#line 155 "..\..\ErrorLogPage.cshtml"
                                    Write(error.StatusDescription);

            
            //#line default
            //#line hidden
WriteLiteral("\">");


            
            //#line 155 "..\..\ErrorLogPage.cshtml"
                                                              Write(error.StatusCode);

            
            //#line default
            //#line hidden
WriteLiteral("</span>\r\n");


            
            //#line 156 "..\..\ErrorLogPage.cshtml"
                        }
                        else if (error.StatusCode != 0)
                        {
                            
            
            //#line default
            //#line hidden
            
            //#line 159 "..\..\ErrorLogPage.cshtml"
                       Write(error.StatusCode);

            
            //#line default
            //#line hidden
            
            //#line 159 "..\..\ErrorLogPage.cshtml"
                                             
                        }

            
            //#line default
            //#line hidden
WriteLiteral("                    </td>\r\n                    <td class=\"type-col\" style=\"white-" +
"space:nowrap;\"><span title=\"");


            
            //#line 162 "..\..\ErrorLogPage.cshtml"
                                                                             Write(error.Type);

            
            //#line default
            //#line hidden
WriteLiteral("\">");


            
            //#line 162 "..\..\ErrorLogPage.cshtml"
                                                                                          Write(error.HumaneType);

            
            //#line default
            //#line hidden
WriteLiteral("</span></td>\r\n                    \r\n                    <td class=\"error-col\"><sp" +
"an>");


            
            //#line 164 "..\..\ErrorLogPage.cshtml"
                                           Write(error.Message);

            
            //#line default
            //#line hidden
WriteLiteral("</span> \r\n                        <a href=\"");


            
            //#line 165 "..\..\ErrorLogPage.cshtml"
                            Write(basePageName);

            
            //#line default
            //#line hidden
WriteLiteral("/detail?id=");


            
            //#line 165 "..\..\ErrorLogPage.cshtml"
                                                    Write(error.Id);

            
            //#line default
            //#line hidden
WriteLiteral("\">Details&hellip;</a></td>\r\n                    \r\n                    <td class=\"" +
"user-col\" style=\"white-space:nowrap;\">");


            
            //#line 167 "..\..\ErrorLogPage.cshtml"
                                                                Write(error.User);

            
            //#line default
            //#line hidden
WriteLiteral("</td>\r\n                    <td class=\"time-col\" style=\"white-space:nowrap;\"><abbr" +
" title=\"");


            
            //#line 168 "..\..\ErrorLogPage.cshtml"
                                                                             Write(error.Iso8601Time);

            
            //#line default
            //#line hidden
WriteLiteral("\">");


            
            //#line 168 "..\..\ErrorLogPage.cshtml"
                                                                                                 Write(error.When);

            
            //#line default
            //#line hidden
WriteLiteral("</abbr></td>\r\n                </tr>\r\n");


            
            //#line 170 "..\..\ErrorLogPage.cshtml"
            }

            
            //#line default
            //#line hidden
WriteLiteral("            </table>\r\n");


            
            //#line 172 "..\..\ErrorLogPage.cshtml"

            // Write out page navigation links.

            //
            // If not on the last page then render a link to the next page.
            //

            var nextPageIndex = pageIndex + 1;
            var moreErrors = nextPageIndex * pageSize < totalCount;
            

            
            //#line default
            //#line hidden
WriteLiteral("            <p>\r\n\r\n");


            
            //#line 184 "..\..\ErrorLogPage.cshtml"
                 if (moreErrors)
                {
                    
            
            //#line default
            //#line hidden
            
            //#line 186 "..\..\ErrorLogPage.cshtml"
               Write(LinkHere(basePageName, HtmlLinkType.Next, "Next errors", nextPageIndex, pageSize));

            
            //#line default
            //#line hidden
            
            //#line 186 "..\..\ErrorLogPage.cshtml"
                                                                                                      
                }

            
            //#line default
            //#line hidden

            
            //#line 188 "..\..\ErrorLogPage.cshtml"
                 if (pageIndex > 0 && totalCount > 0)
                {
                    if (moreErrors) {
                        Write("; ");
                    }
                    
            
            //#line default
            //#line hidden
            
            //#line 193 "..\..\ErrorLogPage.cshtml"
               Write(LinkHere(basePageName, HtmlLinkType.Start, "Back to first page", 0, pageSize));

            
            //#line default
            //#line hidden
            
            //#line 193 "..\..\ErrorLogPage.cshtml"
                                                                                                  
                }

            
            //#line default
            //#line hidden
WriteLiteral("\r\n            </p>\r\n");


            
            //#line 197 "..\..\ErrorLogPage.cshtml"
        }
        else
        {
            // No errors found in the log, so display a corresponding
            // message.

            // It is possible that there are no error at the requested 
            // page in the log (especially if it is not the first page).
            // However, if there are error in the log

            if (pageIndex > 0 && totalCount > 0)
            {

            
            //#line default
            //#line hidden
WriteLiteral("                <p>");


            
            //#line 209 "..\..\ErrorLogPage.cshtml"
              Write(LinkHere(basePageName, HtmlLinkType.Start, "Go to first page", 0, pageSize));

            
            //#line default
            //#line hidden
WriteLiteral(".</p>\r\n");


            
            //#line 210 "..\..\ErrorLogPage.cshtml"
            }
            else
            {

            
            //#line default
            //#line hidden
WriteLiteral("                <p>No errors found.</p>\r\n");


            
            //#line 214 "..\..\ErrorLogPage.cshtml"
            }
        }

            
            //#line default
            //#line hidden
WriteLiteral("\r\n");


WriteLiteral("\r\n");


        }
    }
}
#pragma warning restore 1591
