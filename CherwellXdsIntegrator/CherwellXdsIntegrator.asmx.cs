using CherwellXdsIntegrator.Helpers;
using CherwellXdsIntegrator.za.co.xds.web.www;

using System;
using System.IO;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;

namespace CherwellXdsIntegrator
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : WebService
    {
        private readonly XDSConnectWS _webService = new XDSConnectWS();
        private int _enquiryId; 
        private int _enquiryResultId;   
        private const string XdsUrl = "http://localhost/CherwellXdsIntegrator/CherwellXdsIntegrator.asmx";
        private const string CherwellUrl = "http://localhost/cherwellservice/api.asmx";

        [WebMethod]
        public string CorporateScore(string xdsUrl, string cherwellUrl, string cmsUsrName, string cmsUsrPwd,
            string xdsUsrName, string xdsUsrPw, string reg1,
            string reg2, string reg3, string businessName, string vatNo, string reference, string ticket,
            string productionId, bool attachXml, bool attachPdf, string recId)
        {
            _webService.Url = XdsUrl;
            var myMatch = _webService.ConnectBusinessMatch(ticket, reg1, reg2, reg3, businessName, vatNo, string.Empty,
                reference, string.Empty);
            _enquiryId = int.Parse(XmlReaders.FindXmlValue(myMatch, "EnquiryID"));
            _enquiryResultId = int.Parse(XmlReaders.FindXmlValue(myMatch, "EnquiryResultID"));

            var xdsResult = _webService.ConnectGetResult(ticket, _enquiryId, _enquiryResultId, int.Parse(productionId),
                string.Empty);
            var attachments = new Attachements();
            if (attachXml)
            {
                attachments.AttachXml(cherwellUrl, cmsUsrName, cmsUsrPwd, recId, xdsResult);
            }

            if (attachPdf)
            {
                attachments.AttachPdf(cherwellUrl, cmsUsrName, cmsUsrPwd, recId, ticket, productionId, _enquiryId, _enquiryResultId);
            }

            var results = new Results
            {
                Score = XmlReaders.FindXmlValue(xdsResult, "FinalScore"),
                EnquiryID = _enquiryId.ToString(),
                EnquiryResultID = _enquiryResultId.ToString(),
                Judgements = XmlReaders.FindJudgements(xdsResult, "JudgmentsCount").ToString()
            };
            var sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                var serializer = new XmlSerializer(results.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, results);
            }
            catch (Exception)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                tw?.Close();
            }

            return sw.ToString();
        }

        [WebMethod]
        public string ConsumerScore(string xdsUrl, string cherwellUrl, string cmsUsrName, string cmsUsrPwd,
            string xdsUsrName, string xdsUsrPw, string reason, string idNo, string dob, string passportNumber,
            string reference, string ticket, string firstName, string lastname,
            string productionId, bool attachXml, bool attachPdf, string recId)
        {
            _webService.Url = XdsUrl;
            var myMatch = _webService.ConnectConsumerMatch(ticket, reason, int.Parse(productionId), idNo,
                passportNumber,
                firstName, lastname, dob, reference, string.Empty);

            _enquiryId = int.Parse(XmlReaders.FindXmlValue(myMatch, "EnquiryID"));
            _enquiryResultId = int.Parse(XmlReaders.FindXmlValue(myMatch, "EnquiryResultID"));

            var xdsResult = _webService.ConnectGetResult(ticket, _enquiryId, _enquiryResultId, int.Parse(productionId),
                string.Empty);

            var attachments = new Attachements();
            if (attachXml)
            {
                attachments.AttachXml(CherwellUrl, cmsUsrName, cmsUsrPwd, recId, xdsResult);
            }

            if (attachPdf)
            {
                attachments.AttachPdf(CherwellUrl, cmsUsrName, cmsUsrPwd, recId, ticket, productionId, _enquiryId, _enquiryResultId);
            }

            var results = new Results
            {
                Score = XmlReaders.FindXmlValue(xdsResult, "FinalScore"),
                EnquiryID = _enquiryId.ToString(),
                EnquiryResultID = _enquiryResultId.ToString(),
                Judgements = XmlReaders.FindJudgements(xdsResult, "JudgmentsCount").ToString()
            };
            var sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                var serializer = new XmlSerializer(results.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, results);
            }
            catch (Exception)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                tw?.Close();
            }

            return sw.ToString();
        }
    }
}