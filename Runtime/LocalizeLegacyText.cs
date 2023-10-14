using UnityEngine;
using UnityEngine.UI;

namespace romanlee17.Localization {
    public class LocalizeLegacyText : BaseLocalizeComponent {
        // Properties.
        internal Text LegacyText {
            get => _legacyText;
            set => _legacyText = value;
        }
        // Inaccessible fields.
        [SerializeField] private Text _legacyText = null;
        // Unity events.
        private void OnEnable() {
            _legacyText.text = Localization.GetTable(_tableKey).GetEntry(_entryKey).ReadValue();
        }
    }
}