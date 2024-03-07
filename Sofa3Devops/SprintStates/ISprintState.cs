using Sofa3Devops.Domain;


namespace Sofa3Devops.SprintStates
{
    public interface ISprintState
    {
        public void SetToOngoing(Sprint sprint);
        public void SetToFinished(Sprint sprint);
        public void SetToCanceled(Sprint sprint);
    }
}
