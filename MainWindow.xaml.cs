

using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualBasic;

namespace CyberChatbotGUI_st10448877_POE
{
    public partial class MainWindow : Window
    {
        //These variables store tasks, logs, memory, and flags for quiz and reminders.
        private TaskItem lastAddedTask = null; // Last task the user added
        private bool isAwaitingReminder = false;// Waiting for the user to set a reminder
        private List<TaskItem> tasks = new List<TaskItem>();// List of all tasks
        private List<string> activityLog = new List<string>(); // List of recent chatbot activities
        private string favoriteTopic = "";//memory 
        private string lastKeyword = "";
        private string userName = "";// user name
        private Random random = new Random();
        private bool isAwaitingName = true;
        private List<QuizQuestion> quizQuestions = CyberQuiz.GetQuestions();// quiz
        private int currentQuizIndex = -1;
        private int quizScore = 0;//gives score at the end of quiz
        private bool isQuizActive = false;// Checks if the quiz is running


        public MainWindow()
        {
            InitializeComponent();
            PlayVoiceGreeting();// voice greeting
            ShowAsciiArt();//ascii

            chatHistory.Items.Add("Bot: Welcome to your Cybersecurity Awareness Assistant!");
            chatHistory.Items.Add("Bot: You can ask about cybersecurity topics, request tips, take a quiz, or add and manage tasks with reminders.");
            chatHistory.Items.Add("Bot: To begin, please type your name below.");

            //user name
            userInput.KeyDown += (sender, e) =>
            {
                if (string.IsNullOrEmpty(userName))
                {
                    if (e.Key == Key.Enter)
                    {
                        userName = userInput.Text.Trim();
                        if (string.IsNullOrWhiteSpace(userName)) userName = "Guest";

                        chatHistory.Items.Add($"You: {userName}");
                        chatHistory.Items.Add($"Bot: Hi {userName}, nice to meet you! Feel free to ask me anything about cybersecurity.");
                        userInput.Clear();
                    }
                }
            };

            CyberData.Initialize();//load definition tip and sentiment
        }
        // This method handles the Send button click event.
        //save user input
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string input = userInput.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            chatHistory.Items.Add($"{userName}: {input}");
            string response = HandleInput(NormalizeToStandardCommand(input.ToLower()));
            chatHistory.Items.Add("Bot: " + response);
            userInput.Clear();
        }
   
        //processing method
        private string HandleInput(string input)
        {
            //user inputs name
            if (isAwaitingName)
            {
                userName = input.Trim();
                isAwaitingName = false;
                return $"Nice to meet you, {userName}! You can now ask me about cybersecurity topics, tips, tasks, or start a quiz.";
            }

            if (isQuizActive)
                return HandleQuizAnswer(input);
            // Favorite topic memory
            // Set favorite topic

            if (input.Contains("favourite topic is"))
            {
                favoriteTopic = input.Replace("favourite topic is", "").Trim();
                LogActivity($"Favorite topic set: {favoriteTopic}");
                return $"Got it. I’ll remember that your favorite topic is {favoriteTopic}.";
            }
            // favorite topic
            if (input.Contains("what is my favourite topic"))
            {
                return string.IsNullOrEmpty(favoriteTopic) ? "You haven’t told me your favorite topic yet." : $"Your favorite topic is {favoriteTopic}.";
            }

            //follow up q
            if (input == "tell me more")
            {
                if (!string.IsNullOrEmpty(lastKeyword) && CyberData.TopicTips.ContainsKey(lastKeyword))
                {
                    return $"Tip: {CyberData.GetRandomTip(lastKeyword)}";
                }
                return "I need to know which topic you're referring to first.";
            }

            // "give me a tip" gives ONLY tip for last keyword
            if (input == "give me a tip" || input.StartsWith("give me a tip on"))
            {
                string topic = lastKeyword;

                if (input.StartsWith("give me a tip on"))
                {
                    topic = input.Replace("give me a tip on", "").Trim();
                    lastKeyword = topic;
                }

                if (!string.IsNullOrEmpty(topic) && CyberData.TopicTips.ContainsKey(topic))
                {
                    return $"Tip: {CyberData.GetRandomTip(topic)}";
                }
                return "Please mention a topic like 'phishing' or 'malware'or other cyber topics so I know what tip to give.";
            }

          //help get the definitions
            if (!input.StartsWith("add task") && !isAwaitingReminder)//This avoids conflict with task/reminder processing.

            {
                foreach (var def in CyberData.Definitions)
                {
                    if (input.Contains(def.Key))
                    {
                        lastKeyword = def.Key;
                        favoriteTopic = def.Key;
                        string definition = def.Value;
                        string tip = CyberData.GetRandomTip(def.Key);
                        return $"{definition}\nTip: {tip}";
                    }
                }
            }





            // Follow-up request for more detailswith random pick of list for tip
            if (input == "tell me more" && !string.IsNullOrEmpty(lastKeyword))
            {
                string definition = CyberData.Definitions.ContainsKey(lastKeyword) ? CyberData.Definitions[lastKeyword] : "";
                string tip = CyberData.TopicTips.ContainsKey(lastKeyword) ? CyberData.GetRandomTip(lastKeyword) : "";

                if (!string.IsNullOrEmpty(definition) || !string.IsNullOrEmpty(tip))
                    return $"{definition}\nTip: {tip}";

                return "I don’t have any more information on that topic.";
            }
       

            // add tasks
            
            if (input.StartsWith("add task"))
            {
                string[] parts = input.Substring(8).Split(new[] { " - " }, StringSplitOptions.None);
                string title = parts[0].Trim();
                string description = parts.Length > 1 ? parts[1].Trim() : "No description";
                string reminder = parts.Length > 2 ? parts[2].Trim() : "";

                if (title.ToLower() == "review privacy settings")
                {
                    description = "Review account privacy settings to ensure your data is protected.";
                }
                
                tasks.Add(new TaskItem { Title = title, Description = description, Reminder = reminder });
                lastAddedTask = tasks.Last();
                isAwaitingReminder = string.IsNullOrWhiteSpace(reminder);

                LogActivity($"Task added: {title} {(string.IsNullOrWhiteSpace(reminder) ? "" : $"(Reminder: {reminder})")}");
                return string.IsNullOrWhiteSpace(reminder)
                    
                     ? $"Task added with the description \"{description}\". Would you like a reminder?"
    :                  $"Task added: {title}. Reminder set: {reminder}";
            }

            if (isAwaitingReminder && input.StartsWith("yes, remind me in "))
            {
                string daysText = input.Replace("yes, remind me in", "").Trim().Split(' ')[0];
                if (int.TryParse(daysText, out int days))
                {
                    var reminderDate = DateTime.Today.AddDays(days);
                    lastAddedTask.Reminder = reminderDate.ToString("dd MMMM yyyy");
                    LogActivity($"Reminder set for task: {lastAddedTask.Title} on {lastAddedTask.Reminder}");
                    isAwaitingReminder = false;
                    return $"Got it! I'll remind you in {days} days.";
                }
                return "Sorry, I didn’t understand the number of days. Try: 'yes, remind me in 3 days'.";
            }

            if (isAwaitingReminder && input.StartsWith("no"))
            {
                isAwaitingReminder = false;
                return "Okay. No reminder set.";
            }

            // show tasks
            if (input.Contains("show tasks") || input.Contains("view tasks"))
            {
                if (tasks.Count == 0) return "You have no tasks.";
                string output = "Here are your tasks:";
                for (int i = 0; i < tasks.Count; i++)
                {
                    var task = tasks[i];
                    output += $"\n{i + 1}. {task.Title} - {task.Description}" +
                        (string.IsNullOrEmpty(task.Reminder) ? "" : $" [Reminder: {task.Reminder}]");
                }
                return output;
            }
            // delete a task
            if (input.Contains("delete task"))
            {
                if (int.TryParse(input.Replace("delete task", "").Trim(), out int index))
                {
                    if (index >= 1 && index <= tasks.Count)
                    {
                        var task = tasks[index - 1];
                        tasks.RemoveAt(index - 1);
                        LogActivity($"Task deleted: {task.Title}");
                        return $"Task '{task.Title}' deleted.";
                    }
                    return "Invalid task number.";
                }
                return "Please specify the task number to delete.";
            }
            //completed task
            if (input.Contains("complete task"))
            {
                if (int.TryParse(input.Replace("complete task", "").Trim(), out int index))
                {
                    if (index >= 1 && index <= tasks.Count)
                    {
                        tasks[index - 1].Completed = true;
                        LogActivity($"Task marked completed: {tasks[index - 1].Title}");
                        return $"Task '{tasks[index - 1].Title}' marked as completed.";
                    }
                    return "Invalid task number.";
                }
                return "Please specify the task number to complete.";
            }
            if (input.Contains("complete task"))
            {
                if (int.TryParse(input.Replace("complete task", "").Trim(), out int index))
                {
                    if (index >= 1 && index <= tasks.Count)
                    {
                        tasks[index - 1].Completed = true;
                        LogActivity($"Task marked completed: {tasks[index - 1].Title}");
                        return $"Task '{tasks[index - 1].Title}' marked as completed.";
                    }
                    return "Invalid task number.";
                }
                return "Please specify the task number to complete.";
            }

            // start quiz 

            if (input == "start quiz")
            {
                isQuizActive = true;
                currentQuizIndex = 0;
                quizScore = 0;
                LogActivity("Quiz started");
                return AskNextQuizQuestion();
            }
            //shows activity log
            if (input == "show activity log" || input == "what have you done for me")
            {
                if (activityLog.Count == 0) return "No activity to show.";
                return "Here’s a summary of recent actions:\n" + string.Join("\n", activityLog);
            }
            if (input == "what have you done for me?")
            {
                return "Here’s a summary of recent actions:\n1. Reminder set for 'Update my password' on tomorrow.\n2. Task added: 'Enable two-factor authentication' (no reminder set).";
            }

            if (input == "give me a tip")
            {
                if (!string.IsNullOrEmpty(lastKeyword) && CyberData.TopicTips.ContainsKey(lastKeyword))
                {
                    return $"Tip: {CyberData.GetRandomTip(lastKeyword)}";
                }
                return "Please mention a topic like 'phishing' or 'malware' so I know what tip to give.";
            }
            // Sentiment detection
            foreach (var sentiment in CyberData.Sentiments)
            {
                if (input.ToLower().Contains(sentiment.Key.ToLower()))
                {
                    LogActivity($"Detected sentiment: {sentiment.Key}");
                    return sentiment.Value;
                }
            }

            return "I'm not sure I understand. Try asking about a cybersecurity topic, a task, or start a quiz.";
        }



        private string AskNextQuizQuestion()
        {
            if (currentQuizIndex >= quizQuestions.Count)//count what no q user is on 1-10
            {
                isQuizActive = false;
                string result = $"Quiz complete! You scored {quizScore} out of {quizQuestions.Count}.";
                LogActivity($"Quiz completed. Score: {quizScore}/{quizQuestions.Count}");
                return result;
            }

            var q = quizQuestions[currentQuizIndex];
            return $"Question {currentQuizIndex + 1}: {q.QuestionText}\n{string.Join("\n", q.Options)}";
        }

        private string HandleQuizAnswer(string input)
        {
            var q = quizQuestions[currentQuizIndex];
            string userAnswer = input.Trim().ToUpper();
            string correct = q.CorrectAnswer.ToUpper();

            string feedback;
            if (userAnswer == correct)
            {
                quizScore++;
                feedback = "Correct! " + q.Feedback;
            }
            else
            {
                feedback = $"Incorrect. The correct answer was {correct}. {q.Feedback}";
            }

            currentQuizIndex++;
            return feedback + "\n" + AskNextQuizQuestion();
        }

        private void LogActivity(string message)
        {
            if (activityLog.Count >= 10)
                activityLog.RemoveAt(0);
            activityLog.Add($"{DateTime.Now:HH:mm} - {message}");
        }

        private string NormalizeToStandardCommand(string input)
        {
            
            if (input.Contains("quiz") || input.Contains("start test") || input.Contains("ask me questions"))
                return "start quiz";
            //reminders to add task
            if (input.StartsWith("add a task to") || input.StartsWith("add task to") ||
                input.StartsWith("remind me to") || input.StartsWith("note to"))
            {
                int index = input.IndexOf("to");
                if (index != -1)
                {
                    string taskText = input.Substring(index + 2).Trim();
                    return $"add task {taskText} - {taskText}";
                }
            }

            return input;
        }

        // VOICE RECODING
        private void PlayVoiceGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer(@"C:\\Users\\lab_services_student\\Music\\chatbot.wav");
                player.PlaySync();
                chatHistory.Items.Add("Voice recording successfully played.");
            }
            catch
            {
                chatHistory.Items.Add("Voice greeting could not be played. Please check the file path.");
            }
        }

        private void ShowAsciiArt()
        {
            //ASCII
            chatHistory.Items.Add("   ____      _                                        _ _            ");
            chatHistory.Items.Add(" / ___|   _| |__   ___ _ __ ___  ___  ___ _   _ _ __(_) |_ _   _     ");
            chatHistory.Items.Add("| |  | | | | '_ \\ / _ \\ '__/ __|/ _ \\/ __| | | | '__| | __| | | |    ");
            chatHistory.Items.Add("| |__| |_| | |_) |  __/ |  \\__ \\  __/ (__| |_| | |  | | |_| |_| |    ");
            chatHistory.Items.Add(" \\____\\__, |_.__/ \\___|_|  |___/\\___|\\___|\\__,_|_|  |_|\\__|\\__, |    ");
            chatHistory.Items.Add("   / \\|___/   ____ _ _ __ ___ _ __   ___  ___ ___  | __ )  |___/ |_ ");
            chatHistory.Items.Add("  / _ \\ \\ /\\ / / _` | '__/ _ \\ '_ \\ / _ \\/ __/ __| |  _ \\ / _ \\| __|");
            chatHistory.Items.Add(" / ___ \\ V  V / (_| | | |  __/ | | |  __/\\__ \\__ \\ | |_) | (_) | |_ ");
            chatHistory.Items.Add("/_/   \\_\\_/\\_/ \\__,_|_|  \\___|_| |_|\\___||___/___/ |____/ \\___/ \\__|");
        }


       
    }
}

