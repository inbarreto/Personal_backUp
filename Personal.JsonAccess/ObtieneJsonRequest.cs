using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Personal.JsonAccess
{
   public  class ObtieneJsonRequest
    {
        public void jsonDatos(string uri)
        {


            System.Uri myUri = new System.Uri(uri);
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(myUri);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            myRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback),myRequest);

        }
        private void GetRequestStreamCallback(IAsyncResult callbackResult)
        {
            HttpWebRequest myRequest = (HttpWebRequest)callbackResult.AsyncState;
            // End the stream request operation
            Stream postStream = myRequest.EndGetRequestStream(callbackResult);
            string postData=string.Empty;
            
            if (PhoneApplicationService.Current.State.ContainsKey("postData"))
                postData = PhoneApplicationService.Current.State["postData"].ToString() ;
            

            // Create the post data
            // = "{\"device\":\"windows_8\",\"name\":\"\"}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Add the post data to the web request
            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Close();

            // Start the web request
            
             myRequest.BeginGetResponse(new AsyncCallback(GetResponsetStreamCallback), myRequest);

        }

        private void GetResponsetStreamCallback(IAsyncResult callbackResult)
        {
            HttpWebRequest request = (HttpWebRequest)callbackResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(callbackResult);
            string result = string.Empty;
            using (StreamReader httpWebStreamReader = new StreamReader(response.GetResponseStream()))
            {

                string jsonString = httpWebStreamReader.ReadToEnd();
                if (PhoneApplicationService.Current.State.ContainsKey("jsonResult"))
                    PhoneApplicationService.Current.State["jsonResult"] = jsonString;
                else
                    PhoneApplicationService.Current.State.Add("jsonResult", jsonString);                         
            }            
        }

    }
}
