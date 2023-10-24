namespace romanlee17.Localization.TextMeshPro {
    using romanlee17.Localization.Master;
    using TMPro;

    public static class TextMeshProExtensions {
        public static void Localize(this TextMeshProUGUI textMeshPro, string tableKey, string entryKey) {
            textMeshPro.text = Localization.GetTable(tableKey).GetEntry(entryKey).ReadValue();
            textMeshPro.font = Localization.Settings.ProFont;
        }
    }
}