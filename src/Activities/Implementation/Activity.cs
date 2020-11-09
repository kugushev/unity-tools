using System;
using Common;
using UnityEngine;

namespace Activities.Implementation
{
    public abstract class Activity : MonoBehaviour, IActivity
    {
        [SerializeField] private bool disabled;
        [SerializeField] private Sprite icon;
        public bool Disabled => disabled;
        public abstract Type Active { get; }
        public abstract string FriendlyName { get; }
        public virtual Sprite Icon => icon;
        public abstract bool IsAvailable(IWorldItem active);
    }
}