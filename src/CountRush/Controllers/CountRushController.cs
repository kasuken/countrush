using CountRush.Services;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("badge")]
        public IActionResult GetBadge(string repository)
        {

            Bitmap bitmap = new Bitmap(150, 20, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(0, 0, 300, 50));

            MemoryStream ms = new MemoryStream();
            
                bitmap.Save(ms, ImageFormat.Png);
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(ms.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

            ms.Position = 0;

            var watermarkedStream = new MemoryStream();
            using (var img = Image.FromStream(ms))
            {
                using (var graphic = Graphics.FromImage(img))
                {
                    var font = new Font(FontFamily.GenericSansSerif, 18, FontStyle.Bold, GraphicsUnit.Pixel);
                    var color = Color.FromArgb(255, 0, 0, 0);
                    var brush = new SolidBrush(color);
                    //var point = new Point(img.Width - 120, img.Height - 30);
                    var point = new Point(0, 0);

                    graphic.DrawString("12345 visitors", font, brush, point);
                    img.Save(watermarkedStream, ImageFormat.Png);
                }
            }


            //return Ok();

            watermarkedStream.Position = 0;
            return File(watermarkedStream, "image/png");
        }
    }
}
