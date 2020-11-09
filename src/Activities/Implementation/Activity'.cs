using System;
using Common;

namespace Activities.Implementation
{
    public abstract class Activity<TActive>: Activity
    {
        protected abstract bool IsAvailable(TActive active);
        public override Type Active => typeof(TActive);
        
        public override bool IsAvailable(IWorldItem active)
        {
            if (active is TActive obj)
                return IsAvailable(obj);
            return false;
        }
    }
}