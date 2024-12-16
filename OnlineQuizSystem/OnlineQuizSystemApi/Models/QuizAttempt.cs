using System;
using System.Collections.Generic;

namespace OnlineQuizSystemApi.Models;

public partial class QuizAttempt
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int QuizId { get; set; }

    public DateTime? AttemptDate { get; set; }

    public int? Score { get; set; }

    public virtual ICollection<AnswerSubmission> AnswerSubmissions { get; set; } = new List<AnswerSubmission>();

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
