using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT.Models
{
    public class ChoiceStatic
    {
        public MessageStatic message { get; set; }
        public string finish_reason { get; set; }
        public int index { get; set; }
    }
    public class MessageStatic
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    public class RootStatic
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public UsageStatic usage { get; set; }
        public List<ChoiceStatic> choices { get; set; }
    }

    public class UsageStatic
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }
}
