namespace romanlee17.Localization.LegacyEditor {
    using UnityEngine.UI;
    using UnityEditor;
    using romanlee17.Localization.Legacy;

    public static partial class ContextMenu {
        [MenuItem("CONTEXT/Text/romanlee17.Localization/Localize")]
        private static void LocalizeLegacyText(MenuCommand menuCommand) {
            Text legacyText = (Text)menuCommand.context;
            LocalizeLegacyText localizeComponent = legacyText.gameObject.AddComponent<LocalizeLegacyText>();
            localizeComponent.LegacyText = legacyText;
        }
    }
}