using e_commerce_website.Data;
using e_commerce_website.Enums;
using e_commerce_website.Helper.Evaluation;
using e_commerce_website.Services.Interfaces;
using e_commerce_website.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_website.Services
{
    public class ManageEvaluationService : IManageEvaluationService
    {
        private readonly ShopDbContext _context;
        public ManageEvaluationService(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangeStatusEvaluation(EvaluationChangeStatus request)
        {
            var evaluation = await _context.evaluations.Where(x => x.id == request.id).FirstOrDefaultAsync();
            evaluation.status = request.status;
            _context.Entry(evaluation).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<EvaluationViewModel>> GetEvaluationsDecline()
        {
            return await _context.evaluations.Where(x => x.status == EvaluationStatus.Decline)
                .Include(y => y.user)
                .Select(rs => new EvaluationViewModel
                {
                    id = rs.id,
                    title = rs.title,
                    content = rs.content,
                    Replies = rs.Replies,
                    createDate = rs.createDate,
                    productId = rs.productId,
                    product = rs.product,
                    rating = rs.rating,
                    status = rs.status,
                    userId = rs.userId,
                    user = rs.user,
                }).ToListAsync();
        }
    }
}
