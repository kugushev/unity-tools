using System.Collections;

namespace Activities.Animations
{
    public interface IAnimationsBag
    {
        void Put(IEnumerator coroutine);
    }
}