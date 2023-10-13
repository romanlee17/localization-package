#if TextMeshPro
using TMPro;
#endif
using UnityEngine;

namespace romanlee17.Localization {
    public class LocalizeTextMeshPro : MonoBehaviour {

#if TextMeshPro
        // Inaccessible properties.
        private TextMeshProUGUI TextMeshPro {
            get {
                if (_textMeshPro == null) {
                    _textMeshPro = GetComponent<TextMeshProUGUI>();
                }
                return _textMeshPro;
            }
        }

        // Inaccessible fields.
        private TextMeshProUGUI _textMeshPro = null;
#endif

        [SerializeField] private string tableKey = default;
        [SerializeField] private string entryKey = default;

#if TextMeshPro
        private void OnEnable() {
            TextMeshPro.text = Localization.GetTable(tableKey).GetEntry(entryKey).ReadValue();
        }
#endif

    }
}