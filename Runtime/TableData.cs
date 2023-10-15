using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace romanlee17.Localization {
    [CreateAssetMenu(menuName = "romanlee17.Localization/Table")]
    public class TableData : ScriptableObject {
        // Properties.
        public string Key {
            get => _key;
            internal set {
                _key = value;
            }
        }
        internal int Count {
            get {
                if (_entries == null) {
                    return -1;
                }
                return _entries.Length;
            }
        }
        internal EntryData[] Entries {
            get => _entries;
        }
        // Inaccessible fields.
        [SerializeField] private string _key = default;
        [SerializeField] private EntryData[] _entries = null;
        // Functions.
        public EntryData GetEntry(string key) {
            EntryData entry = _entries.First(entry => entry.Key == key);
            // Check if entry with specified key exists.
            if (entry != null) {
                // Entry with specified key exists.
                return entry;
            }
            // Entry with specified key does NOT exist.
            else {
                Debug.LogError($"TableData: There is no entry with key ({key}).");
                return null;
            }
        }
        public bool IsEntryExists(string key) {
            return _entries.Any(entry => entry.Key == key);
        }
        internal void RemoveAt(int index) {
            List<EntryData> list = _entries.ToList();
            list.RemoveAt(index);
            _entries = list.ToArray();
        }
        internal EntryData Create(string key) {
            EntryData entry = new(key);
            _entries = _entries.Append(entry).ToArray();
            return entry;
        }
    }
}