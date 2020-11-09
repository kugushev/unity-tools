using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Activities.Implementation
{
    public abstract class ActivityAsync<TActive> : Activity<TActive>, IActivityAsync
    {
        [CanBeNull]
        protected abstract Task<IObjective> ActAsync(TActive active);

        public Task<IObjective> ActAsync(object active)
        {
            if (active is TActive obj)
                return ActAsync(obj);

            Debug.LogError($"Unable to cast {active} to {typeof(TActive)}");
            return Task.FromResult<IObjective>(null);
        }
    }
}