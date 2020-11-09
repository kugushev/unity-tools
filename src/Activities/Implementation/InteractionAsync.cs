using System.Threading.Tasks;
using Common;
using UnityEngine;

namespace Activities.Implementation
{
    public abstract class InteractionAsync<TActive, TPassive>: Interaction<TActive, TPassive>, IInteractionAsync
        where TActive : IWorldItem
        where TPassive : IWorldItem
    {
        protected abstract Task<IObjective> InteractAsync(TActive active, TPassive passive);
        
        public Task<IObjective> InteractAsync(IWorldItem active, IWorldItem passive)
        {
            if (TryCast(active, passive, out var activeObj, out var passiveObj))
                return InteractAsync(activeObj, passiveObj);
            
            Debug.LogError($"Unable to cast {active} and {passive} to {typeof(TActive)} and {typeof(TPassive)}");
            return Task.FromResult<IObjective>(null);;
        }
    }
}