using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionsAnswersApp.Data;
using QuestionsAnswersApp.Models;

namespace QuestionsAnswersApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnswersController : ControllerBase
{
    private readonly InterviewDBContext _DBContext;

    public AnswersController(InterviewDBContext dbContext)
    {
        this._DBContext = dbContext;
    }

    [HttpGet("{id}")]
    public Answer? Get(int id)
    {
        Answer answer = _DBContext.Answers.FirstOrDefault(s => s.AnswerId == id);

        //retrieving question details along with answer
        answer.Question = _DBContext.Questions.FirstOrDefault(s => s.QuestionId == answer.QuestionId);

        return answer;
    }

    [HttpPost]
    public async Task<ActionResult<Answer>> AnswerQuestion(Answer answer)
    {
        var question = _DBContext.Questions.FirstOrDefault(s => s.QuestionId == answer.QuestionId);
        if (question != null)
        {
            await _DBContext.Answers.AddAsync(answer);
            await _DBContext.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = answer.AnswerId }, answer);    
        }
        else return StatusCode(StatusCodes.Status500InternalServerError, $"Question with id={answer.QuestionId} could not be found.");
;
    }

    /// <summary>Increment Answers.UpVotes or Answers.DownVotes accordingly</summary>
    /// <returns>The updated Answers row</returns>
    [HttpPut]
    public async Task<ActionResult<Answer>> Put(int id, bool upVote)
    {
        Answer answer = _DBContext.Answers.FirstOrDefault(s => s.AnswerId == id);

        if(upVote)
        {
            answer.UpVotes++;
        }
        else
        {
            answer.DownVotes++;
        }

        _DBContext.Entry(answer).State = EntityState.Modified;
        await _DBContext.SaveChangesAsync();

        return CreatedAtAction("Get", new { id = answer.QuestionId }, answer);    
    }


}
