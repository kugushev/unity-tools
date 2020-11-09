using System;
using System.Collections;
using UnityEngine;

namespace Utils.Randomization
{
    public class RandomController: MonoBehaviour
    {
        [SerializeField] private GameObject dropPrefab;
        [SerializeField] private AnimationCurve curve;
        private CustomDistribution _customDistribution;

        private void Awake()
        {
            _customDistribution = new CustomDistribution(1, x => curve.Evaluate(x / 18f));
        }

        private IEnumerator Start()
        {
            while (true)
            {
                var x = _customDistribution.Range(-9f, 9f);
                Instantiate(dropPrefab, new Vector3(x, 10f), Quaternion.identity);
                yield return null;
            }
        }
    }
}