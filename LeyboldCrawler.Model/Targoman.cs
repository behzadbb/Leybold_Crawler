using System;
using System.Collections.Generic;
using System.Text;

namespace LeyboldCrawler.Model.Targoman
{
    public class Targoman
    {
        public string jsonrpc { get; set; }
        public string method { get; set; }
        public int id { get; set; }
        public dynamic @params { get; set; }
    }

    public class Tr
    {
        public List<List<string>> @base { get; set; }
        public List<List<List<object>>> phrases { get; set; }
        public List<List<List<object>>> alignments { get; set; }
    }

    public class Result
    {
        public string tuid { get; set; }
        public double TrTime { get; set; }
        public string serverID { get; set; }
        public string @class { get; set; }
        public string errno { get; set; }
        public Tr tr { get; set; }
        public double rpcTime { get; set; }
    }

    public class TargomanResponse
    {
        public string jsonrpc { get; set; }
        public int id { get; set; }
        public Result result { get; set; }
    }
}
