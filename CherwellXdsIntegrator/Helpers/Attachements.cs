using System;
using CherwellXdsIntegrator.za.co.xds.web.www;

namespace CherwellXdsIntegrator.Helpers
{
    public class Attachements
    {   
        private readonly XDSConnectWS _webService = new XDSConnectWS();

        public void AttachXml(string url, string usrName, string usrPwd, string regId, string xmlFile)
        {

            var cherwellApi = new CherwellWebConnector();

            var dataBytes = System.Text.Encoding.UTF8.GetBytes(xmlFile);
            var attacmentdata = Convert.ToBase64String(dataBytes);

            if (cherwellApi.Login(url, usrName, usrPwd))
            {
                cherwellApi.AttachFile("CustomerCreditRating", regId, "Record.xml", attacmentdata);
            }

            cherwellApi.Logout();
        }

        public void AttachPdf(string url, string usrName, string usrPwd, string regId, string ticket,
            string productionId,int enquiryId,int enquiryResultId)
        {
            var cherwellApi = new CherwellWebConnector();
            if (!cherwellApi.Login(url, usrName, usrPwd)) return;
            var xdsResult = _webService.ConnectGetResultBinary(ticket, enquiryId, enquiryResultId,
                int.Parse(productionId),
                string.Empty);
            var attachementData = Convert.ToBase64String(xdsResult);

            cherwellApi.AttachFile("CustomerCreditRating", regId, "Record.pdf", attachementData);
            cherwellApi.Logout();
            
        }
    }
}