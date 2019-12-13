using UnityEngine;
using System;
using System.Collections.Generic;

namespace MultiLanguageText
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))] // requerição de um certo componente
    public class LanguageManager : EventListenerLanguageBase // herda a superclasse abstrata
    {
        [SerializeField] private LanguageVariable languageVariable; // variavel que guarda o atual idioma do jogo
        [SerializeField] private List<MultiLanguageText> languageTextList = new List<MultiLanguageText>(); // lista com todos os idiomas e seus respectivos textos

        private Language previousLanguage = null; // idioma anterior
        private TMPro.TextMeshProUGUI textMesh; // componente TextMeshPro do objeto

        protected override void OnEnable()
        {
            base.OnEnable(); // uso do register

            if (!textMesh) { textMesh = GetComponent<TMPro.TextMeshProUGUI>(); } // o ponteiro só recebe o TextMeshPro uma vez
            ChangeLanguage(languageVariable); // muda o objeto de linguagem
        }

        public override void OnEventRaised(Language value) { ChangeLanguage(value); } // preenchimento da função OnEventRaised()

        #region Changing the language
        public void ChangeLanguage(Language language)
        {
            /*
             * Muda o idioma do TextMeshPro comparando o idioma atual com cada um dos registrados dentro da lista de structs (List<MultiLanguageText>)
             * Depois de encontrar substitui o texto do TextMeshPro e muda a previousLanguage
             */
            if (language != previousLanguage)
            {
                foreach (MultiLanguageText item in languageTextList)
                {
                    if (item.language == language)
                    {
                        textMesh.text = item.text;
                        previousLanguage = language;
                        return; // depois que o idioma é encontrado ele simpleste para o foreach
                    }
                }
            }
        }

        public void ChangeLanguage(int index)
        {
            /*
             * Função que existe apenas para motivos de Debug
             */
            textMesh.text = languageTextList[index].text;
            previousLanguage = languageTextList[index].language;
        }
        #endregion

        public void AddNewText() { languageTextList.Add(new MultiLanguageText()); } // função usada no Editor do script
    }

    [Serializable]
    public struct MultiLanguageText
    {
        /*
         * Struct que define qual texto representa qual idioma
         */
        public string name;
        public Language language;
        [TextArea(2, 10)] public string text;
    }

    public class LanguageAlreadyAdded : Exception { } // ainda não implementado
}
