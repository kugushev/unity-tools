using JetBrains.Annotations;
using UnityEngine;

namespace Activities.Implementation
{
    public abstract class ActivitySync<TActive> : Activity<TActive>, IActivitySync
    {
        [CanBeNull]
        protected abstract IObjective Act(TActive active);
        
        public IObjective Act(object active)
        {
            if (active is TActive obj)
                return Act(obj);

            Debug.LogError($"Unable to cast {active} to {typeof(TActive)}");
            return null;
        }
    }
}