using System;
using System.Linq;
using UnityEngine;

namespace romanlee17.Localization.Master {
    [Serializable]
    public class EntryData {
        // Properties.
        public string Key {
            get => _key;
            internal set {
                _key = value;
            }
        }
        // Constructor.
        public EntryData(string key) {
            _key = key;
        }
        // Inaccessible constructor.
        private EntryData() { }
        // Inaccessible fields.
        [SerializeField] private string _key = default;
        [SerializeField] private StringData[] _languagePairs = null;
        // Internal functions.
        internal StringData Set(SystemLanguage language, string value) {
            // Check for array not to be null.
            if (_languagePairs == null) {
                _languagePairs = new StringData[0];
            }
            if (_languagePairs.Any(pair => pair.Language == language)) {
                // Language already exists in array.
                StringData firstPair = _languagePairs.First(x => x.Language == language);
                firstPair.Value = value;
                return firstPair;
            }
            else {
                // There is no language specified.
                StringData languagePair = new(language, value);
                _languagePairs = _languagePairs.Append(languagePair).ToArray();
                return languagePair;
            }
        }
        internal StringData Get(SystemLanguage language) {
            // Check for array not to be null.
            if (_languagePairs == null) {
                _languagePairs = new StringData[0];
            }
            if (_languagePairs.Any(pair => pair.Language == language)) {
                // There is a language defined in language pairs.
                return _languagePairs.First(pair => pair.Language == language);
            }
            // There is no language pair with specified langauge.
            StringData newPair = Set(language, string.Empty);
            return newPair;
        }
        // Functions.
        public string ReadValue() {
            SystemLanguage language = Localization.Settings.Language;
            StringData languagePair = _languagePairs.First(pair => pair.Language == language);
            // Check if specified language pair exists.
            if (languagePair != null) {
                // Specified language pair does exist.
                return languagePair.Value;
            }
            // Specified language pair does NOT exist.
            else {
                Debug.LogError($"EntryData: There is no such language pair ({language}).");
                return string.Empty;
            }
        }
    }
}