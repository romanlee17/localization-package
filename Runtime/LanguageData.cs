using System;
using UnityEngine;
using TMPro;

namespace romanlee17.Localization {
    [Serializable]
    public class LanguageData {
        // Properties.
        public SystemLanguage Language {
            get => _language;
        }
        public Font LegacyFont {
            get => _legacyFont;
        }
        public TMP_FontAsset ProFont {
            get => _proFont;
        }
        // Inaccessible fields.
        [SerializeField] private SystemLanguage _language = SystemLanguage.English;
        [SerializeField] private Font _legacyFont = null;
        [SerializeField] private TMP_FontAsset _proFont = null;
    }
}