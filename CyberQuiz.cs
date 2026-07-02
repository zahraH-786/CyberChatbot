using System.Collections.Generic;

namespace CyberChatbotGUI_st10448877_POE
{
    public static class CyberQuiz
    {
        public static List<QuizQuestion> GetQuestions()
        {
            return new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    //quiz questions
                    QuestionText = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "A) Reply with your password", "B) Delete the email", "C) Report the email as phishing", "D) Ignore it" },
                    CorrectAnswer = "C",
                    Feedback = "Reporting phishing emails helps prevent scams."
                },
                new QuizQuestion
                {
                    QuestionText = "True or False: You should use the same password for multiple accounts.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = "False",
                    Feedback = "Using the same password puts all accounts at risk if one is compromised."
                },
                new QuizQuestion
                {
                    QuestionText = "Which is the strongest password?",
                    Options = new List<string> { "A) 123456", "B) Password1", "C) !Q2w#E4r", "D) qwerty" },
                    CorrectAnswer = "C",
                    Feedback = "Strong passwords use a mix of characters and are hard to guess."
                },
                new QuizQuestion
                {
                    QuestionText = "What is phishing?",
                    Options = new List<string> { "A) A hacking technique", "B) An attempt to trick you into giving personal info", "C) A type of firewall", "D) A secure email protocol" },
                    CorrectAnswer = "B",
                    Feedback = "Phishing tricks users into revealing sensitive information."
                },
                new QuizQuestion
                {
                    QuestionText = "True or False: HTTPS is more secure than HTTP.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = "True",
                    Feedback = "HTTPS encrypts the data sent between your browser and the website."
                },
                new QuizQuestion
                {
                    QuestionText = "What is two-factor authentication (2FA)?",
                    Options = new List<string> { "A) Using two passwords", "B) A second layer of security", "C) A type of virus", "D) A backup email" },
                    CorrectAnswer = "B",
                    Feedback = "2FA requires an extra verification step like a code or app confirmation."
                },
                new QuizQuestion
                {
                    QuestionText = "What should you do before clicking on a link in an email?",
                    Options = new List<string> { "A) Click immediately", "B) Hover to preview the URL", "C) Reply to the sender", "D) Forward to friends" },
                    CorrectAnswer = "B",
                    Feedback = "Hovering helps you verify if the link is legitimate or suspicious."
                },
                new QuizQuestion
                {
                    QuestionText = "Which of the following is a social engineering tactic?",
                    Options = new List<string> { "A) Phishing", "B) Encryption", "C) VPN", "D) Firewall" },
                    CorrectAnswer = "A",
                    Feedback = "Phishing is a common form of social engineering attack."
                },
                new QuizQuestion
                {
                    QuestionText = "Which device security step is essential?",
                    Options = new List<string> { "A) Never update software", "B) Avoid using passwords", "C) Install antivirus", "D) Use public Wi-Fi without protection" },
                    CorrectAnswer = "C",
                    Feedback = "Installing antivirus helps detect and prevent malware threats."
                },
                new QuizQuestion
                {
                    QuestionText = "True or False: Public Wi-Fi is always safe to use for banking.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = "False",
                    Feedback = "Public Wi-Fi is often unsecured. Use a VPN for sensitive actions."
                }
            };
        }
    }
}
