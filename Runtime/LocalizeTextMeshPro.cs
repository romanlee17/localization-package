#if TextMeshPro
using TMPro;
#endif
using UnityEngine;

namespace romanlee17.Localization {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizeTextMeshPro : MonoBehaviour {

        public string tableKey;
        public string entryKey;

#if TextMeshPro
        [SerializeField] TextMeshProUGUI textMeshProUGUI;
        void OnEnable() {
            textMeshProUGUI.text = Localization.GetTable(tableKey)[entryKey].ReadValue();
        }
        void Reset() {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }
#endif

    }
}