using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveKaoExam.Data
{
    public class 分类相关联分类
    {
        #region 属性

        public List<string> 是类别的父分类集合
        {
            get;
            set;
        }

        public List<string> 子分类集合
        {
            get;
            set;
        }


        /// <summary>
        /// 不包含父分类的同义词
        /// </summary>
        public int 是类别的父分类数量
        {
            get;
            set;
        }


        public string 分类名
        {
            get;
            set;
        }

        #endregion
    }
}
