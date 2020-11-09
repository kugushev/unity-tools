using System;
using Common;

namespace Activities.Implementation
{
    public abstract class Interaction: Activity, IInteraction
    {
        public abstract Type Passive { get; }
        public abstract bool IsAvailable(IWorldItem active, IWorldItem passive);

        public sealed override bool IsAvailable(IWorldItem active) => false; // cant interact with yourself
    }
}