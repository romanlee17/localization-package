using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace romanlee17.UnityLocalization {
    [CreateAssetMenu(menuName = "romanlee17/Localization/Localization Table", fileName = "LocalizationTable")]
    public sealed class LocalizationTable : ScriptableObject {

        public string key = string.Empty;

        [Serializable]
        public class LanguageEntry {
            public SystemLanguage language;
            public string value;
            // Constructor.
            public LanguageEntry(SystemLanguage language, string value = "") {
                this.language = language;
                this.value = value;
            }
        }

        [Serializable]
        public class LocalizationEntry {
            public string key;
            public List<LanguageEntry> languages;
            // Indexer.
            public string this[SystemLanguage language] {
                get {
                    // Check if language entry exists.
                    if (languages.FindIndex(x => x.language == language) == -1) {
                        languages.Add(new(language));
                    }
                    // Return localized string value of entry.
                    return languages.First(x => x.language == language).value;
                }
                set {
                    // Check if language entry exists.
                    if (languages.FindIndex(x => x.language == language) == -1) {
                        languages.Add(new(language));
                    }
                    // Assign value.
                    languages.First(x => x.language == language).value = value;
                }
            }
            // Constructor.
            public LocalizationEntry() {
                key = string.Empty;
                languages = new();
            }
            // Functions.
            public string ReadValue() {
                if (string.IsNullOrEmpty(key)) {
                    return string.Empty;
                }
                return this[Localization.GetLanguage()];
            }
            // Returned when there's no entry with key specified.
            public static LocalizationEntry empty = new();
        }

        public List<LocalizationEntry> entries;

        public LocalizationEntry this[string entryKey] {
            get {
                foreach (var entry in entries) {
                    if (entry.key != entryKey) continue;
                    return entry;
                }
                Debug.LogError($"No entry found with key: <{entryKey}>.");
                return LocalizationEntry.empty;
            }
        }

    }
}