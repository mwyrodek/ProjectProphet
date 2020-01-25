using Newtonsoft.Json;
using ProjecWitch.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjecWitch.Services
{
    public class ProphecyService : IProphecyService
    {
        List<ProphecyModel> prophecyModels = new List<ProphecyModel>();
        public ProphecyService()
        {
            ReadDataFromJson();
        }

        private void ReadDataFromJson()
        {
            using (StreamReader r = new StreamReader("data\\dataImput.json"))
            {
                string json = r.ReadToEnd();
                var array = JsonConvert.DeserializeObject<ProphecyModel[]>(json);
                prophecyModels = array.ToList();
            }
        }

        public  Task<string> GetProphecy()
        {
            return new Task<string>(() => { return prophecyModels.First().Message; });
        }
    }
}
