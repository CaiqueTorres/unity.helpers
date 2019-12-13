using System.Collections.Generic;
using UnityEngine;

namespace MultiLanguageText
{
    [CreateAssetMenu(menuName = "GameEvent/Language", fileName = "New Language GameEvent ")]
    public class GameEventLanguage : ScriptableObject 
    {
        private List<EventListenerLanguageBase> eventListeners = new List<EventListenerLanguageBase>(); // lista com todos os ouvintes do GameEventLanguage ativos
        public Language reference = null; // variavel que permite a visualização do valor que está sendo passado durante a execução do jogo
        public string r = "null";

        public void Raise(Language value)
        {
            /*
             * Função que troca o idioma de cada um dos LanguageManagers do jogo
             */

            reference = value;
            r = value.languageName;
            foreach (EventListenerLanguageBase item in eventListeners) { item.OnEventRaised(value); } // executa o comando dentro de OnEventRaised
        }

        public void Register(EventListenerLanguageBase listener) // registra o EventListenerLanguage na lista dos EventListenerLanguages ativos
        {
            if (!eventListeners.Contains(listener)) { eventListeners.Add(listener); }
        }

        public void Unregister(EventListenerLanguageBase listener) // exclui o EventListenerLanguage da lista dos EventListenerLanguages ativos
        {
            if (eventListeners.Contains(listener)) { eventListeners.Remove(listener); }
        }
    }
}
