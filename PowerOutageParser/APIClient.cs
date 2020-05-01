using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PowerOutageParser
{
    public class APIClient
    {
        public static string GetOblenergoPage(int placeId, string date, PowerOutageEnum type)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://www.oblenergo.kharkov.ua/"+(type == PowerOutageEnum.Emergency ? "tabledisconnect" : "tabledisconnectplan"));
            var postData = $"start[date]={date}";
            postData += $"&operator={placeId}";
            postData += @"&end[date]=";
            postData +=  "&" + (type == PowerOutageEnum.Emergency ? "form-1KCiA9-OxYf0yDQsfbvOC98P-7InQjfBoKTVVVF2Jcg" : "form-3mEyHcEDxxdCtNNJR9_bxn85oKToemxRZ4l151Y-DtI");
            postData +=  "&" + (type == PowerOutageEnum.Emergency ? "form_id=tabledisconnect_page_form" : "tabledisconnectplan-page-form");
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
    }
}
