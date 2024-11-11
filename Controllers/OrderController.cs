using e_commerce_website.Helper.Order;
using e_commerce_website.Services.Interfaces;
using e_commerce_website.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.CodeAnalysis;
using System;
using e_commerce_website.Hubs;
using e_commerce_website.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace e_commerce_website.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IOrderService _orderService;
        private readonly IHubContext<ChatHub> _hubContext;

        private Guid adminId = new Guid("4557893f-1f56-4b6f-bb3b-caefd62c8c49");
        public OrderController(IOrderService orderService, [NotNull] IHubContext<ChatHub> hubContext)
        {
            _orderService = orderService;
            _hubContext = hubContext;
        }
        [HttpPost]
        public async Task<IActionResult> create([FromBody] OrderCreateRequest request)
        {

            var orderId = await _orderService.Create(request);
            if (orderId == 0)
            {
                return BadRequest("Đặt hàng thất bại!");
            }

            var notify = new NotifyViewModel()
            {
                notify = $"Có khách vừa mới đặt hàng!",
                link = "/admin/order-manage/order-not-confirm",
                //senderId = request.userId.HasValue ? request.userId.Value ? null,
                receiverId = adminId,
                isViewed = false,
                status = NotifyStatus.order,
            };
            await _hubContext.Clients.All.SendAsync("ReceiveNotify", notify);
            return Ok("Đặt hàng thành công! Admin sẽ thông báo đến bạn thông qua số điện thoại hoặc gmail! Trân trọng!");
        }
        [HttpGet("GetOrderListByUserId/{userId}")]
        public async Task<IActionResult> GetOrderListByUserId(Guid userId)
        {
            var list = await _orderService.GetOrderListByUserId(userId);
            return Ok(list);
        }
        //// GET: api/<OrderController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<OrderController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<OrderController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<OrderController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<OrderController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
