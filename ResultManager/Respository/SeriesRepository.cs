using Newtonsoft.Json;
using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ResultManager.Respository
{
    public class SeriesRepository : ISeriesRepository
    {
        private string SeriesRootPath => @"M:\www\discgolf\series";

        public IList<SerieInfo> GetSerieInfos()
        {
            var result = new List<SerieInfo>();
            var directories = new DirectoryInfo(SeriesRootPath);

            foreach (var item in directories.GetDirectories())
            {
                result.Add(GetSerieInfo(item.Name));
            }

            return result;
        }

        public SerieInfo GetSerieInfo(string name)
        {
            if (Directory.Exists(SeriesRootPath + @"\" + name))
            {
                return new SerieInfo
                { 
                    Name = name,
                    SeriePath = SeriesRootPath + @"\" + name
                };
            }

            throw new Exception($"Serie with name {name} not found");
        }

        public SeriesSetting GetSettings(SerieInfo serieInfo)
        {
            var file = File.ReadAllText(serieInfo.SeriePath + @"\settings.json");

            return JsonConvert.DeserializeObject<SeriesSetting>(file);
        }
    }
}
