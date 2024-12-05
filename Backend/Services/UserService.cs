using e_commerce_website.Data;
using e_commerce_website.Enums;
using e_commerce_website.Helper;
using e_commerce_website.Helper.Facebook;
using e_commerce_website.Helper.User;
using e_commerce_website.Models;
using e_commerce_website.Services.Interfaces;
using e_commerce_website.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using MimeKit;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace e_commerce_website.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly ShopDbContext _context;
        private readonly IStorageService _storageService;
        private readonly EmailConfiguration _emailConfiguration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IConfiguration config,
            ShopDbContext context, IStorageService storageService,
            EmailConfiguration emailConfiguration, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _context = context;
            _storageService = storageService;
            _emailConfiguration = emailConfiguration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.username);
            if (user == null || user.status == ActionStatus.Deleted)
            {
                return null;
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.password, true, true);
            if(!result.Succeeded)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("fullname", user.displayname),
                new Claim("role", roles[0]),
                new Claim("avatar", user.avatar!=null? user.avatar:"")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var testUserExist = _context.Users.FirstOrDefault(u => u.Email == request.email);
            if (testUserExist == null)
            {
                var user = new AppUser()
                {
                    displayname = request.displayname,
                    Email = request.email,
                    UserName = request.email,
                    birthDay = DateTime.Now,
                };
                var result = await _userManager.CreateAsync(user, request.password);
                if (result.Succeeded)
                {
                    var userRole = _context.Roles.FirstOrDefault(x => x.Name == "User");
                    await _userManager.AddToRoleAsync(user, userRole.Name);
                    return true;
                }
            }
            return false;
        }

        public async Task<UserViewModel> getUserById(Guid userId)
        {
            var user = await _context.Users.Where(i => i.status == ActionStatus.Display)
                .Select(ele => new UserViewModel()
                {
                    id = ele.Id,
                    avatar = ele.avatar,
                    birthDay = ele.birthDay,
                    displayname = ele.displayname,
                    address = ele.address,
                    gender = ele.gender,
                    Orders = ele.Orders,
                    phone = ele.phone,
                    status = ele.status,
                    email = ele.Email,
                })
                .FirstOrDefaultAsync(x => x.id == userId);

            return user;
        }
        public async Task<Guid> Update(UserUpdateRequest request)
        {
            var findUser = await _context.Users.Include(or => or.Orders)
            .FirstOrDefaultAsync(p => p.Id == request.id);
            if (findUser == null)
            {
                // If user not found, throw an exception or return a meaningful result.
                throw new ArgumentException("User not found.");
            }

            // Step 2: Update user details (non-avatar related)
            findUser.displayname = request.displayname;
            findUser.gender = request.gender;
            findUser.phone = request.phone;
            findUser.address = request.address;
            findUser.birthDay = request.birthDay;
            // Handle avatar update
            if (!string.IsNullOrEmpty(request.avatar) && request.file == null)
            {
                // Use the existing avatar
                findUser.avatar = request.avatar;
            }
            else if (request.file != null)
            {
                // Save the new file and update avatar
                findUser.avatar = await this.SaveFile(request.file);
            }
            else if (string.IsNullOrEmpty(request.avatar) && request.file == null)
            {
                // Optional: You can decide whether to throw an error or use a default avatar
                throw new ArgumentException("Both avatar and file are missing. Please provide at least one.");
            }

            // Step 4: Save the changes to the database
            _context.Entry(findUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // if ((findUser.avatar == request.avatar) && request.file == null)
            // {
            //     findUser.displayname = request.displayname;
            //     findUser.gender = request.gender;
            //     findUser.phone = request.phone;
            //     findUser.address = request.address;
            //     findUser.birthDay = request.birthDay;
            //     _context.Entry(findUser).State = EntityState.Modified;
            //     await _context.SaveChangesAsync();
            //     return findUser.Id;
            // }
            // else
            // {
            //
            //     findUser.avatar = await this.SaveFile(request.file);
            //     findUser.displayname = request.displayname;
            //     findUser.gender = request.gender;
            //     findUser.address = request.address;
            //     findUser.phone = request.phone;
            //     findUser.birthDay = request.birthDay;
            //     _context.Entry(findUser).State = EntityState.Modified;
            //     await _context.SaveChangesAsync();
            //     return findUser.Id;
            // }
            return findUser.Id;
        }
        public async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim(); //Check lib
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return $"https://localhost:5001/MyImages/{fileName}";
        }

        public async Task<bool> ForgotPassword(ForgotPasswordRequest request)
        {

            var user = await _context.Users.Where(u => u.Email == request.email && !u.UserName.Contains("facebook")).FirstOrDefaultAsync();
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResetLink = string.Format("https://localhost:3000/ResetPassword?token={0}&email={1}", token, request.email);
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Sender Name",_emailConfiguration.From));
                //var to = new List<MailboxAddress>();

                emailMessage.To.Add(new MailboxAddress("Receiver Name", request.email));
                emailMessage.Subject = "RESET PASSWORD";


                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = string.Format("<a href='https://localhost:3000/ResetPassword?token={0}&email={1}'>Nhấp vào link này để Reset Password</a>",
                        token, request.email)
                };
                return await Send(emailMessage);
            }
            return false;
        }
        private async Task<bool> Send(MimeMessage mailMessage)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                    await client.AuthenticateAsync(_emailConfiguration.Username, _emailConfiguration.Password);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.SendAsync(mailMessage);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }

            }
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _context.Users.Where(u => u.Email == request.email && !u.UserName.Contains("facebook")).FirstOrDefaultAsync();
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, request.token, request.newPassword);
                if (result.Succeeded)
                {
                    return true;
                }
                return false;

            }
            else
            {
                return false;
            }
        }

        public async Task<string> LoginWithFacebook(FacebookLoginRequest request)
        {
            var username = request.userId + "facebook";
            var checkUserExist = await _userManager.FindByNameAsync(username);
            //
            if (checkUserExist != null && checkUserExist.status == ActionStatus.Deleted)
            {
                return "FAILED";
            }
            if (checkUserExist == null)
            {
                var user = new AppUser()
                {
                    displayname = request.name,
                    Email = request.email,
                    avatar = request.avatar,
                    UserName = username,
                    //birthDay = DateTime.Now,
                };
                var result = await _userManager.CreateAsync(user, request.userId);
                if (result.Succeeded)
                {
                    var userRole = _context.Roles.FirstOrDefault(x => x.Name == "User");
                    await _userManager.AddToRoleAsync(user, userRole.Name);
                    var userTemp = await _userManager.FindByNameAsync(username);
                    return await CreateToken(userTemp);
                }
                else
                {
                    return "FAILED";
                }

            }
            else
            {
                return await CreateToken(checkUserExist);
            }
        }
        private async Task<string> CreateToken(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("fullname", user.displayname),
                new Claim("role", roles[0]),
                new Claim("avatar", string.IsNullOrEmpty(user.avatar) ? "" : user.avatar)
            };
            //var key = Base64UrlEncoder.DecodeBytes(_config["Tokens:Key"]);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        private byte[] getBytes(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
        private string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0]; // Remove any trailing '='s
            output = output.Replace('+', '-'); // 62nd char of encoding
            output = output.Replace('/', '_'); // 63rd char of encoding
            return output;
        }
    }
}
