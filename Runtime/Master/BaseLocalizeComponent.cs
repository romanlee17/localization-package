using UnityEngine;

namespace romanlee17.Localization.Master {
    [DisallowMultipleComponent]
    public abstract class BaseLocalizeComponent : MonoBehaviour {
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
        // Inaccessible editor fields.
        [SerializeField] protected string _tableKey = default;
        [SerializeField] protected string _entryKey = default;
    }
}