using System.Linq;
using UnityEngine;
#if TextMeshPro
using TMPro;
#endif

namespace romanlee17.Localization.Master {
    [CreateAssetMenu(menuName = "romanlee17.Localization/Settings")]
    public class Settings : ScriptableObject {
        // Properties.
        public SystemLanguage Language {
            get => _language;
        }
        internal LanguageData[] Languages {
            get => _languagesData;
        }
        public Font LegacyFont {
            get {
                if (_languagesData.Any(data => data.Language == Language)) {
                    LanguageData languageData = _languagesData.First(data => data.Language == Language);
                    return languageData.LegacyFont;
                }
                else {
                    Debug.LogError($"Legacy font for language ({Language}) is not defined.");
                    return null;
                }
            }
        }
#if TextMeshPro
        public TMP_FontAsset ProFont {
            get {
                if (_languagesData.Any(data => data.Language == Language)) {
                    LanguageData languageData = _languagesData.First(data => data.Language == Language);
                    return languageData.ProFont;
                }
                else {
                    Debug.LogError($"TextMeshPro font for language ({Language}) is not defined.");
                    return null;
                }
            }
        }
#endif
        // Inaccessible fields.
        [SerializeField] private SystemLanguage _language;
        [SerializeField] private LanguageData[] _languagesData;
    }
}