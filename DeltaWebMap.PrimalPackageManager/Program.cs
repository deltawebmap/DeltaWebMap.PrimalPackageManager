using DeltaWebMap.PrimalPackageManager.Definitions;
using LibDeltaSystem;
using LibDeltaSystem.WebFramework;
using System;

namespace DeltaWebMap.PrimalPackageManager
{
    class Program
    {
        public static DeltaConnection conn;
        
        static void Main(string[] args)
        {
            //Connect to database
            conn = DeltaConnection.InitDeltaManagedApp(args, 0, 1, new PrimalPackageManagerNetwork());

            //Start server
            DeltaWebServer server = new DeltaWebServer(conn, conn.GetUserPort(0));
            server.AddService(new PackageQueryDefinition());
            server.AddService(new PackageDWFDefinition());
            server.AddService(new PackageJSONDefinition());
            server.RunAsync().GetAwaiter().GetResult();
        }
    }
}
