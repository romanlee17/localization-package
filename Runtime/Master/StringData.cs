using System;
using UnityEngine;

namespace romanlee17.Localization.Master {
    [Serializable]
    public class StringData {
        // Properties.
        public SystemLanguage Language {
            get => _language;
        }
        public string Value {
            get => _value;
            internal set {
                _value = value;
            }
        }
        // Constructor.
        public StringData(SystemLanguage language, string value) {
            _language = language;
            _value = value;
        }
        // Inaccessible constructor.
        private StringData() { }
        // Inaccessible fields.
        [SerializeField] private SystemLanguage _language = SystemLanguage.English;
        [SerializeField] private string _value = default;
    }
}