using TMPro;
using UnityEngine;

namespace romanlee17.Utils {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizeTextMeshPro : MonoBehaviour {

        public string tableKey;
        public string entryKey;

        [SerializeField] TextMeshProUGUI textMeshProUGUI;

        void OnEnable() {
            textMeshProUGUI.text = Localization.GetTable(tableKey)[entryKey].ReadValue();
        }

        void Reset() {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

    }
}