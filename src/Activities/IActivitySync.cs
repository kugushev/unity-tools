using JetBrains.Annotations;

namespace Activities
{
    public interface IActivitySync
    {
        [CanBeNull] IObjective Act(object active);
    }
}