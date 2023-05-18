namespace WEBAPP.Models
{
    public class RequestMessage
    {
        public string role { get; set; }
        public string content { get; set; }
    }
    public class RequestRoot
    {
        public RequestRoot()
        {
            this.model = "gpt-3.5-turbo";
            this.stream = false;
            this.temperature = 0.8;
            this.messages = new List<RequestMessage>();
            this.presence_penalty = 0.8;
            this.frequency_penalty = 0.8;
        }
        public string model { get; set; }
        public bool stream { get; set; }
        public double temperature { get; set; }
        public double presence_penalty { get; set; }
        public double frequency_penalty { get; set; }
        public List<RequestMessage> messages { get; set; } 
    }
}
