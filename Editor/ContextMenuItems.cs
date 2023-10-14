using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace romanlee17.Localization {
    public class ContextMenuItems {

        [MenuItem("CONTEXT/Text/romanlee17.Localization/Localize")]
        private static void LocalizeLegacyText(MenuCommand menuCommand) {
            Text legacyText = (Text)menuCommand.context;
            legacyText.gameObject.AddComponent<LocalizeLegacyText>();
        }

        [MenuItem("CONTEXT/TextMeshProUGUI/romanlee17.Localization/Localize")]
        private static void LocalizeTextMeshPro(MenuCommand menuCommand) {
            TextMeshProUGUI textMeshPro = (TextMeshProUGUI)menuCommand.context;
            textMeshPro.gameObject.AddComponent<LocalizeTextMeshPro>();
        }

    }
}