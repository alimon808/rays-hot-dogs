using RaysHotDogs.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace RaysHotDogs.Core
{
    public class HotDogRepository
    {
        private static List<HotDogGroup> hotDogGroups = new List<HotDogGroup>();
        string url = "http://gillcleerenpluralsight.blob.core.windows.net/files/hotdogs.json";
        private async Task LoadDataAsync(string uri)
        {
            if (hotDogGroups != null)
            {
                string responseJsonString = null;
                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        Task<HttpResponseMessage> getResponse = httpClient.GetAsync(uri);
                        HttpResponseMessage response = await getResponse;
                        responseJsonString = await response.Content.ReadAsStringAsync();
                        hotDogGroups = JsonConvert.DeserializeObject<List<HotDogGroup>>(responseJsonString);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
        public HotDogRepository()
        {
            Task.Run(() => this.LoadDataAsync(url)).Wait();
        }

        public List<HotDog> GetAllHotDogs()
        {
            IEnumerable<HotDog> hotDogs =
                from hotDogGroup in hotDogGroups
                from hotDog in hotDogGroup.HotDogs

                select hotDog;
            return hotDogs.ToList<HotDog>();
        }

        public List<HotDogGroup> GetGroupedHotDogs()
        {
            return hotDogGroups;
        }

        public List<HotDog> GetHotDogsForGroup(int hotDogGroupId)
        {
            var group = hotDogGroups.Where(h => h.HotDogGroupId == hotDogGroupId).FirstOrDefault();

            if (group != null)
            {
                return group.HotDogs;
            }
            return null;
        }

        public List<HotDog> GetFavoriteHotDogs()
        {
            IEnumerable<HotDog> hotDogs =
                from hotDogGroup in hotDogGroups
                from hotDog in hotDogGroup.HotDogs
                where hotDog.IsFavorite
                select hotDog;

            return hotDogs.ToList<HotDog>();
        }

        public HotDog GetHotDogById(int hotDogId)
        {
            IEnumerable<HotDog> hotDogs =
                from hotDogGroup in hotDogGroups
                from hotDog in hotDogGroup.HotDogs
                where hotDog.HotDogId == hotDogId
                select hotDog;

            return hotDogs.FirstOrDefault();
        }
    }
}