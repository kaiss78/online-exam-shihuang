using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using LoveKaoExam.Data;
using Webdiyer.WebControls.Mvc;
using LoveKaoExam.Library.CSharp;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.UI;
using LoveKao.Page;


namespace LoveKaoExam.Controllers.Admin
{
    /// <summary>
    /// �û�������
    /// </summary>
    [Authorize(Roles = "����Ա")]
    public class UserController : BaseController
    {
        /// <summary>
        /// �����˻�
        /// </summary>
        /// <returns>������ҳ��ͼ</returns>
        public ActionResult Add()
        {
            return View("~/Views/Admin/User/Add.aspx");
        }

        /// <summary>
        /// ����/�༭�˻���ͼ
        /// </summary>
        /// <param name="id">Ҫ�༭�û���ID</param>
        /// <returns>���ؿؼ���ͼ</returns>
        public ActionResult UCEditor(string id)
        {
            /* ��ֹҳ�汻���� */
            BasePage.PageNoCache();

            �û� model = new �û�();
            Guid �û�ID = LKExamURLQueryKey.SToGuid(id);


            /* ����û�ID��Ϊ�� �򷵻��û�������Ϣ */
            if (�û�ID != Guid.Empty)
            {
                model = �û�.�õ��û�������Ϣ����ID(�û�ID);
            }
            else if (LKExamURLQueryKey.GetString("rtype") == "1")
            {
                model.��ɫ = 1;
                model.�Ա� = 1;
            }
            /* �����û�����Ϣ */
            else
            {
                model.��ɫ = 0;
                model.�Ա� = 1;
            }

            ViewData["����ID"] = new SelectList(����.��ѯ����(), "ID", "����", model.����ID);

            return View("~/Views/Admin/User/UCEditor.ascx", model);
        }

        /// <summary>
        /// ����/�༭�˻�
        /// </summary>
        /// <param name="model">�û�</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UCEditor(�û� model)
        {
            #region �û���ɫ
            if (model.��ɫ == 0)
            {
                model.���� = "";
            }
            else if (model.��ɫ == 1)
            {
                model.����ID = null;
                model.���� = model.���� == null ? "" : model.����;
            }
            #endregion

            #region try/catch(){}
            try
            {
                int returnValue;

                #region ����/�޸��û�Model
                if (model.ID == Guid.Empty)
                {
                    returnValue = �û�.����û�(model);
                }
                else
                {
                    returnValue = �û�.�޸��û�������Ϣ(model);
                }
                #endregion

                #region ������ֵ
                if (returnValue == 1)
                {
                    string ��ɫ��� = LKExamEnvironment.��ɫ���(model.��ɫ);
                    return LKPageJsonResult.Exists(��ɫ���, model.���);
                }
                else if (returnValue == 2)
                {
                    return LKPageJsonResult.Exists("�����ַ", model.����);
                }
                else
                {
                    return new JsonResult
                    {
                        Data = new { result = true, info = model }
                    };
                }
                #endregion

            }
            catch (Exception ex)
            {
                /* �����쳣��Json��ʽ�ַ��� */
                return LKPageJsonResult.Exception(ex);
            }
            #endregion
        }

        /// <summary>
        /// ����/�޸��˻���Ƕ��IFrame����
        /// ����/Views/Admin/User/IFrame.aspx��ͼ
        /// </summary>
        /// <returns></returns>
        public ActionResult IFrame()
        {
            /*
             *	���ز�����ϢǶ��IFrame������ͼ
             *--------------------*/
            return View("~/Views/Admin/User/IFrame.aspx");
        }

        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="id">�û�ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(string id)
        {
            #region try/catch(){}
            try
            {
                Guid �û�ID = LKExamURLQueryKey.SToGuid(id);
                �û�.ɾ���û�(�û�ID);

                /* ����ִ�гɹ���Json�ַ��� */
                return LKPageJsonResult.Success();
            }
            catch (Exception ex)
            {
                /* �����쳣��Json��ʽ�ַ��� */
                return LKPageJsonResult.Exception(ex);
            }
            #endregion
        }

        /// <summary>
        /// ������Ϣ�б�
        /// </summary>
        /// <param name="id">��ǰҳ��</param>
        /// <returns></returns>
        public ActionResult examinee(int? id)
        {
            /* ��ֹҳ�汻���� */
            BasePage.PageNoCache();

            /* ������Ϣ��ͼ���� */
            string pageTitle = LKExamEnvironment.�������� + "��Ϣ";

            /* ����PagedList */
            PagedList<����> ����PagedList = ������Ϣ�б�(id);

            /* ��Ϣ��ͼ */
            LKExamMvcPagerData<����>.������Ϣ(����PagedList, MvcPager����Ŀ��.����Ա_������Ϣ, ViewData, pageTitle);

            /* �����Ajax�첽�����򷵻ؿؼ���ͼ */
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Admin/User/UCExaminee.ascx", ����PagedList);
            }

            /* ���ؿ�����Ϣ��ͼ */
            return View("~/Views/Admin/User/Examinee.aspx", ����PagedList);

        }

        /// <summary>
        /// ������Ϣ�б�
        /// </summary>
        /// <param name="id">��ǰҳ��</param>
        /// <returns></returns>
        public ActionResult examiner(int? id)
        {
            /* ��ֹҳ�汻���� */
            BasePage.PageNoCache();

            /* ������Ϣ��ͼ���� */
            string pageTitle = LKExamEnvironment.�������� + "��Ϣ";

            /* ����PagedList */
            PagedList<����> ����PagedList = ������Ϣ�б�(id);

            /* ��Ϣ��ͼ */
            LKExamMvcPagerData<����>.������Ϣ(����PagedList, MvcPager����Ŀ��.����Ա_������Ϣ, ViewData, pageTitle);

            /* �����Ajax�첽�����򷵻ؿؼ���ͼ */
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Admin/User/UCExaminer.ascx", ����PagedList);
            }

            /* ���ؿ���Ϣ��ͼ */
            return View("~/Views/Admin/User/Examiner.aspx", ����PagedList);
        }

        /// <summary>
        /// ���뵼������Excel��Ϣ
        /// </summary>
        /// <returns></returns>
        public ActionResult Imexport()
        {
            /* �������� */
            string �������� = LKExamURLQueryKey.GetString("handleType");

            /*
              *��������
              *  (1)1��ʾ��������Excel
              *  (2)2��ʾ��ѯ������Ϣ
              */
            if (�������� == "1")
            {
                Guid ����ID = LKExamURLQueryKey.����ID();
                string ��������;
                DataSet dataSet = ����.��ѯ��������(����ID, out ��������);
                string sFileName = �������� + "(" + LKExamEnvironment.�������� + "��Ϣ)";
                new LKExamOffice().����������Ϣ��Execl(dataSet, sFileName);
            }

            /* ���뵼������Excel��Ϣ��ͼ���� */
            string pageTitle = "���뵼��" + LKExamEnvironment.�������� + "Excel��Ϣ";

            /* ����PagedList */
            PagedList<����> ����PagedList = ����Excel�б�();

            /* ��Ϣ��ͼ */
            LKExamMvcPagerData<����>.������Ϣ(����PagedList, MvcPager����Ŀ��.����Ա_��������Excel, ViewData, pageTitle);

            /* ���ؿ�����Ϣ��ͼ */
            return View("~/Views/Admin/User/Imexport.aspx", ����PagedList);
        }

        /// <summary>
        /// ���뿼��Excel
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void ImportExcel()
        {
            JsonResult jsonResult = null;
            try
            {
                /* �ͻ��������ļ� */
                HttpPostedFileBase postFile = Request.Files["InputFile"];

                /* ��ȡ�ļ���չ�� */
                string ext = System.IO.Path.GetExtension(postFile.FileName);

                /* �����չ�������Ϲ涨��ʽ ���׳��쳣 */
                if (ext != ".xls" && ext != ".xlsx")
                {
                    throw new Exception("Excel�ļ���ʽ����ȷ�����ϴ�xls,xlsx��ʽ��Excel");
                }

                /* ���뿼��Excel */
                List<string> �Ѵ���ѧ�ż��� = ����Excel.����excel(postFile.InputStream, LKExamEnvironment.�Ƿ�ΪѧУ ? 0 : 1);
                object info = new { �Ѵ��ڱ������ = �Ѵ���ѧ�ż��� };

                /* �ɹ����JsonResult */
                jsonResult = LKPageJsonResult.Success(info);
                
                /* �ͷ���ռ����Դ */
                postFile.InputStream.Dispose();
                postFile.InputStream.Close();
                postFile = null;
            }
            catch (Exception ex)
            {
                jsonResult = LKPageJsonResult.Exception(ex);
            }
            /* ����ת��ΪJson��ʽ�ַ��� */
            string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(jsonResult.Data);

            /* ��Json��ʽ�ַ��������ҳ�� */
            Response.Write(jsonText);
        }
    }
}

