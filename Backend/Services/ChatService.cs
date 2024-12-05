using Microsoft.AspNetCore.Identity;
using e_commerce_website.Data;
using e_commerce_website.Helper.Chat;
using e_commerce_website.Models;
using e_commerce_website.Services.Interfaces;
using e_commerce_website.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_website.Services
{
    public class ChatService : IChatService
    {
        private readonly ShopDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private Guid adminId = new Guid("4557893f-1f56-4b6f-bb3b-caefd62c8c49");

        public ChatService(ShopDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<int> CreateMessage(ChatCreateRequest request)
        {
            var checkSender = await _context.Users.FirstAsync(x => x.Id == request.senderId);
            var checkRoleSender = await _userManager.GetRolesAsync(checkSender);
            var chat = new Chat();
            if (checkRoleSender[0] == "User")
            {

                chat = new Chat()
                {
                    createDate = DateTime.Now,
                    content = request.content,
                    senderId = request.senderId,
                    receiverId = adminId,
                };

            }
            else
            {
                chat = new Chat()
                {
                    createDate = DateTime.Now,
                    content = request.content,
                    senderId = request.senderId,
                    //receiverId = request.receiverId,
                    receiverId = request.receiverId.Value,
                };
            }
            _context.chats.Add(chat);
            await _context.SaveChangesAsync();
            return chat.id;



        }

        public async Task<ChatViewModel> GetMessageById(int chatId)
        {
            var data = await _context.chats.Where(x => x.id == chatId).Select(y => new ChatViewModel
            {
                id = y.id,
                content = y.content,
                createDate = y.createDate,
                senderId = y.senderId,
                sender = y.sender,
                receiverId = y.receiverId,
                receiver = y.receiver,
            }).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<ChatViewModel>> GetMessages(ChatGetMessageRequest request)
        {
            var checkSender = await _context.Users.FirstAsync(x => x.Id == request.senderId);
            var checkRoleSender = await _userManager.GetRolesAsync(checkSender);
            var chat = new Chat();
            var data = _context.chats.AsQueryable();
            if (checkRoleSender[0] == "User")
            {
                data = data.Where(x => (x.senderId == request.senderId && x.receiverId == adminId) ||
                (x.senderId == adminId && x.receiverId == request.senderId));
            }
            if (checkRoleSender[0] == "Admin")
            {
                if (request.receiverId.HasValue)
                {
                    data = data.Where(x => (x.senderId == request.senderId && x.receiverId == request.receiverId.Value) ||
                    (x.senderId == request.receiverId.Value && x.receiverId == request.senderId));
                }
                else
                {
                    return new List<ChatViewModel>();
                }
            }
            return await data.Select(y => new ChatViewModel
            {
                id = y.id,
                content = y.content,
                createDate = y.createDate,
                senderId = y.senderId,
                sender = y.sender,
                receiverId = y.receiverId,
                receiver = y.receiver,
            }).ToListAsync();

        }
        /*public async Task<List<ChatViewModel>> GetMessages(ChatGetMessageRequest request)
        {
            // Fetch sender and verify existence
            var checkSender = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.senderId);
            if (checkSender == null)
            {
                // Handle case when sender is not found
                return new List<ChatViewModel>(); // or throw an exception if preferred
            }

            // Retrieve the sender's roles
            var checkRoleSender = await _userManager.GetRolesAsync(checkSender);
            if (checkRoleSender.Count == 0)
            {
                // Handle case when no role is assigned
                return new List<ChatViewModel>();
            }

            var chat = new Chat();
            var data = _context.chats.AsQueryable();

            // Determine query based on the sender's role
            if (checkRoleSender[0] == "User")
            {
                data = data.Where(x => (x.senderId == request.senderId && x.receiverId == adminId) ||
                                       (x.senderId == adminId && x.receiverId == request.senderId));
            }
            else if (checkRoleSender[0] == "Admin")
            {
                if (request.receiverId.HasValue)
                {
                    data = data.Where(x => (x.senderId == request.senderId && x.receiverId == request.receiverId.Value) ||
                                           (x.senderId == request.receiverId.Value && x.receiverId == request.senderId));
                }
                else
                {
                    // Return an empty list if no receiver ID is provided for Admin role
                    return new List<ChatViewModel>();
                }
            }

            // Return the messages as ChatViewModel list
            return await data.Select(y => new ChatViewModel
            {
                id = y.id,
                content = y.content,
                createDate = y.createDate,
                senderId = y.senderId,
                sender = y.sender,
                receiverId = y.receiverId,
                receiver = y.receiver,
            }).ToListAsync();
        }*/


        public async Task<List<UserViewModel>> GetUserOnlines(List<Guid> userIds)
        {
            var data = await _context.Users.Where(x => userIds.Any(value => value == x.Id)).Select(y => new UserViewModel
            {
                id = y.Id,
                displayname = y.displayname,
                avatar = y.avatar
            }).ToListAsync();
            return data;
        }
    }
}
