#if TextMeshPro
using TMPro;
using UnityEngine;

namespace romanlee17.Localization {
    public class LocalizeTextMeshPro : BaseLocalizeComponent {
        // TextMeshPro component.
        internal TextMeshProUGUI TextMeshPro {
            get => _textMeshPro;
            set => _textMeshPro = value;
        }
        [SerializeField] private TextMeshProUGUI _textMeshPro = null;
        // Unity events.
        private void OnEnable() {
            TextMeshPro.text = Localization.GetTable(_tableKey).GetEntry(_entryKey).ReadValue();
        }
    }
}
#endif