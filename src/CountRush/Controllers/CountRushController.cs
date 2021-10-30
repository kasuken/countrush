using CountRush.Services;
using DotBadge;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CountRush.Controllers
{
    [Route("l")]
    [ApiController]
    public class CountRushController : ControllerBase
    {
        ICountRushRepository countRushRepository;

        public CountRushController(ICountRushRepository _countRushRepository)
        {
            countRushRepository = _countRushRepository;
        }

        [HttpGet("badge")]
        public IActionResult GetBadge(string repository)
        {
            var visitors = countRushRepository.RetrieveVisitors(repository);

            var bp = new BadgePainter();
            string svg = bp.DrawSVG("visitors", visitors.ToString(), ColorScheme.BrightGreen, Style.Flat);

            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Content-Type", "image/svg+xml");
            Response.Headers.Add("Expires", "0");
            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");

            return Content(svg, "image/svg+xml; charset=utf-8");
        }
    }
}
