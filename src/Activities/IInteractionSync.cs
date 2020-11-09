using Common;
using JetBrains.Annotations;

namespace Activities
{
    public interface IInteractionSync: IInteraction
    {
        [CanBeNull] IObjective Interact(IWorldItem active, IWorldItem passive);
    }
}