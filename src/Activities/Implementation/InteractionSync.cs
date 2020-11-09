using Common;
using JetBrains.Annotations;
using UnityEngine;

namespace Activities.Implementation
{
    public abstract class InteractionSync<TActive, TPassive>: Interaction<TActive, TPassive>, IInteractionSync
        where TActive : IWorldItem
        where TPassive : IWorldItem
    {
        [CanBeNull] protected abstract IObjective Interact(TActive active, TPassive passive);
        
        public IObjective Interact(IWorldItem active, IWorldItem passive)
        {
            if (TryCast(active, passive, out var activeObj, out var passiveObj))
                return Interact(activeObj, passiveObj);
            
            Debug.LogError($"Unable to cast {active} and {passive} to {typeof(TActive)} and {typeof(TPassive)}");
            return null;
        }
    }
}