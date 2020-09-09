using LibDeltaSystem;
using LibDeltaSystem.Db.ArkEntries;
using LibDeltaSystem.Entities.ArkEntries.Dinosaur;
using LibDeltaSystem.Tools;
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
    public class PackageQueryService : DeltaWebService
    {
        public PackageQueryService(DeltaConnection conn, HttpContext e) : base(conn, e)
        {
        }

        public override async Task<bool> OnPreRequest()
        {
            return true;
        }

        public override async Task<bool> SetArgs(Dictionary<string, string> args)
        {
            return true;
        }

        public override async Task OnRequest()
        {
            //Decode
            var request = await ReadPOSTContentChecked<RequestData>();
            if(request == null)
            {
                return;
            }

            //Calculate the current epoch
            int currentEpoch = TimeTool.GetStandardEpochFromTicks(DateTime.UtcNow.Ticks);

            //Create response
            var response = new ResponseData
            {
                packages = new List<ResponseData_Package>(),
                current_epoch = currentEpoch
            };

            //Search for requested packages
            foreach(var p in request.requested_packages)
            {
                //Parse
                if(!long.TryParse(p.mod_id, out long modId))
                {
                    await WriteString("Invalid mod ID '" + p.mod_id + "'.", "text/plain", 400);
                    return;
                }
                
                //Search
                var r = await conn.GetPrimalPackageByModAsync(modId, p.package_type);
                if (r == null)
                    return;

                //Write response
                response.packages.Add(new ResponseData_Package
                {
                    last_updated = TimeTool.GetStandardEpochFromTicks(r.last_updated),
                    mod_id = r.mod_id.ToString(),
                    package_name = r.name,
                    package_type = r.package_type,
                    entity_count = await r.CountItemsAsync(conn, p.last_epoch),
                    display_name = r.display_name,
                    package_url_dwf = conn.config.hosts.packages + "/packages/DWF/" + r.name,
                    package_url_json = conn.config.hosts.packages + "/packages/JSON/" + r.name
                });
            }

            //Write
            await WriteJSON(response);
        }       

        /* Request data */

        class RequestData
        {
            public RequestData_Mod[] requested_packages;
        }

        class RequestData_Mod
        {
            public string mod_id;
            public string package_type;
            public int? last_epoch; //Only used to get the counts
        }

        /* Response data */

        class ResponseData
        {
            public List<ResponseData_Package> packages;
            public int current_epoch;
        }

        class ResponseData_Package
        {
            public string package_name;
            public string package_type;
            public string mod_id;
            public int last_updated;
            public string package_url_dwf;
            public string package_url_json;
            public long entity_count;
            public string display_name;
        }
    }
}
