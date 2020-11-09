using System.Collections;
using System.Collections.Generic;

namespace Activities.Animations
{
    public class AnimationsCollector : IAnimationsBag
    {
        private readonly List<IEnumerator> _animations = new List<IEnumerator>();
        public IReadOnlyList<IEnumerator> Animations => _animations;

        public void Put(IEnumerator coroutine)
        {
            _animations.Add(coroutine);
        }

        public AnimationsCollector Merge(AnimationsCollector another)
        {
            var collector = new AnimationsCollector();
            collector._animations.AddRange(_animations);
            collector._animations.AddRange(another._animations);
            return collector;
        }
    }
}