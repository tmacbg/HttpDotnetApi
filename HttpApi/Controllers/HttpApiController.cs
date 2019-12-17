using System;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;
using HttpApi.Models;
using HttpApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HttpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpApiController : ControllerBase
    {
        private readonly HtmlService _htmlService;

        public HttpApiController(HtmlService htmlService)
        {
            _htmlService = htmlService;
        }

        [HttpGet]
        public ActionResult<List<Html>> Get() =>
            _htmlService.Get();

        [HttpGet("{id:length(24)}", Name = "GetSingleHtml")]
        public ActionResult<Html> Get(String id) =>
            _htmlService.Get(id);

        [HttpPost("data/createHtml", Name = "Create")]
        public ActionResult<Html> Create(Html html)
        {
            _htmlService.Create(html);

            return CreatedAtRoute("GetSingleHtml", new { id = html.Id.ToString() }, html);
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<Response> GetData(RequestModel requestModel)
        {
	    //var vpnStatus = DeleteVPN();

            var country = "UK";
	    
            if (requestModel.country != null)
            {
                country = requestModel.country;
            }

            //var output = CallVPN(country);

            var url = requestModel.url;

            HtmlWeb web = new HtmlWeb();
            var lastStatusCode = HttpStatusCode.OK;
            web.PostResponse = (request, response) =>
            {
                if (response != null)
                {
                    lastStatusCode = response.StatusCode;
                }
            };

            var htmlDoc = web.Load(url);

            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//html");

            if (requestModel.selector != null )
            {
                htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//"+requestModel.selector);
            }

            var resp = new Response();
            resp.data = htmlBody.OuterHtml;
            resp.statusCode = (int)lastStatusCode;
            resp.debug = "test";
            resp.country = country;

            var siteData = new Html();
            siteData.rawHtml = resp.data;
            siteData.url = url;
            siteData.selector = requestModel.selector;
            siteData.Date = DateTime.Now;

            _htmlService.Create(siteData);

            // Get the data from the DB

            var filter = "{url: '"+url+"', selector: '"+ requestModel.selector+"' }";

            var dbData = _htmlService.GetSource(filter); 

            resp.data = dbData.rawHtml;

            return resp;
        }

	public String DeleteVPN()
        {

            var cmd = "killall -9 openvpn";

            var output = cmd.Bash();

            return output;

        }

        public String CallVPN(string countryCode)
        {
            var vpn = "UK.London.TCP.ovpn";

            if (countryCode == "DE")
            {
                vpn = "Germany.Hesse.Frankfurt.TCP.ovpn";
            }
            else if (countryCode == "ES")
            {
                vpn = "Spain.Alicante.TCP.ovpn";
            }

            var cmd = "/root/openvpn.sh " + vpn;
            var outPut = cmd.Bash();

            return outPut;
        }
    }
}
