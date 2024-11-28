using Core.Entities;

namespace Application.Activity.Interfaces;

public interface IActivityReadRepository
{
    public Task<List<ActivityEntity>> GetAllActivitiesAsync();
    public Task<ActivityEntity> GetActivityById(int id);
}
