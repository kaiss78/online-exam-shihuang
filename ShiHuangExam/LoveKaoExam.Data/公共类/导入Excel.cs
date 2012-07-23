using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.IO;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace LoveKaoExam.Data
{
    public class 导入Excel
    {

        /// <summary>
        /// 返回已存在的编号集合
        /// </summary>
        /// <param name="fs">Excel文件流</param>
        /// <param name="type">0学生，1员工</param>
        /// <returns></returns>
        public static List<string> 导入excel(Stream stream, int type)
        {
            try
            {
                HSSFWorkbook workBook = new HSSFWorkbook(stream);
                HSSFSheet sheet = (HSSFSheet)workBook.GetSheetAt(0);
                //判断是否符合格式
                Cell cell0 = sheet.GetRow(0).GetCell(0);
                if (cell0 == null)
                {
                    抛出格式不符异常();
                }
                string cell0Value = 得到流中列值(cell0.CellType, sheet, 0, 0);
                if (cell0Value != "学号" && cell0Value != "编号")
                {
                    抛出格式不符异常();
                }
                Cell cell1 = sheet.GetRow(0).GetCell(1);
                if (cell1 == null)
                {
                    抛出格式不符异常();
                }
                string cell1Value = 得到流中列值(cell1.CellType, sheet, 0, 1);
                if (cell1Value != "姓名")
                {
                    抛出格式不符异常();
                }
                Cell cell2 = sheet.GetRow(0).GetCell(2);
                if (cell2 == null)
                {
                    抛出格式不符异常();
                }
                string cell2Value = 得到流中列值(cell2.CellType, sheet, 0, 2);
                if (cell2Value != "性别")
                {
                    抛出格式不符异常();
                }
                Cell cell3 = sheet.GetRow(0).GetCell(3);
                if (cell3 == null)
                {
                    抛出格式不符异常();
                }
                string cell3Value = 得到流中列值(cell1.CellType, sheet, 0, 3);
                if (cell3Value != "班级"&&cell3Value!="部门")
                {
                    抛出格式不符异常();
                }
                LoveKaoExamEntities db = new LoveKaoExamEntities();
                //取出班级,学号
                List<string> listClassName = new List<string>();
                List<string> listNum = new List<string>();
                for (int k = sheet.FirstRowNum + 1; k < sheet.LastRowNum + 1; k++)
                {
                    string className = string.Empty;
                    string classType = string.Empty;
                    if (type == 0)
                    {
                        classType = "班级";
                    }
                    else
                    {
                        classType = "部门";
                    }
                    Cell cellClass = sheet.GetRow(k).GetCell(3);
                    if (cellClass == null)
                    {
                        throw new Exception("第" + k + "行" + classType + "未填写，请先填写，然后再导入！");
                    }
                    CellType classCellType = cellClass.CellType;
                    className = 得到流中列值(classCellType, sheet, k, 3);                                      
                    if (className.Length > 16)
                    {
                        throw new Exception("第" + k + "行" + classType + "名称长度大于16，请填写16以内的名称！");
                    }
                    string num = string.Empty;
                    string numType = string.Empty;
                    if (type == 0)
                    {
                        numType = "学号";
                    }
                    else
                    {
                        numType = "编号";
                    }
                    Cell cellNum=sheet.GetRow(k).GetCell(0);
                    if (cellNum == null)
                    {
                        throw new Exception("第" + k + "行" + numType + "未填写，请先填写，然后再导入！");
                    }
                    CellType numCellType = cellNum.CellType;
                    num = 得到流中列值(numCellType, sheet, k, 0);                                       
                    if (num.Length < 4 || num.Length > 16)
                    {
                        throw new Exception("第" + k + "行" + numType + "长度不符，必须是4到16位！");
                    }
                    else if (Regex.IsMatch(num, @"[^a-zA-Z0-9]"))
                    {
                        throw new Exception("第" + k + "行" + numType + "格式不符，" + numType + "必须只含数字或字母！");
                    }
                    listClassName.Add(className);
                    listNum.Add(num);
                }
                //有不存在的班级，则先添加
                listClassName = listClassName.Distinct().ToList();
                List<string> listExistClassName = db.部门表.Where(a => listClassName.Contains(a.名称))
                    .Select(a => a.名称).ToList();
                List<string> listNotExistClassName = listClassName.Except(listExistClassName).ToList();
                foreach (string className in listNotExistClassName)
                {
                    部门表 department = new 部门表();
                    department.ID = Guid.NewGuid();
                    department.名称 = className;
                    department.添加人ID = 用户信息.CurrentUser.用户ID;
                    department.添加时间 = DateTime.Now;
                    db.部门表.AddObject(department);
                }
                db.SaveChanges();
                //已存在的学生则不再导入
                List<string> listExistNum = db.用户表.Where(a => listNum.Contains(a.编号)).Select(a => a.编号).ToList();
                List<string> listNotExistNum = listNum.Except(listExistNum).ToList();
                List<部门表> listClass = db.部门表.Where(a => listClassName.Contains(a.名称)).ToList();
                for (int i = sheet.FirstRowNum+1; i < sheet.LastRowNum+1; i++)
                {
                    CellType numCellType = sheet.GetRow(i).GetCell(0).CellType;
                    string stuNum = 得到流中列值(numCellType, sheet, i, 0);
                   
                    if (listNotExistNum.Contains(stuNum))
                    {
                        Cell cellName=sheet.GetRow(i).GetCell(1);
                        if (cellName == null)
                        {
                            throw new Exception("第" + i + "行姓名未填写，请先填写，然后再导入！");
                        }
                        CellType nameCellType = cellName.CellType;
                        string name = 得到流中列值(nameCellType, sheet, i, 1);                      
                        if (name.Length < 2 || name.Length > 8)
                        {
                            throw new Exception("第" + i + "行姓名长度不符，必须是2到8位！");
                        }
                        else if (Regex.IsMatch(name, @"[^a-zA-Z\u4e00-\u9fa5]"))
                        {
                            throw new Exception("第" + i + "行姓名格式不符，姓名必须只含中文或字母！");
                        }
                        用户表 user = new 用户表();
                        user.ID = Guid.NewGuid();
                        user.编号 = stuNum;
                        user.姓名 = name;
                        user.密码 = stuNum;
                        Cell cellSex=sheet.GetRow(i).GetCell(2);
                        if (cellSex == null)
                        {
                            throw new Exception("第" + i + "行性别未填写，请先填写，然后再导入！");
                        }
                        CellType sexCellType = cellSex.CellType;
                        string sex = 得到流中列值(sexCellType, sheet, i, 2);                       
                        if (sex == "男")
                        {
                            user.性别 = 1;
                        }
                        else
                        {
                            user.性别 = 2;
                        }
                        CellType classCellType = sheet.GetRow(i).GetCell(3).CellType;
                        string className = 得到流中列值(classCellType, sheet, i, 3);
                        user.部门ID = listClass.Where(a => a.名称 == className).First().ID;
                        user.角色 = 0;
                        user.邮箱 = "";
                        user.添加时间 = DateTime.Now;
                        db.用户表.AddObject(user);
                    }
                }
                db.SaveChanges();
                return listExistNum;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Table"))
                {
                    throw new Exception("格式错误，请按模板格式来导入！");
                }
                else
                {
                    throw ex;
                }
            }
        }



        private static string 得到流中列值(CellType cellType,Sheet sheet,int rowIndex,int cellIndex)
        {
            string value = string.Empty;
            Cell cell=sheet.GetRow(rowIndex).GetCell(cellIndex);
            switch (cell.CellType)
            {
                case CellType.STRING:
                case CellType.BLANK:
                    {
                        value = cell.StringCellValue;
                        break;
                    }
                case CellType.BOOLEAN:
                    {
                        value = cell.BooleanCellValue.ToString();
                        break;
                    }
                case CellType.NUMERIC:
                    {
                        value = cell.NumericCellValue.ToString();
                        break;
                    }
            }
            return value;
        }



        private static void 抛出格式不符异常()
        {
            throw new Exception("格式不正确，请确认和模板格式一致再导入！");
        }

    }
}
