using UnityEngine;

namespace MultiLanguageText
{
    [CreateAssetMenu(menuName = "Variable/Language", fileName = "New Variable Language")]
    public class LanguageVariable : ScriptableObject 
    {
        public enum GameEventType
        {
            /*
             * Define os tipos de eventos que serão chamados quando a variável mudar de valor
             */
            Language,
            Void,
            LanguageAndVoid
        }

        public GameEventType gameEventType; // variável que guarda qual o tipo de evento será chamado
        public GameEventLanguage changedEventLanguage; // evento do tipo Language que será chamado
        public GameEventVoid changedEventVoid; // evento do tipo Void que será chamado

        [Space]
        [SerializeField] private Language value; // valor da variável

        public Language Value // propriedade que informa quando a variável sofre alguma alteração
        {
            get { return value; }
            set { SetValue(value); }
        }

        public void SetValue(Language value) 
        {
            /*
             * Função necessária, ela pode ser chamada depois
             * Muda o valor da variável dentro do ScriptableObject
             */
            this.value = value;

            Debug.Log("Game language has changed to " + value.languageName);

            if (changedEventLanguage != null)
            {
                switch (gameEventType)
                {
                    case GameEventType.Language:
                        if (changedEventLanguage != null)
                        {
                            /*
                             * Chama apenas o GameEvent do tipo Language
                             */
                            changedEventLanguage.Raise(value);
                        }
                        break;
                    case GameEventType.Void:
                        if (changedEventVoid != null)
                        {
                            /*
                             * Chama apenas o GameEvent do tipo Void
                             */
                            changedEventVoid.Raise();
                        }
                        break;
                    default:
                        if (changedEventLanguage != null && changedEventVoid != null)
                        {
                            /*
                             * Chama os dois tipos de GameEvents (Language e Void)
                             */
                            changedEventVoid.Raise();
                            changedEventLanguage.Raise(value);
                        }
                        break;
                }
            }
        }

        public static implicit operator Language(LanguageVariable variable)
        {
            // melhora a parte de recebimento
            return variable.Value;
        }
    }
}
