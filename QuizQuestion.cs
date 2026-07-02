using System.Collections.Generic;

namespace CyberChatbotGUI_st10448877_POE
{
    public class QuizQuestion
    {
        //quiz getters and setters
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public string Feedback { get; set; }
    }
}
