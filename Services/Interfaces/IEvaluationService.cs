using e_commerce_website.Helper.Evaluation;
using e_commerce_website.ViewModel;

namespace e_commerce_website.Services.Interfaces
{
    public interface IEvaluationService
    {
        Task<int> Create(EvaluationCreateRequest request);
        Task<EvaluationViewModel> getEvaluationById(int evaluationId);
        Task<List<EvaluationViewModel>> getEvaluationsByProductId(int productId);
    }
}
