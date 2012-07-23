using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoveKaoExam.Models
{
    public class 爱考网服务器连接
    {
        public 爱考网服务器连接() {
            连接是否成功 = true;
        }

        public 爱考网服务器连接(string s连接失败消息)
        {
            连接是否成功 = false;
            连接失败消息 = s连接失败消息;
        }

        public 爱考网服务器连接(Exception ex)
        {
            连接是否成功 = false;
            连接失败消息 = ex.Message;
        }

        public bool 连接是否成功 { get; set; }

        private string _连接失败消息;
        public string 连接失败消息 {
            get { return _连接失败消息; }
            set { _连接失败消息 = value; }
        }
    }
}