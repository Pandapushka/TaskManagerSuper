using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Models.Abstractions;
using TaskManager.Api.Models.Data;
using TaskManager.Common.Models;

namespace TaskManager.Api.Models.Services
{
    public class ProjectsService : AbstractionService, ICommonService<ProjectModel>
    {
        private readonly ApplicationContext _db;

        public ProjectsService(ApplicationContext db)
        {
            _db = db;
        }
        public bool Create(ProjectModel model)
        {
            bool result = DoAction(delegate ()
            {
                Project project = new Project(model);
                _db.Projects.Add(project);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                Project project = _db.Projects.FirstOrDefault(x => x.Id == id);
                _db.Projects.Remove(project);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Update(int id, ProjectModel model)
        {
            bool result = DoAction(delegate ()
            {

                Project project = _db.Projects.FirstOrDefault(x => x.Id == id);
                project.Name = model.Name;
                project.Description = model.Description;
                project.Status = model.Status;
                project.AdminId = model.AdminId;
                _db.Projects.Update(project);
                _db.SaveChanges();
            });
            return result;
        }
        public ProjectModel Get(int id)
        {
            Project project = _db.Projects.FirstOrDefault(x => x.Id == id);
            return project?.ToDto();
        }

        public List<ProjectModel> GetByUserId(int id)
        {
            List<ProjectModel> result = new List<ProjectModel>();
            var admin = _db.ProjectAdmins.FirstOrDefault(x => x.UserId == id);
            if (admin == null)
            {
                var projectForAdmin = _db.Projects.Where(x => x.AdminId == id).Select(p => p.ToDto());
                result.AddRange(projectForAdmin);
            }
            var projectsForUser = _db.Projects.Include(p => p.AllUsers).Where(p => p.AllUsers.Any(u => u.Id == id)).Select(p => p.ToDto());
            result.AddRange(projectsForUser);
            return result;
        }
    }
}
