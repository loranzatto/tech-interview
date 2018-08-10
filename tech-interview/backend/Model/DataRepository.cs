using Newtonsoft.Json;
using System.Configuration;
using System.IO;

namespace tech_interview.backend.Model
{
    public class DataRepository : IDataRepository
    {
        /// <summary>
        /// Method to get data from the JSON File within a Model tier avoiding communication with the Controller tier
        /// </summary>
        /// <param name="level">Level data will be get (1,2 or 3)</param>
        /// <returns>Object with whole instances needed to implement tech interview project</returns>
        public Root getDataFromLevels(string level)
        {
            string path = ConfigurationManager.AppSettings["level" + level + "ReadData"];

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            
            return JsonConvert.DeserializeObject<Root>(File.ReadAllText(path), settings);
        }  
    }
}
