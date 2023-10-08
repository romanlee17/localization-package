using UnityEngine;

namespace romanlee17.Utils {
    [CreateAssetMenu(menuName = "romanlee17/Localization Settings")]
    public sealed class LocalizationSettings : ScriptableObject {

        public SystemLanguage[] languages;
        public SystemLanguage defaultLanguage = SystemLanguage.English;

    }
}