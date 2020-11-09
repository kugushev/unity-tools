using System.Threading.Tasks;
using Common;
using JetBrains.Annotations;

namespace Activities
{
    public interface IInteractionAsync: IInteraction
    {
        [ItemCanBeNull] Task<IObjective> InteractAsync(IWorldItem active, IWorldItem passive);
    }
}