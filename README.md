
# Online Classroom Quiz System

## Overview
The Online Classroom Quiz System is a web application that allows professors to create and manage quizzes, and students to attempt quizzes and receive grades. The system is designed to simplify quiz management for professors and provide an interactive platform for students.

## Features
- Professors can:
  - Create and publish quizzes.
  - Add questions and multiple-choice answers to quizzes.
  - Mark correct answers for each question.
- Students can:
  - Attempt quizzes within a specified time limit.
  - View their scores after completing a quiz.
- The system ensures data consistency with relationships defined in the database schema.

## Database Schema
The database consists of the following tables:
1. **Professor**
   - Stores information about professors.
   - Fields: `Id`, `FirstName`, `LastName`, `Email`.

2. **Quiz**
   - Represents a quiz created by a professor.
   - Fields: `Id`, `ProfessorId`, `Title`, `Description`, `DateCreated`, `TimeLimit`, `IsPublished`.

3. **Question**
   - Contains the questions of a quiz.
   - Fields: `Id`, `QuizId`, `QuestionText`, `Points`.

4. **Answer**
   - Stores the possible answers for each question.
   - Fields: `Id`, `QuestionId`, `AnswerText`, `IsCorrect`.

### Entity-Relationship Diagram (ERD)
The ER diagram depicts the relationships between the entities:
- A **Professor** can create multiple **Quizzes**.
- A **Quiz** contains multiple **Questions**.
- A **Question** can have multiple **Answers**.

## Prerequisites
- **SQL Server Management Studio (SSMS)**: To run the SQL scripts.
- **Git**: To clone the project repository if required.

## Installation
1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd online-quiz-system
