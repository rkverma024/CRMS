using CRMS.Core.Models;
using CRMS.Core.RepositoryInterface;
using CRMS.Core.ServiceInterface;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRMS.Services.Services
{
    public class AuditLogsService : IAuditLogsService
    {
        IAuditLogsRepository auditLogsRepository;
        public AuditLogsService(IAuditLogsRepository AuditLogsRepository)
        {
            auditLogsRepository = AuditLogsRepository;
        }

        public IEnumerable<AuditLogs> GetListOfAuditLogs()
        {
            return auditLogsRepository.Collection();
        }

        public void CreateAuditLog(string error)
        {           
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            RouteData routeData = urlHelper.RouteCollection.GetRouteData(currentContext);
            AuditLogs model = new AuditLogs();
            model.UserId = (Guid)HttpContext.Current.Session["Id"];
            model.Comments = "Controller :: " + routeData.Values["controller"].ToString() + " Action :: " + routeData.Values["action"].ToString();
            model.ClientAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            model.Parameters = routeData.Values["Id"].ToString();
            model.Url = HttpContext.Current.Request.Path;
            model.HttpMethod = HttpContext.Current.Request.HttpMethod;
            model.BrowserInfo = HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;
            model.ExecutionTime = DateTime.UtcNow;
            model.ExecutionDuration = DateTime.Now.Millisecond;
            model.HttpStatusCode = HttpContext.Current.Response.StatusCode;
            model.Exception = error;
            auditLogsRepository.Insert(model);
            auditLogsRepository.Commit();
        }

        public IEnumerable<AuditLogsIndexViewModel> IndexOfAuditLogs()
        {
            return auditLogsRepository.GetAllAuditLogList();
        }

        public AuditLogsIndexViewModel AuditLogDetailsById(Guid Id)
        {
            return auditLogsRepository.GetAuditLogDetailsById(Id);
        }

        public List<AuditLogs> IndexErrorLog()
        {
            return auditLogsRepository.Collection().Where(x => x.Exception != null).OrderByDescending(x => x.CreatedOn).ToList();
        }
    }
}