﻿using LibDeltaSystem;
using LibDeltaSystem.Tools;
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
    public class PackageDWFService : BasePackageService
    {
        public PackageDWFService(DeltaConnection conn, HttpContext e) : base(conn, e)
        {
            epoch = TimeTool.GetStandardEpochFromTicks(DateTime.UtcNow.Ticks);
        }

        int epoch;

        public override async Task WritePackageResponse(object[] data, Type dataType)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                //Encode
                DeltaWebFormatEncoder encoder = new DeltaWebFormatEncoder(ms, dataType);
                encoder.Encode(data, new Dictionary<byte, byte[]>()
                {
                    {0, BitConverter.GetBytes(epoch) }
                });
                ms.Position = 0;
                e.Response.ContentType = "application/octet-stream";
                await ms.CopyToAsync(e.Response.Body);
            }
        }
    }
}
