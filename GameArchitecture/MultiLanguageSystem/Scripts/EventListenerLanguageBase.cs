using UnityEngine;

namespace MultiLanguageText
{
    public abstract class EventListenerLanguageBase : MonoBehaviour
    {
        /*
         * Superclasse abstrata que será usada como base para todo tipo de ouvinte de GameEvents do tipo Language
         */
        [SerializeField] private GameEventLanguage gameEvent = null;

        protected virtual void OnEnable()
        {
            if (gameEvent != null) { gameEvent.Register(this); } // registro do ouvinte na lista de ouvintes ativos
        }

        protected virtual void OnDisable()
        {
            if (gameEvent != null) { gameEvent.Unregister(this); } // exclusão do ouvinte da lista de ouvintes ativos
        }

        public abstract void OnEventRaised(Language value); // função abstrata necessária em todos os ouvintes
    }
}
