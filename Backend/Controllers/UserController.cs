﻿using e_commerce_website.Helper.Facebook;
using e_commerce_website.Helper.Github;
using e_commerce_website.Helper.Google;
using e_commerce_website.Helper.User;
using e_commerce_website.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultToken = await _userService.Authenticate(request);
            if (string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("username hoặc password sai!");
            }
            return Ok(new { token = resultToken });
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Register(request);
            if (!result)
            {
                return BadRequest("Email này đã tồn tại!");
            }
            return Ok("Đăng ký tài khoản thành công!");
        }
        [HttpGet("get-user-by-id/{userId}")]
        public async Task<IActionResult> getUserById(Guid userId)
        {
            var user = await _userService.getUserById(userId);
            return Ok(user);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UserUpdateRequest request)
        {
            var userId = await _userService.Update(request);
            if (userId == null)
            {
                return BadRequest();

            }
            var user = await _userService.getUserById(userId);
            return Ok(new { message = "Cập nhập user thành công!", user = user });
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgotPasswordRequest request)
        {
            var check = await _userService.ForgotPassword(request);
            return Ok(check);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {

            var check = await _userService.ResetPassword(request);
            return Ok(check);
        }
        [HttpPost("LoginWithFacebook")]
        public async Task<IActionResult> LoginWithFacebook(FacebookLoginRequest request)
        {
            var token = await _userService.LoginWithFacebook(request);
            if (token == "FAILED")
            {
                return BadRequest("LOGIN FACEBOOK FAILED");
            }
            HttpContext.Response.Headers.Add("Token", $"{token}");
            return Ok(token);
        }
        [HttpPost("LoginWithGoogle")]
        public async Task<IActionResult> LoginWithGoogle(GoogleLoginRequest request)
        {
            var token = await _userService.LoginWithGoogle(request);
            if (token == "FAILED")
            {
                return BadRequest("LOGIN GOOGLE FAILED");
            }
            HttpContext.Response.Headers.Add("Token", $"{token}");
            return Ok(token);
        }
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok();
        }
    }
    
}
