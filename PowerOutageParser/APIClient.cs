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
        public static string GetOblenergoPage(int placeId, string date)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://www.oblenergo.kharkov.ua/tabledisconnect/");
            var postData = $"start[date]={date}";
            postData += $"&operator={placeId}";
            postData += @"&end[date]=";
            postData += "&form-1KCiA9-OxYf0yDQsfbvOC98P-7InQjfBoKTVVVF2Jcg";
            postData += "&form_id=tabledisconnect_page_form";
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
