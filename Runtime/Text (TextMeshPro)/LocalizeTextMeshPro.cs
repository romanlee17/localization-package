using romanlee17.Localization.Master;
using TMPro;
using UnityEngine;

namespace romanlee17.Localization.TextMeshPro {
    public class LocalizeTextMeshPro : BaseLocalizeComponent {
        // TextMeshPro component.
        internal TextMeshProUGUI TextMeshPro {
            get => _textMeshPro;
            set => _textMeshPro = value;
        }
        [SerializeField] private TextMeshProUGUI _textMeshPro = null;
        // Unity events.
        private void OnEnable() {
            _textMeshPro.Localize(_tableKey, _entryKey);
        }
    }
}