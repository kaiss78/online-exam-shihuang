using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveKaoExam.Data
{
    public class 考生范围
    {
        #region 属性

        public Guid ID
        {
            get;
            set;
        }


        /// <summary>
        /// 考试设置ID或试卷内容ID
        /// </summary>
        public Guid 相关ID
        {
            get;
            set;
        }


        public Guid 考生ID
        {
            get;
            set;
        }


        public static IQueryable<考生范围> 考生范围查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return db.考生范围表.Select(a => new 考生范围
                    {
                        ID = a.ID,
                        考生ID = a.考生ID,
                        相关ID = a.相关ID
                    });
            }
        }


        public static IQueryable<考生范围> 考生范围联合查询
        {
            get
            {
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                return from examScope in db.考生范围表
                       join user in db.用户表
                       on examScope.考生ID equals user.ID
                       select new 考生范围
                       {
                           ID = examScope.ID,
                           考生ID = examScope.考生ID,
                           相关ID = examScope.相关ID,
                           考生表 = user
                       };
            }
        }


        
        public 用户表 考生表
        {
            get;
            set;
        }

        #endregion



        #region 方法

        

        public static void 更新考生范围(Guid 相关ID, List<Guid> 考生ID集合, LoveKaoExamEntities db)
        {
            List<考生范围表> listOldArea = db.考生范围表.Where(a=>a.相关ID==相关ID).ToList();
            List<Guid> listOldAreaId = listOldArea.Select(a => a.考生ID).ToList();
            //没变，则不作任何操作，若有变，则先删除原来的，再插入新的
            List<Guid> listNewId = listOldAreaId.Except(考生ID集合).ToList();
            if (listOldAreaId.Count != 考生ID集合.Count || listOldAreaId.Count == 考生ID集合.Count && listNewId.Count != 0)
            {
                foreach (var oldArea in listOldArea)
                {
                    db.考生范围表.DeleteObject(oldArea);
                }
                List<考生范围表> listNewArea = new List<考生范围表>();
                foreach (Guid examineeId in 考生ID集合)
                {
                    考生范围表 area = new 考生范围表();
                    area.ID = Guid.NewGuid();
                    area.相关ID = 相关ID;
                    area.考生ID = examineeId;
                    listNewArea.Add(area);
                }
                foreach (var newArea in listNewArea)
                {
                    db.考生范围表.AddObject(newArea);
                }
            }
        }

        #endregion
    }
}
