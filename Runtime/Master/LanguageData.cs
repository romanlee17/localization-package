using System;
using UnityEngine;
#if TextMeshPro
using TMPro;
#endif

namespace romanlee17.Localization.Master {
    [Serializable]
    public class LanguageData {
        // Properties.
        public SystemLanguage Language {
            get => _language;
        }
        public Font LegacyFont {
            get => _legacyFont;
        }
#if TextMeshPro
        public TMP_FontAsset ProFont {
            get => _proFont;
        }
#endif
        // Inaccessible fields.
        [SerializeField] private SystemLanguage _language = SystemLanguage.English;
        [SerializeField] private Font _legacyFont = null;
#if TextMeshPro
        [SerializeField] private TMP_FontAsset _proFont = null;
#endif
    }
}