using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using ProjecWitch.Model;
using ProjecWitch.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjecWitch.Pages
{
    public class IndexBase : ComponentBase
    {
        public string Messege { get; set; }
        List<ProphecyModel> prophecyModels = new List<ProphecyModel>();
        public bool IsMessegeHidden { get; set; }
        public bool IsButtonHidden { get; set; }
        public string rarityCss;

        protected override async Task OnInitializedAsync()
        {
            ReadDataFromJson();
            IsMessegeHidden = true;
            IsButtonHidden = false;
            Messege = "";
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

        public void TellProphecy()
        {
            var rarity = ChoseRarity();
            var prophecies = prophecyModels.Where(p => p.Probability == rarity);
            Random random = new Random();
            var value = random.Next(prophecies.Count());
            Messege = prophecies.ToList()[value].Message;
            IsMessegeHidden = false;
            IsButtonHidden = true;
            rarityCss = prophecies.ToList()[value].Probability.ToString().ToLower();
            StateHasChanged();
        }

        //Common 50/100 Uncomon 25/100 rare 15/100 , epic 9/100 legendary 1/100 
        private Rarity ChoseRarity()
        {
            Random random = new Random();
            var value = random.Next(0, 99);
            if(value <50)
            {
                return Rarity.Common;
            }
            if(value < 75)
            {
                return Rarity.Uncommon;
            }
            if(value < 90)
            {
                return Rarity.Rare;
            }
            if(value < 99)
            {
                return Rarity.Epic;
            }

            return Rarity.Legendary;
        }
    }
}
