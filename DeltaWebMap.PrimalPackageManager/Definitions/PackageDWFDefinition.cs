using DeltaWebMap.PrimalPackageManager.Services;
using LibDeltaSystem;
using LibDeltaSystem.WebFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeltaWebMap.PrimalPackageManager.Definitions
{
    public class PackageDWFDefinition : DeltaWebServiceDefinition
    {
        public override string GetTemplateUrl()
        {
            return "/packages/DWF/{PACKAGE_NAME}";
        }

        public override DeltaWebService OpenRequest(DeltaConnection conn, HttpContext e)
        {
            return new PackageDWFService(conn, e);
        }
    }
}
