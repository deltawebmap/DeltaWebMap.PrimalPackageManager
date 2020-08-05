using LibDeltaSystem;
using LibDeltaSystem.Tools.DeltaWebFormat;
using LibDeltaSystem.WebFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWebMap.PrimalPackageManager.Services
{
    public class PackageJSONService : BasePackageService
    {
        public PackageJSONService(DeltaConnection conn, HttpContext e) : base(conn, e)
        {
        }

        public override async Task WritePackageResponse(object[] data, Type dataType)
        {
            await WriteJSON(new RenderedResponseData
            {
                content = data
            });
        }

        class RenderedResponseData
        {
            public object[] content;
        }
    }
}
