#if UNITY_EDITOR
using TMPro;
using UnityEditor;
using UnityEngine.UI;

namespace romanlee17.Localization {
    public class ContextMenuItems {

        [MenuItem("CONTEXT/Text/romanlee17.Localization/Localize")]
        private static void LocalizeLegacyText(MenuCommand menuCommand) {
            Text legacyText = (Text)menuCommand.context;
            LocalizeLegacyText localizeComponent = legacyText.gameObject.AddComponent<LocalizeLegacyText>();
            localizeComponent.LegacyText = legacyText;
        }

        [MenuItem("CONTEXT/TextMeshProUGUI/romanlee17.Localization/Localize")]
        private static void LocalizeTextMeshPro(MenuCommand menuCommand) {
            TextMeshProUGUI textMeshPro = (TextMeshProUGUI)menuCommand.context;
            LocalizeTextMeshPro localizeComponent = textMeshPro.gameObject.AddComponent<LocalizeTextMeshPro>();
            localizeComponent.TextMeshPro = textMeshPro;
        }

    }
}
#endif