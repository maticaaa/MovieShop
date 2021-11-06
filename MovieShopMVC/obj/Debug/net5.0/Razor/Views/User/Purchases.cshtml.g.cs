#pragma checksum "C:\Users\Matica\source\repos\MovieShop\MovieShopMVC\Views\User\Purchases.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "00ae38e74a0e44c4b2427ef4956a82a56d65a4db"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Purchases), @"mvc.1.0.view", @"/Views/User/Purchases.cshtml")]
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
#line 1 "C:\Users\Matica\source\repos\MovieShop\MovieShopMVC\Views\_ViewImports.cshtml"
using MovieShopMVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Matica\source\repos\MovieShop\MovieShopMVC\Views\_ViewImports.cshtml"
using MovieShopMVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"00ae38e74a0e44c4b2427ef4956a82a56d65a4db", @"/Views/User/Purchases.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b6112aabca9b932007558671ce74e32c023ef6c3", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Purchases : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<ApplicationCore.Models.PurchaseDetailsResponseModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Matica\source\repos\MovieShop\MovieShopMVC\Views\User\Purchases.cshtml"
  
    ViewData["Title"] = "Purchases";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"rounded\">\r\n    <div class=\"row\">\r\n");
#nullable restore
#line 9 "C:\Users\Matica\source\repos\MovieShop\MovieShopMVC\Views\User\Purchases.cshtml"
         foreach (var purchaseDetail in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"col-xl-2 col-sm-4 col-lg-3 col-6\">\r\n");
            WriteLiteral("\r\n            ");
#nullable restore
#line 14 "C:\Users\Matica\source\repos\MovieShop\MovieShopMVC\Views\User\Purchases.cshtml"
       Write(await Html.PartialAsync("_MovieCard", 
                new ApplicationCore.Models.MovieCardResponseModel { 
                    Id = purchaseDetail.MovieId,
                    Title = purchaseDetail.Title,
                    PosterUrl = purchaseDetail.PosterUrl
                }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"card-body\">\r\n                \r\n                    ");
#nullable restore
#line 22 "C:\Users\Matica\source\repos\MovieShop\MovieShopMVC\Views\User\Purchases.cshtml"
               Write(await Html.PartialAsync("_PurchaseDetails", purchaseDetail));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                \r\n            </div>\r\n            \r\n        </div>\r\n");
#nullable restore
#line 27 "C:\Users\Matica\source\repos\MovieShop\MovieShopMVC\Views\User\Purchases.cshtml"

        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<ApplicationCore.Models.PurchaseDetailsResponseModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591