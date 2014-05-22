using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Personal.JsonAccess
{
    public class JsonRequest
    {
        // Data you'll be sending
        private string postData;
        // Data you'll be receiving
        private string responseTxt;
        // Event handled for handling end of response
        public event EventHandler Completed;
        // Public property to retrieve response
        public string ResponseTxt
        {
            get { return responseTxt; }
        }


        public void beginRequest(string postParameters,string Url)
        {
            // Assemble postData string
            postData = postParameters;
            // Set destination url
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Url, UriKind.Absolute));
            // Set request method, using POST of course
            request.Method = "POST";
            // This is important
            request.ContentType = "application/x-www-form-urlencoded";
            // Start request
            request.BeginGetRequestStream(new AsyncCallback(RequestReady), request);
        }


        void RequestReady(IAsyncResult asyncResult)
        {

            HttpWebRequest request = asyncResult.AsyncState as HttpWebRequest;
            // Retrieve created stream
            Stream stream = request.EndGetRequestStream(asyncResult);
            // Avoid 
            Deployment.Current.Dispatcher.BeginInvoke(delegate()
            {
                StreamWriter writer = new StreamWriter(stream);
                // Write data to the stream
                writer.Write(postData);
                writer.Flush();
                writer.Close();
                // Expect response
                request.BeginGetResponse(new AsyncCallback(ResponseReady), request);
            });

        }

        void ResponseReady(IAsyncResult asyncResult)
        {
            HttpWebRequest request = asyncResult.AsyncState as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asyncResult);
            // Avoid trashing the procedure

            Deployment.Current.Dispatcher.BeginInvoke(delegate()
            {// Retrieve response stream
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                // Read response
                responseTxt = reader.ReadToEnd();
                // Dispatch event
                Completed(this, new EventArgs());
            });
        }

    }
}
