using System;
using System.Collections.Generic;
using System.Linq;
using Activities.Implementation;
using UnityEngine;

namespace Activities.Managers
{
    public class InteractionsManager : MonoBehaviour
    {
        [SerializeField] private List<Interaction> interactions;

        private void Awake()
        {
            interactions = GetComponents<Interaction>().Where(x => !x.Disabled).ToList();
        }

        public IReadOnlyList<WrappedInteraction> GetInteractions(IWorldItem active, IReadOnlyList<IWorldItem> targets)
        {
            var result = new List<WrappedInteraction>();

            var filtered = FindPairs(active.GetType(), targets);
            foreach (var (interaction, passive) in filtered)
            {
                var isAvailable = interaction.IsAvailable(active, passive);
                if (isAvailable)
                    result.Add(new WrappedInteraction(interaction, active, passive));
            }

            return result;
        }

        private IEnumerable<(Interaction interaction, IWorldItem passive)> FindPairs(Type activeInteractor,
            IReadOnlyList<IWorldItem> worldItems)
        {
            var activeInteractions = FindInteractionsToType(activeInteractor, interactions, x => x.Active).ToArray();
            foreach (var worldItem in worldItems)
            {
                var passiveInteractor = worldItem.GetType();
                var passiveInteractions = FindInteractionsToType(passiveInteractor, activeInteractions, x => x.Passive);
                foreach (var interaction in passiveInteractions)
                {
                    yield return (interaction, worldItem);
                }
            }
        }

        private static IEnumerable<Interaction> FindInteractionsToType(Type interactor,
            IEnumerable<Interaction> interactions, Func<Interaction, Type> selector)
        {
            var interfaces = interactor.GetInterfaces();
            foreach (var interaction in interactions)
            {
                var expectedInteractorType = selector(interaction);
                if (interactor == expectedInteractorType || interfaces.Contains(expectedInteractorType))
                {
                    yield return interaction;
                }
            }
        }
    }
}