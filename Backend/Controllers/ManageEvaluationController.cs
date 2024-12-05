using e_commerce_website.Helper.Evaluation;
using e_commerce_website.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace e_commerce_website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageEvaluationController : ControllerBase
    {
        private readonly IManageEvaluationService _manageEvaluation;
        public ManageEvaluationController(IManageEvaluationService manageEvaluation)
        {
            _manageEvaluation = manageEvaluation;
        }
        //// GET: api/<ManageEvaluationController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<ManageEvaluationController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ManageEvaluationController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ManageEvaluationController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ManageEvaluationController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        [HttpGet("GetEvaluationsDecline")]
        public async Task<IActionResult> GetEvaluationsDecline()
        {
            var data = await _manageEvaluation.GetEvaluationsDecline();
            return Ok(data);
        }
        [HttpPost("ConfirmEvaluation")]
        public async Task<IActionResult> ConfirmEvaluation(EvaluationChangeStatus request)
        {
            var check = await _manageEvaluation.ChangeStatusEvaluation(request);
            return Ok(check);
        }
        [HttpPost("DeleteEvaluation")]
        public async Task<IActionResult> DeleteEvaluation(EvaluationChangeStatus request)
        {
            var check = await _manageEvaluation.ChangeStatusEvaluation(request);
            return Ok(check);
        }
    }
}
