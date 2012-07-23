using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace LoveKaoExam.Data
{
    public class 取题条件
    {
        #region 变量

        private string webSiteTagList;
        private string collectionTagList;
        private bool isOldExam;
        private bool isDoneWrong;
        private bool isNotDone;
        private Guid memberId;
        private bool isMyUpload;
        private string problemContent;

        #endregion


        #region 属性
        /// <summary>
        /// 是否是自己出的，true是，false不是
        /// </summary>
        public bool 是否是自己出的
        {
            get { return isMyUpload; }
            set { isMyUpload = value; }
        }


        /// <summary>
        /// 会员id，必须赋值
        /// </summary>
        public Guid 会员ID
        {
            get { return memberId; }
            set { memberId = value; }
        }



        /// <summary>
        /// 是否是做错的，true是，false不是
        /// </summary>
        public bool 是否是做错的
        {
            get { return isDoneWrong; }
            set { isDoneWrong = value; }
        }


        /// <summary>
        /// 是否是没做过的，true是，false不是
        /// </summary>
        public bool 是否是没做过的
        {
            get { return isNotDone; }
            set { isNotDone = value; }
        }

     

        /// <summary>
        /// 网站分类名称集合字符串,以逗号隔开
        /// </summary>
        public string 网站分类集合
        {
            get { return webSiteTagList; }
            set { webSiteTagList = value; }
        }


       

        /// <summary>
        /// 题干中包含此关键字
        /// </summary>
        public string 题干关键字
        {
            get { return problemContent; }
            set { problemContent = value; }
        }


        public string 试卷名称
        {
            get;
            set;
        }


        public short 考试时长
        {
            get;
            set;
        }


        public int 及格条件
        {
            get;
            set;
        }


      


        public List<取题题型及数量> 取题题型及数量集合
        {
            get;
            set;
        }


        #endregion


        #region 方法

        public string 转化成Json字符串()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }

        public static 取题条件 把Json转化成试题外部信息(string 试题Json字符串)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            取题条件 condition = jss.Deserialize<取题条件>(试题Json字符串);
            return condition;
        }

        #endregion
    }


    public class 取题题型及数量
    {
        public byte 题型Enum
        {
            get;
            set;
        }

        public int 数量
        {
            get;
            set;
        }

        public string 题型名称
        {
            get;
            set;
        }

        public decimal 每小题分值
        {
            get;
            set;
        }
    }
}
