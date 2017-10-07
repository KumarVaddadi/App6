using System.Collections.Generic;
using System.Json;
using System.Threading.Tasks;

namespace App5
{
    class DataClient
    {
        WebAPIClient webClient;
        //JsonValue jsonDoc;
        public async Task<List<string>> GetFleetData(string tag)
        {
            string apiURl = "https://fm.bt.ab2ls.ch/Portal.Web/api/Fleets/getFleetData?fleetName=AC3K1_10";


            webClient = new WebAPIClient();
            var lstNames = await webClient.AuthenticateAsync(apiURl, tag);
            return lstNames;
        }


    }


}