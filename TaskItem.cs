using System;
namespace CyberChatbotGUI_st10448877_POE
{

    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
     
        public string Reminder { get; set; }
        public bool Completed { get; set; }

       
    }
}
