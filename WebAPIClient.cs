using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.Json;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace App5
{
    class WebAPIClient
    {
        public async System.Threading.Tasks.Task<List<string>> AuthenticateAsync(string url, string tag)
        {

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/xml";
            request.Method = "GET";

            /*
             <CarManageDto>
                <ConsistName>187001</ConsistName>
                <Id>531CBDC5-FF42-4B7A-8FD2-A21D13060D74</Id>
                <Name>AC3K1_10_187001_1</Name>
                <Position>1</Position>
                <Type>Type0</Type>
             </CarManageDto>
             */

            List<string> carNames = new List<string>();
            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var responseData = streamReader.ReadToEnd();
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(responseData);
                    string strRegex = @"<My_RootNode(?<xmlns>\s+xmlns([\s]|[^>])*)>";
                    var myMatch = new Regex(strRegex, RegexOptions.None).Match(xmlDoc.InnerXml);
                    if (myMatch.Success)
                    {
                        var grp = myMatch.Groups["xmlns"];
                        if (grp.Success)
                        {
                            xmlDoc.InnerXml = xmlDoc.InnerXml.Replace(grp.Value, "");
                        }
                    }

                    //XmlNode carNode = xmlDoc.GetElementsByTagName("Cars")[0];
                    XmlNode carNode = xmlDoc.GetElementsByTagName("Consists")[0];
                    foreach (XmlNode node in carNode)
                    {
                        foreach (XmlNode n in node)
                        {
                            if (n.Name == tag)
                            {
                                //carNames.Add(tag + ": " + n.InnerText + " ");
                                carNames.Add(n.InnerText );
                            }
                        }
                       
                    }
                }
                // Get a stream representation of the HTTP web response:
                return carNames;
            }
        }
    }
    class DictionaryConverter : JsonConverter
    {
        public DictionaryConverter()
        {

        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Contract);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var contract = value as Contract;
            var json = JsonConvert.SerializeObject(value);
            var dictArray = String.Join(",", contract.UnknownValues.Select(pair => "\"" + pair.Key + "\":\"" + pair.Value + "\""));

            json = json.Substring(0, json.Length - 1) + "," + dictArray + "}";
            writer.WriteRaw(json);
        }
    }
    class Contract
    {
        public Contract()
        {
            UnknownValues = new Dictionary<string, string>();
        }

        public int id { get; set; }
        public string name { get; set; }

        [JsonIgnore]
        public Dictionary<string, string> UnknownValues { get; set; }
    }
    public class CarManageDto
    {
        public int ConsistName { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string Type { get; set; }


    }
}