using e_commerce_website.Helper.Evaluation;
using e_commerce_website.ViewModel;

namespace e_commerce_website.Services.Interfaces
{
    public interface IManageEvaluationService
    {
        Task<List<EvaluationViewModel>> GetEvaluationsDecline();
        Task<bool> ChangeStatusEvaluation(EvaluationChangeStatus requesst);
    }
}
