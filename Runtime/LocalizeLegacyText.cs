using UnityEngine.UI;

namespace romanlee17.Localization {
    public class LocalizeLegacyText : BaseLocalizeComponent {
        // Legacy text component.
        private Text LegacyText {
            get {
                if (_legacyText == null) {
                    _legacyText = GetComponent<Text>();
                }
                return _legacyText;
            }
        }
        private Text _legacyText = null;
        // Unity events.
        private void OnEnable() {
            LegacyText.text = Localization.GetTable(_tableKey).GetEntry(_entryKey).ReadValue();
        }
    }
}