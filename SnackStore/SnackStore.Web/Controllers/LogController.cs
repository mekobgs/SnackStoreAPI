using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnackStore.Core.Interfaces;
using SnackStore.Core.Models;

namespace SnackStore.Web.Controllers
{
    [Route("logs")]
    public class LogController : BaseController
    {
        private readonly IRepository<PriceLog> _priceUpdateLogRepository;
        private readonly IRepository<PurchaseLog> _purchaseLogRepository;

        public LogController(IRepository<PurchaseLog> purchaseLogRepository, IRepository<PriceLog> priceUpdateLogRepository)
        {
            _priceUpdateLogRepository = priceUpdateLogRepository;
            _purchaseLogRepository = purchaseLogRepository;
        }

        [HttpGet]
        [Route("price")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetPriceUpdateLogs()
        {
            return Ok(_priceUpdateLogRepository.FindAll());
        }

        [HttpGet]
        [Route("purchase")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetPurchaseLogs()
        {
            return Ok(_purchaseLogRepository.FindAll());
        }
    }
}