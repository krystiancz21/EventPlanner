using EventPlanner.Domain.Constants;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Domain.Interfaces
{
    public interface IEventPlannerAuthorizationService
    {
        bool Authorize(Workshop workshop, ResourceOperation resourceOperation);
    }
}