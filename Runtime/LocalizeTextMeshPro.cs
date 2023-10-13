#if TextMeshPro
using TMPro;
#endif
using UnityEngine;

namespace romanlee17.Localization {
    public class LocalizeTextMeshPro : MonoBehaviour {
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
#endif
        // Properties.
        public string TableKey {
            get => _tableKey;
            internal set {
                _tableKey = value;
            }
        }
        public string EntryKey {
            get => _entryKey;
            internal set {
                _entryKey = value;
            }
        }
        // Inaccessible hidden fields.
        [SerializeField, HideInInspector] private string _tableKey = default;
        [SerializeField, HideInInspector] private string _entryKey = default;
#if TextMeshPro
        private void OnEnable() {
            TextMeshPro.text = Localization.GetTable(_tableKey).GetEntry(_entryKey).ReadValue();
        }
#endif
    }
}