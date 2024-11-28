using Core.Entities;

namespace Application.Activity.Interfaces
{
    public interface IActivityWriteRepository
    {
        public Task<int> CreateActivityAsync(ActivityEntity Activity);
        public Task UpdateActivity(ActivityEntity Activity);
        public Task DeleteActivity(int Id);
        public Task AddAttendeeAsync(UserActivityEntity attendee);
    }
}
