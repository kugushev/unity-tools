using System;
using Common;
using JetBrains.Annotations;
using UnityEngine;

namespace Activities
{
    public interface IActivity
    {
        bool Disabled { get; }
        Type Active { get; }
        string FriendlyName { get; }
        [CanBeNull] Sprite Icon { get; }
        bool IsAvailable(IWorldItem active);
    }
}