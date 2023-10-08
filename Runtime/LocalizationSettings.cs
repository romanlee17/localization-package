using UnityEngine;
#if TextMeshPro
using TMPro;
#endif

namespace romanlee17.Localization {
    [CreateAssetMenu(menuName = "romanlee17.Localization/Localization Settings", fileName = "LocalizationSettings")]
    public sealed class LocalizationSettings : ScriptableObject {

        [System.Serializable]
        public class LanguageEntry {
            [HideInInspector] public string name = "English";
            public SystemLanguage language = SystemLanguage.English;
            public Font fontLegacy = null;
#if TextMeshPro
            public TMP_FontAsset fontTMPro = null;
#endif
        }

        public LanguageEntry[] languages;
        public SystemLanguage defaultLanguage = SystemLanguage.English;

    }
}