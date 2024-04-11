using TaskManager.Common.Models;

namespace TaskManager.Api.Models
{
    public class CommonObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public CommonObject()
        {
               CreateDate = DateTime.Now;
        }
        public CommonObject(CommonModel model)
        {
            Name = model.Name;
            Description = model.Description;
            CreateDate = model.CreateDate;
        }
    }
}
