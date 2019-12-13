using UnityEngine;

namespace MultiLanguageText
{
    [CreateAssetMenu(menuName = "MultiLanguageSystem/Language", fileName = "New Language")]
    public class Language : ScriptableObject 
    {
        public string languageName; // depois será comparado com a liguagem do sistema do usuário
    }
}
