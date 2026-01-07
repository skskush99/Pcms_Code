using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Xml;
using Authentication.ServiceBus.UnitOfWork;

namespace Authentication.Service.Controllers
{
    public class EsignController : Controller
    {
        private IConfiguration Configuration;
        private readonly IUnitOfWorkService _unitOfWork;
        public EsignController(IConfiguration _configuration, IUnitOfWorkService unitOfWork)
        {
            Configuration = _configuration;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            try
            {
                var signedXMLData = Request.Form["esignData"];              

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(signedXMLData);
                XmlElement root = doc.DocumentElement;
                string txn = root.Attributes["txn"].Value;

                byte[] byteArray = Encoding.UTF8.GetBytes(signedXMLData);
                string base64String = Convert.ToBase64String(byteArray);
                var result = _unitOfWork.Esign.AddEsignData(txn, base64String);
                
                var ReturnUrl = this.Configuration["EsignAngualarReturnURL"] + "?esignData=" + txn;
                return Redirect(ReturnUrl);
            }
            catch(Exception ex)
            {
                var ReturnUrl = this.Configuration["EsignAngualarReturnURL"] + "?esignData=" + ex.Message;
                return Redirect(ReturnUrl);
            }
        }
    }
}
