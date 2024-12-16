using System;
using System.Collections.Generic;

namespace OnlineQuizSystemApi.Models;

public partial class Question
{
    public int Id { get; set; }

    public int QuizId { get; set; }

    public string QuestionText { get; set; } = null!;

    public int Points { get; set; }

    public virtual ICollection<AnswerSubmission> AnswerSubmissions { get; set; } = new List<AnswerSubmission>();

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Quiz Quiz { get; set; } = null!;
}
