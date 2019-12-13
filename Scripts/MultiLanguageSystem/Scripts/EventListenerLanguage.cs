using UnityEngine;

namespace MultiLanguageText
{
    public class EventListenerLanguage : EventListenerLanguageBase
    {
        /*
         * Ouvinte padrão
         */
        [SerializeField] private UnityEventLanguage response; // UnityEvent responsável por chamar alguma certa função de um certo componente dentro da Unity
        public override void OnEventRaised(Language value) { response.Invoke(value); } // preenchimento da função OnEventRaised()
    }
}
