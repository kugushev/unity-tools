using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils.Randomization
{
    public class CustomDistribution
    {
        private readonly Func<float, float> _distribution;
        private readonly float _stepSize;

        public CustomDistribution(int precisionDigits, Func<float, float> distribution)
        {
            _stepSize = Mathf.Pow(10, -1 * precisionDigits);
            _distribution = distribution;
        }

        public float Range(float min, float max)
        {
            float offset = min < 0 ? -min : 0;
            min += offset;
            max += offset;
            
            var extendedMax = FindExtendedMax(min, max);
            float extendedRandom = Random.Range(min, extendedMax);
            var result = FindProbabilityRange(extendedRandom, min, max);
            return result - offset;
        }

        private float FindExtendedMax(float min, float max)
        {
            float extendedMax = min;
            float step = min;
            while (step < max)
            {
                step += _stepSize;
                var probability = _distribution(step);
                extendedMax += _stepSize * probability;
            }

            extendedMax += _stepSize * _distribution(max);
            return extendedMax;
        }

        private float FindProbabilityRange(float extendedRandom, float min, float max)
        {
            float extendedStep = min;
            float step = min;
            while (step < max)
            {
                step += _stepSize;
                var probability = _distribution(step);
                extendedStep += _stepSize * probability;
                if (extendedRandom < extendedStep)
                    return step;
            }

            return max;
        }
    }
}