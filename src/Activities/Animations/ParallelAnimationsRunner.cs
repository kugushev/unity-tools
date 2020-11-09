using System.Collections;
using UnityEngine;

namespace Activities.Animations
{
    public class ParallelAnimationsRunner: MonoBehaviour
    {
        private int _counter;
        
        public IEnumerator Animate(AnimationsCollector collector)
        {
            _counter = collector.Animations.Count;
            foreach (var coroutine in collector.Animations) 
                StartCoroutine(ExecuteAnimation(coroutine));

            yield return new WaitUntil(() => _counter == 0);
        }

        private IEnumerator ExecuteAnimation(IEnumerator coroutine)
        {
            yield return StartCoroutine(coroutine);
            _counter--;
        }
    }
}