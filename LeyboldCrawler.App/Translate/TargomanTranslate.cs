using System;
using System.Collections.Generic;
using System.Text;
using LeyboldCrawler.Model.Targoman;
using LeyboldCrawler.Model.Translate;
using RestSharp;
using Newtonsoft.Json;
using System.Linq;

namespace LeyboldCrawler.App.Translate
{
    public class TargomanTranslate : ITranslate
    {
        public ResultTranslate Translate(InputTranslate input)
        {
            Targoman targoman = new Targoman()
            {
                jsonrpc = "2.0",
                method = "Targoman::translate",
                id = 1,
                @params = new object[] { "sSTargomanWUI", input.Input, "en2fa", "127.0.0.10", "NMT", true, true, true, "Tk1UO2VuMmZhOzUuNS41LjU7Mzs3NjRlZmE4ODNkZGExZTExZGI0NzY3MWM0YTNiYmQ5ZTsx" }
            };
            string param = JsonConvert.SerializeObject(targoman);

            var client = new RestClient("https://targoman.ir/API/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("content-length", param.Length.ToString());
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("Host", "targoman.ir");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 Safari/537.36");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", param, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var res = JsonConvert.DeserializeObject<TargomanResponse>(response.Content);
            var res1 = JsonConvert.DeserializeObject<dynamic>(response.Content);

            List<string> txt = new List<string>();
            foreach (var item in res.result.tr.@base)
            {
                txt.Add(item[1]);
            }

            string lll = string.Join("</p><p>", txt);

            return new ResultTranslate { Result = lll };
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TargomanTranslate()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
