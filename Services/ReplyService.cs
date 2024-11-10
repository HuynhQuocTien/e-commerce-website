using e_commerce_website.Data;
using e_commerce_website.Enums;
using e_commerce_website.Helper.Reply;
using e_commerce_website.Models;
using e_commerce_website.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;

namespace e_commerce_website.Services
{
    public class ReplyService
    {
        private readonly ShopDbContext _context;
        public ReplyService(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(ReplyCreateRequest request)
        {
            var reply = new Reply()
            {
                content = request.content,
                evaluationId = request.evaluationId,
                status = ActionStatus.Display,
                createDate = DateTime.Now,
                userId = request.userId,
            };
            _context.replies.Add(reply);
            await _context.SaveChangesAsync();
            return reply.id;
        }
        public async Task<ReplyViewModel> getReplyById(int replyId)
        {
            var reply = await _context.replies.Where(e => e.status == ActionStatus.Display).Include(u => u.user)
                .Select(rs => new ReplyViewModel()
                {
                    id = rs.id,
                    content = rs.content,
                    evaluationId = rs.evaluationId,

                    createDate = rs.createDate,
                    status = rs.status,
                    userId = rs.userId,
                    user = rs.user,
                })
                .FirstOrDefaultAsync(x => x.id == replyId);

            return reply;
        }
    }
}
