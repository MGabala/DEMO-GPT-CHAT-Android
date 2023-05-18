namespace WEBAPP.Entities
{
    public class Choice
    {
        public Delta delta { get; set; }
        public int index { get; set; }
        public object finish_reason { get; set; }
    }
    public class Delta
    {
        public string role { get; set; }
        public string content { get;set; }
    }
    public class Root
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public List<Choice> choices { get; set; }
    }
}
