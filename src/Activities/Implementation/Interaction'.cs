using System;
using Common;

namespace Activities.Implementation
{
    public abstract class Interaction<TActive, TPassive> : Interaction
        where TActive : IWorldItem
        where TPassive : IWorldItem
    {
        protected abstract bool IsAvailable(TActive active, TPassive passive);
        public sealed override Type Active => typeof(TActive);
        public sealed override Type Passive => typeof(TPassive);

        public sealed override bool IsAvailable(IWorldItem active, IWorldItem passive)
        {
            if (TryCast(active, passive, out var activeObj, out var passiveObj))
                return IsAvailable(activeObj, passiveObj);

            return false;
        }

        protected static bool TryCast(IWorldItem active, IWorldItem passive, out TActive activeObj, out TPassive passiveObj)
        {
            if (active is TActive activeObject && passive is TPassive passiveObject)
            {
                activeObj = activeObject;
                passiveObj = passiveObject;
                return true;
            }

            activeObj = default;
            passiveObj = default;
            return false;
        }
    }
}