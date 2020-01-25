using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;

namespace GenerateQRCode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string inputText)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator oQRCodeGenerator = new QRCodeGenerator();
                QRCodeData oQRCodeData = oQRCodeGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q);
                QRCode oQRCode = new QRCode(oQRCodeData);
                using(Bitmap oBitmap = oQRCode.GetGraphic(20))
                {
                    oBitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return View();
        }

    }
}
