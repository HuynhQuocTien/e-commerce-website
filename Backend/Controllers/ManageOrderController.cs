using e_commerce_website.Helper.Order;
using e_commerce_website.Models;
using e_commerce_website.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace e_commerce_website.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ManageOrderController : ControllerBase
    {
        private readonly IManageOrderService _manageOrderService;
        private readonly IEmailSender _emailSender;

        public ManageOrderController(IManageOrderService manageOrderService, IEmailSender emailSender)
        {
            _manageOrderService = manageOrderService;
            _emailSender = emailSender;

        }
        //// GET: api/<ManageOrderController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<ManageOrderController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ManageOrderController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ManageOrderController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ManageOrderController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        [HttpGet("GetAllOrderNotConfirm")]
        public async Task<IActionResult> GetAllOrderNotConfirm()
        {
            var data = await _manageOrderService.GetAllOrderNotConfirm();
            return Ok(data);
        }
        [HttpGet("GetAllOrderDelivering")]
        public async Task<IActionResult> GetAllOrderDelivering()
        {
            var data = await _manageOrderService.GetAllOrderDelivering();
            return Ok(data);
        }
        [HttpGet("GetAllOrderSuccess")]
        public async Task<IActionResult> GetAllOrderSuccess()
        {
            var data = await _manageOrderService.GetAllOrderSuccess();
            return Ok(data);
        }
        [HttpGet("GetOrderDetailByOrderId")]
        public async Task<IActionResult> GetOrderDetailByOrderId(int orderId)
        {
            var result = await _manageOrderService.GetOrderDetailByOrderId(orderId);
            return Ok(result);
        }
        [HttpPost("ConfirmShippingAndSendMailBillOrder")]
        public async Task<IActionResult> ConfirmShippingAndSendMailBillOrder(StatusOrderRequest request)
        {

            var result = await _manageOrderService.confirmShippingAndSendMailBillOrder(request);
            if (result.success)
            {
                var listData = await _manageOrderService.GetOrderDetailByOrderId(request.orderId);
                var message = new Message(new String[] { result.email }, "ONLINE SHOP - Hóa Đơn Khách Hàng - " + result.customer, string.Empty);
                var flag = await _emailSender.SendMailOrderBill(message, listData, result.total);
                if (flag == false)
                {
                    await _manageOrderService.SetStatusNotConfirm(request.orderId, 0);
                }
                return Ok(flag);
            }
            return Ok(result.success);
        }
        [HttpPost("CancelOrder")]
        public async Task<IActionResult> CancelOrder(CancelOrderRequest request)
        {
            var result = await _manageOrderService.CancelOrder(request);
            if (result)
            {
                var order = await _manageOrderService.GetOrderByOrderId(request.orderId);
                var customer = String.IsNullOrEmpty(order.guess) ? order.user.displayname : order.guess;
                var note = String.IsNullOrEmpty(request.note) ? $"Đơn hàng có mã {request.orderId} của bạn đã bị hủy bởi Admin!" :
                    $"Đơn hàng có mã {request.orderId} của bạn đã bị hủy bởi Admin. Do " + request.note;
                var message = new Message(new string[] { order.email }, "ONLINE SHOP - Thông Báo Khách Hàng - "
                    + customer, note);
                var flag = await _emailSender.SendMailOrderBill(message, null, 0);
                if (flag == false)
                {
                    await _manageOrderService.SetStatusNotConfirm(request.orderId, request.statusRollBack);
                }
                return Ok(flag);
            }
            return Ok(result);
        }
        [HttpPost("ConfirmSuccessOrder")]
        public async Task<IActionResult> ConfirmSuccessOrder(StatusOrderRequest request)
        {
            var data = await _manageOrderService.confirmSuccessOrder(request);
            return Ok(data);
        }
        [HttpPost("SearchProduct")]
        public async Task<IActionResult> SearchProduct(SearchOrderRequest request)
        {
            var data = await _manageOrderService.SearchOrder(request);
            return Ok(data);
        }
    }
}
