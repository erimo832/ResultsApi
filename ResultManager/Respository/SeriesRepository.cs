using Newtonsoft.Json;
using ResultManager.Model;
using ResultManager.Model.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace ResultManager.Respository
{
    public class SeriesRepository : ISeriesRepository
    {
        private ISeriesConfiguration Config { get;}

        public SeriesRepository(ISeriesConfiguration cfg)
        {
            Config = cfg;
        }

        public IList<SerieInfo> GetSerieInfos()
        {
            var result = new List<SerieInfo>();
            var directories = new DirectoryInfo(Config.SeriesPath);

            foreach (var item in directories.GetDirectories())
            {
                result.Add(GetSerieInfo(item.Name));
            }

            return result;
        }

        public SerieInfo GetSerieInfo(string name)
        {
            if (Directory.Exists(Config.SeriesPath + @"\" + name))
            {
                return new SerieInfo
                { 
                    Name = name,
                    SeriePath = Config.SeriesPath + @"\" + name
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
