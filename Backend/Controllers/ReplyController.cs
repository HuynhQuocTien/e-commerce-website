﻿using e_commerce_website.Helper.Reply;
using e_commerce_website.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyController : ControllerBase
    {
        private readonly IReplyService _replyService;
        public ReplyController(IReplyService replyService)
        {
            _replyService = replyService;
        }
        [HttpGet("getReplyById")]
        public async Task<IActionResult> getReplyById(int replyId)
        {
            var reply = await _replyService.getReplyById(replyId);
            return Ok(reply);
        }
        [HttpPost]
        public async Task<IActionResult> create([FromBody] ReplyCreateRequest request)
        {

            var replyId = await _replyService.Create(request);
            if (replyId == 0)
            {
                return BadRequest("Thêm phản hồi không thành công!");
            }
            var reply = await _replyService.getReplyById(replyId);
            return CreatedAtAction(nameof(getReplyById), new { id = replyId }, reply);
        }
    }
}
