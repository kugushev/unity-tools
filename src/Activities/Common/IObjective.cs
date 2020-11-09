using JetBrains.Annotations;

namespace Activities.Common
{
    public interface IObjective
    {
        bool IsCompleted { get; }
        bool TryNextCommand(out ICommand command);
    }
}