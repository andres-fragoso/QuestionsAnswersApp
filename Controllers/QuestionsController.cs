using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionsAnswersApp.Data;
using QuestionsAnswersApp.Models;

namespace QuestionsAnswersApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly InterviewDBContext _DBContext;

    public QuestionsController(InterviewDBContext dbContext)
    {
        this._DBContext = dbContext;
    }

    /// <summary>Retrieves all Questions records</summary>
    /// <returns>All Questions models</returns>
    [HttpGet]
    public IEnumerable<Question> Get()
    {
        var result = this._DBContext.Questions;

        return result;
    }

    /// <summary>Retrieves Question rows by Id</summary>
    /// <returns>Question model by Id</returns>
    [HttpGet("{id}")]
    public Question? Get(int id)
    {
        Question question = _DBContext.Questions.FirstOrDefault(s => s.QuestionId == id);

        //retrieving answers as well
        _DBContext.Entry(question).Collection(a => a.Answers).Query().Where(q => q.QuestionId == id).ToList();

        return question;
    }

    /// <summary>Retrieves all Questions records that contain the specified tag</summary>
    /// <returns>All Questions models that contain the specified tag</returns>
    [HttpGet("GetByTag/{tag}")]
    public IEnumerable<Question>? Get(string tag)
    {
        List<Question> question = _DBContext.Questions.Where(s => s.QuestionTags.Contains(tag)).ToList();

        return question;
    }

    /// <summary>Create a new Question record</summary>
    /// <returns>The created Question row</returns>
    [HttpPost]
    public async Task<ActionResult<Question>> Post(Question question)
    {
        await _DBContext.Questions.AddAsync(question);
        await _DBContext.SaveChangesAsync();

        return CreatedAtAction("Get", new { id = question.QuestionId }, question);    
    }

    /// <summary>Increment Questions.UpVotes or Questions.DownVotes accordingly</summary>
    /// <returns>The updated Questions row</returns>
    [HttpPut]
    public async Task<ActionResult<Question>> Put(int id, bool upVote)
    {
        Question question = _DBContext.Questions.FirstOrDefault(s => s.QuestionId == id);

        if(upVote)
        {
            question.UpVotes++;
        }
        else
        {
            question.DownVotes++;
        }

        _DBContext.Entry(question).State = EntityState.Modified;
        await _DBContext.SaveChangesAsync();

        return CreatedAtAction("Get", new { id = question.QuestionId }, question);    
    }

}
