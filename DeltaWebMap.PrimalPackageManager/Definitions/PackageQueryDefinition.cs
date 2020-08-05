using DeltaWebMap.PrimalPackageManager.Services;
using LibDeltaSystem;
using LibDeltaSystem.WebFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeltaWebMap.PrimalPackageManager.Definitions
{
    public class PackageQueryDefinition : DeltaWebServiceDefinition
    {
        public override string GetTemplateUrl()
        {
            return "/query";
        }

        public override DeltaWebService OpenRequest(DeltaConnection conn, HttpContext e)
        {
            return new PackageQueryService(conn, e);
        }
    }
}
