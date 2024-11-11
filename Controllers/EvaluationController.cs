using e_commerce_website.Helper.Evaluation;
using e_commerce_website.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace e_commerce_website.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;
        public EvaluationController(IEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }
        [HttpGet("getEvaluationById")]
        public async Task<IActionResult> getEvaluationById(int evaluationId)
        {
            var evaluation = await _evaluationService.getEvaluationById(evaluationId);
            return Ok(evaluation);
        }
        [HttpGet("evaluations-by-productId/{productId}")]
        public async Task<IActionResult> getEvaluationsByProductId(int productId)
        {
            var evaluations = await _evaluationService.getEvaluationsByProductId(productId);
            return Ok(evaluations);
        }
        [HttpPost("create")]
        [HttpPost]
        public async Task<IActionResult> create([FromBody] EvaluationCreateRequest request)
        {

            var evaluationId = await _evaluationService.Create(request);
            if (evaluationId == 0)
            {
                return BadRequest("Thêm đánh giá không thành công!");
            }
            var evaluation = await _evaluationService.getEvaluationById(evaluationId);
            return CreatedAtAction(nameof(getEvaluationById), new { id = evaluationId }, evaluation);
        }
    }
}
