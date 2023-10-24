namespace romanlee17.Localization.TextMeshProEditor {
    using romanlee17.Localization.TextMeshPro;
    using TMPro;
    using UnityEditor;

    public static partial class ContextMenu {
        [MenuItem("CONTEXT/TextMeshProUGUI/romanlee17.Localization/Localize")]
        private static void LocalizeTextMeshPro(MenuCommand menuCommand) {
            TextMeshProUGUI textMeshPro = (TextMeshProUGUI)menuCommand.context;
            LocalizeTextMeshPro localizeComponent = textMeshPro.gameObject.AddComponent<LocalizeTextMeshPro>();
            localizeComponent.TextMeshPro = textMeshPro;
        }
    }
}