using LibDeltaSystem;
using LibDeltaSystem.Db.ArkEntries;
using LibDeltaSystem.Entities.ArkEntries.Dinosaur;
using LibDeltaSystem.WebFramework;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWebMap.PrimalPackageManager.Services
{
    /// <summary>
    /// Handles all of the package stuff up until it's actually written out. That's handled by the parent class
    /// </summary>
    public abstract class BasePackageService : DeltaWebService
    {
        public BasePackageService(DeltaConnection conn, HttpContext e) : base(conn, e)
        {
        }

        public abstract Task WritePackageResponse(object[] data, Type packageType);

        public string packageName;

        public override async Task<bool> OnPreRequest()
        {
            return true;
        }

        public override async Task OnRequest()
        {
            //Get URL params
            int epoch = GetIntFromQuery("last_epoch", 0);
            int offset = GetIntFromQuery("skip", 0);
            int limit = GetIntFromQuery("limit", int.MaxValue);

            //Get the package
            var package = await conn.GetPrimalPackageByNameAsync(packageName);
            if(package == null)
            {
                await WriteString("Package Not Found", "text/plain", 400);
                return;
            }

            //Calculate epoch
            long minTime = 0;

            //Get the package type and get it's data
            object[] content = await package.GetContentAsync(conn, epoch, offset, limit);

            //Render
            await WritePackageResponse(content, package.GetPackageContentType());
        }

        public override async Task<bool> SetArgs(Dictionary<string, string> args)
        {
            packageName = args["PACKAGE_NAME"];
            return true;
        }
    }
}
