﻿using Microsoft.Extensions.Configuration;
using System.IO;

namespace TestUtilities
{
    public static class ConfigurationProvider
    {
        public static IConfigurationRoot GetApplicationConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(@"../../../../../src/CQRSExample.API/"))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }
    }
}
