#if TextMeshPro
using TMPro;
#endif

namespace romanlee17.Localization {
    public class LocalizeTextMeshPro : BaseLocalizeComponent {
#if TextMeshPro
        // TextMeshPro component.
        private TextMeshProUGUI TextMeshPro {
            get {
                if (_textMeshPro == null) {
                    _textMeshPro = GetComponent<TextMeshProUGUI>();
                }
                return _textMeshPro;
            }
        }
        private TextMeshProUGUI _textMeshPro = null;
        // Unity events.
        private void OnEnable() {
            TextMeshPro.text = Localization.GetTable(_tableKey).GetEntry(_entryKey).ReadValue();
        }
#endif
    }
}