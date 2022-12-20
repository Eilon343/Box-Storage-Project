using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxModel
{
    public class Configuration
    {
        public ConfigData Data { get; set; }
        public Configuration()
        {
            var currentDir = Environment.CurrentDirectory;
            var FileName = "Config.json";
            var configpath = Path.Combine(currentDir, FileName);
            var raw = File.ReadAllText(@".\Config.json");
            Data = JsonConvert.DeserializeObject<ConfigData>(raw);
        }
    }
    public class ConfigData
    {
        public int MaxBoxes { get; set; }
        public double MaxWidthToSearch { get; set; }
        public double MaxHeightToSearch { get; set; }
    }
}
