using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.WebSocket.Infrastructure;

namespace Oryx.FastAdmin.Core3.Controllers
{
    public class ChatController : Controller
    {
        OryxWebSocketPool oryxWebSocketPool;
        public ChatController(OryxWebSocketPool _oryxWebSocketPool)
        {
            oryxWebSocketPool = _oryxWebSocketPool;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Kefu()
        {
            return View();
        }
        public async Task PushWSMsg()
        {

        }
    }
}