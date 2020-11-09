using Activities.Animations;

namespace Activities.Common
{
    public interface ICommand
    {
        void Prepare(IAnimationsBag animationsBag);
        void Execute(IAnimationsBag animationsBag);
    }
}