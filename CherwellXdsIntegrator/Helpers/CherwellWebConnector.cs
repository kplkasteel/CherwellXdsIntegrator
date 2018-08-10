using System.Net;
using CherwellXdsIntegrator.CherwellAPI;

namespace CherwellXdsIntegrator.Helpers
{
    public class CherwellWebConnector
    {
        private api _cherwellService;

        internal bool Login(string url,string username, string password)
        {
            _cherwellService = new api
            {
                CookieContainer = new CookieContainer(),
                Url = url
            };
            return _cherwellService.Login(username, password);
        }

        internal bool Logout()
        {
            return !_cherwellService.Logout();
        }

        public string AttachFile(string busObj, string recId, string attachementName, string attachementData)
        {
            return _cherwellService.AddAttachmentToRecord(busObj, recId, attachementName, attachementData) ? "True" : GetLastError();
        }

        private string GetLastError()
        {
            return _cherwellService.GetLastError();
        }
    }
}