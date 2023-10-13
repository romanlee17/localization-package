using UnityEngine;

namespace romanlee17.Localization {
    [CreateAssetMenu(menuName = "romanlee17.Localization/Settings")]
    public class Settings : ScriptableObject {
        // Properties.
        public SystemLanguage Language {
            get => _language;
        }
        internal LanguageData[] Languages {
            get => _languagesData;
        }
        // Inaccessible fields.
        [SerializeField] private SystemLanguage _language;
        [SerializeField] private LanguageData[] _languagesData;
    }
}