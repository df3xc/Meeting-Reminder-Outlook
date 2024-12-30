

// 23.08.2020 Carsten Lueck
// Read WaterLevel / Pegel des Wassers


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace WebTest
{

    //    {
    //  "timestamp": "2020-08-23T03:00:00+02:00",
    //  "value": 218.0,
    //  "trend": 0,
    //  "stateMnwMhw": "normal",
    //  "stateNswHsw": "normal"
    //}

    [Serializable()]
    public class measurement
    {
        public string timestamp { get; set; }
        public float value { get; set; }
        public float trend { get; set; }
        public string stateMnwMhw { get; set; }
        public string stateNswHsw { get; set; }
    }

    class neckar
    {
        public string url_base = "https://www.pegelonline.wsv.de/webservices/rest-api/v2/";

        public float getWaterLevel(string station)
        {
            string rc = "";
            measurement m = new measurement();

            try
            {
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(url_base + "stations/" + station + "/W/currentmeasurement.json");
                WebReq.Method = "GET";
                WebReq.Credentials = CredentialCache.DefaultCredentials;
                //WebReq.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                //Let's show some information about the response
                if (WebResp.StatusCode.ToString() != "OK")
                {
                    Exception ex = new Exception("Access failed ");
                    throw (ex);
                }

                //Now, we read the response
                Stream Answer = WebResp.GetResponseStream();
                StreamReader _Answer = new StreamReader(Answer);
                string response = _Answer.ReadToEnd();

                m = Newtonsoft.Json.JsonConvert.DeserializeObject<measurement>(response);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            return (m.value);
        }


    }
}
