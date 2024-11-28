using Core.Entities;

namespace Application.Activity.Interfaces
{
    public interface IActivityAttendeeReadRepository
    {
        public Task<List<UserActivityEntity>> GetAllAttendeesForActivity(int ActivityId);
    }
}
