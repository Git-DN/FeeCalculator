using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace FeeCalculator.Services.Config
{
    // Singleton implementation borrowed from:  https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Design-Patterns-Singleton

    public sealed class FeeCalculatorConfig
    {
        private FeeCalculatorConfig() { }

        private static volatile FeeCalculatorConfig _instance;

        private static readonly object _syncLock = new object();

        public static FeeCalculatorConfig Instance
        {
            get
            {
                if (_instance != null) return _instance;

                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .Build();

                        _instance = new FeeCalculatorConfig();

                        configuration.GetSection("FeeCalculator").Bind(_instance);
                    }
                }

                return _instance;
            }
        }

        public string FilePath { get; set; }

        public Dictionary<string, RuleConfig> Rules { get; set; }
    }
}