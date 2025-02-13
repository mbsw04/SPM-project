using dotnetTest.Data;
using dotnetTest.Models;

namespace dotnetTest.Repositories;

using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProjectInfoRepository(MongoDbContext context)
{
    private readonly IMongoCollection<ProjectInfo> _ProjectInfos = context.GetCollection<ProjectInfo>("Projects");

    public async Task<List<ProjectInfo>> GetAllProjectInfosAsync()
    {
        return await _ProjectInfos.Find(_ => true).ToListAsync();
    }

    public async Task<ProjectInfo> GetProjectInfoByIdAsync(string id)
    {
        return await _ProjectInfos.Find(projectInfo => projectInfo.ProjectId == id).FirstOrDefaultAsync();
    }

    public async Task CreateProjectInfoAsync(ProjectInfo projectInfo)
    {
        await _ProjectInfos.InsertOneAsync(projectInfo);
    }

    public async Task UpdateProjectInfoAsync(string id, ProjectInfo projectInfo)
    {
        await _ProjectInfos.ReplaceOneAsync(u => u.ProjectId == id, projectInfo);
    }

    public async Task DeleteProjectInfoAsync(string id)
    {
        await _ProjectInfos.DeleteOneAsync(u => u.ProjectId == id);
    }
    
    public void AddProject(ProjectInfo project)
    {
        _ProjectInfos.InsertOne(project);
    }
}
