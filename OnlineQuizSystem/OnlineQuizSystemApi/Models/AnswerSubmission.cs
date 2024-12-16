using System;
using System.Collections.Generic;

namespace OnlineQuizSystemApi.Models;

public partial class AnswerSubmission
{
    public int Id { get; set; }

    public int QuizAttemptId { get; set; }

    public int QuestionId { get; set; }

    public int? SelectedAnswerId { get; set; }

    public bool? IsCorrect { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual QuizAttempt QuizAttempt { get; set; } = null!;

    public virtual Answer? SelectedAnswer { get; set; }
}
