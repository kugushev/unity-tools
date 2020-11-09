using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Activities
{
    public interface IActivityAsync
    {
        [ItemCanBeNull] Task<IObjective> ActAsync(object active);
    }
}