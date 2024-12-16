using System;
using System.Collections.Generic;

namespace OnlineQuizSystemApi.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int ProfessorId { get; set; }

    public virtual User Professor { get; set; } = null!;

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
