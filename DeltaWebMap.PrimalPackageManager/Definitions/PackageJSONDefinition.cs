using DeltaWebMap.PrimalPackageManager.Services;
using LibDeltaSystem;
using LibDeltaSystem.WebFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeltaWebMap.PrimalPackageManager.Definitions
{
    public class PackageJSONDefinition : DeltaWebServiceDefinition
    {
        public override string GetTemplateUrl()
        {
            return "/packages/JSON/{PACKAGE_NAME}";
        }

        public override DeltaWebService OpenRequest(DeltaConnection conn, HttpContext e)
        {
            return new PackageJSONService(conn, e);
        }
    }
}
