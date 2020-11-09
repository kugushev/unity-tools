using System.Threading.Tasks;
using Common;

namespace Activities.Implementation
{
    public readonly struct WrappedInteraction
    {
        public WrappedInteraction(IInteraction interaction, IWorldItem active, IWorldItem passive)
        {
            Interaction = interaction;
            Active = active;
            Passive = passive;
            IsSync = interaction is IInteractionSync;
        }

        public IInteraction Interaction { get; }
        public IWorldItem Active { get; }
        public IWorldItem Passive { get; }
        public bool IsSync { get; }

        public IObjective Execute()
        {
            if (Interaction is IInteractionSync executable)
                return executable.Interact(Active, Passive);
            throw new ApplicationException("Unable to execute not sync interaction");
        }
        
        public Task<IObjective> ExecuteAsync()
        {
            if (Interaction is IInteractionAsync executable)
                return executable.InteractAsync(Active, Passive);
            throw new ApplicationException("Unable to execute not async interaction");
        }
    }
}