using DeltaWebMap.PrimalPackageManager.Definitions;
using LibDeltaSystem;
using LibDeltaSystem.WebFramework;
using System;

namespace DeltaWebMap.PrimalPackageManager
{
    class Program
    {
        public static DeltaConnection conn;

        public const byte APP_VERSION_MAJOR = 0;
        public const byte APP_VERSION_MINOR = 2;
        
        static void Main(string[] args)
        {
            //Connect to database
            conn = DeltaConnection.InitDeltaManagedApp(args, APP_VERSION_MAJOR, APP_VERSION_MINOR, new PrimalPackageManagerNetwork());

            //Start server
            DeltaWebServer server = new DeltaWebServer(conn, conn.GetUserPort(0));
            server.AddService(new PackageQueryDefinition());
            server.AddService(new PackageDWFDefinition());
            server.AddService(new PackageJSONDefinition());
            server.RunAsync().GetAwaiter().GetResult();
        }
    }
}
