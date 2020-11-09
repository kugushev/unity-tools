using System;
using Common;

namespace Activities
{
    public interface IInteraction: IActivity
    {
        Type Passive { get; }
        bool IsAvailable(IWorldItem active, IWorldItem passive);
    }
}