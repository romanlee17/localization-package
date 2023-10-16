namespace romanlee17.Localization.Legacy {
    using romanlee17.Localization.Master;
    using UnityEngine.UI;

    public static class LegacyTextExtensions {
        public static void Localize(this Text legacyText, string tableKey, string entryKey) {
            legacyText.text = Localization.GetTable(tableKey).GetEntry(entryKey).ReadValue();
            legacyText.font = Localization.Settings.LegacyFont;
        }
    }
}