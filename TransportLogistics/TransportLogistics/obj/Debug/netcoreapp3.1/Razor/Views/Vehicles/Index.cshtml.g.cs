#pragma checksum "D:\Internship\TransportLogistics\TransportLogistics\TransportLogistics\Views\Vehicles\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2942b3375bba6c978c667bd6dc2a141450dd3e4d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Vehicles_Index), @"mvc.1.0.view", @"/Views/Vehicles/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Internship\TransportLogistics\TransportLogistics\TransportLogistics\Views\_ViewImports.cshtml"
using TransportLogistics;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Internship\TransportLogistics\TransportLogistics\TransportLogistics\Views\_ViewImports.cshtml"
using TransportLogistics.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2942b3375bba6c978c667bd6dc2a141450dd3e4d", @"/Views/Vehicles/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99b2c2636727c6e6efa2d6de4cd1330bd3c73003", @"/Views/_ViewImports.cshtml")]
    public class Views_Vehicles_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 3 "D:\Internship\TransportLogistics\TransportLogistics\TransportLogistics\Views\Vehicles\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<link href=""//cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css"" rel=""stylesheet"" />
<script src=""//cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js""></script>
<div class=""d-flex flex-column flex-md-row"">
    <div class=""d-flex flex-column flex-grow-1"">

    </div>
    <div class=""d-flex flex-column "">
        <div class=""input-group mb-3"">
            <div class=""input-group-prepend"">
                <button class=""btn-car"">
                    <i class=""fa fa-car""></i>Add Vehicle
                </button>

            </div>
        </div>
    </div>
</div>
<table id=""Vehicles"">
    <thead>
        <tr>
            <th>Vehicle Name</th>
            <th>Vehicle Type</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Ford Tourneo Courier</td>
            <td>Small Transport</td>
        </tr>
        <tr>
            <td>VW Transporter</td>
            <td>Medium Transport</td>
        </tr>
    </tbody>
</table>


");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    <script>\r\n        $(document).ready(function () {\r\n            $(\'#Vehicles\').DataTable();\r\n        });\r\n    </script>\r\n\r\n\r\n");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
