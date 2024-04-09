namespace TaskManager.Api.Models
{
    public class CommonObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public byte[] Photo { get; set; }
        public CommonObject()
        {
               CreateDate = DateTime.Now;
        }
    }
}
