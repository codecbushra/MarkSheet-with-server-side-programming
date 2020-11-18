using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Marksheet.server
{
    /// <summary>
    /// Summary description for Marksheet
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class Marksheet : System.Web.Services.WebService
    {
         [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet =true)]
        public string CalculatePercentage()
        {
            string subjectStr = HttpContext.Current.Request.Params["request"];
            List<MarksheetModel> subjects = JsonConvert.DeserializeObject<List<MarksheetModel>>(subjectStr);
            double minMarks = subjects[0].subjectMarksObtained;
            double maxMarks = subjects[0].subjectMarksObtained;
            string minMarksSubject=subjects[0].subjectName;
            string maxMarksSubject=subjects[0].subjectName;
            double totalMarks = 0.0;

            for (int i = 0; i < subjects.Count; i++)
            {
                totalMarks += subjects[i].subjectMarksObtained;
                if (minMarks > subjects[i].subjectMarksObtained)
                {
                    minMarks = subjects[i].subjectMarksObtained;
                    minMarksSubject = subjects[i].subjectName;
                }

                if (maxMarks < subjects[i].subjectMarksObtained)
                {
                    maxMarks = subjects[i].subjectMarksObtained;
                    maxMarksSubject = subjects[i].subjectName;
                }
            }
            double percentage = (totalMarks / (subjects.Count * 100)) * 100;

            MarksheetModel marksheetModel = new MarksheetModel();
            marksheetModel.percentage = percentage;
            marksheetModel.minMarks = minMarks;
            marksheetModel.maxMarks = maxMarks;
            marksheetModel.minMarksSubject = minMarksSubject;
            marksheetModel.maxMarksSubject = maxMarksSubject;

            string str = JsonConvert.SerializeObject(marksheetModel);
            return str;
            }
        public class MarksheetModel
        {
            public string subjectName { get; set; }
            public double subjectMarksObtained { get; set; }
            public double percentage { get; set; }
            public double minMarks { get; set; }
            public double maxMarks { get; set; }
            public string minMarksSubject { get; set; }
            public string maxMarksSubject { get; set; }

            

        }
    }

}
