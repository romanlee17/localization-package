using System.Linq;
using UnityEngine;

// Localization.GetTable("Example").GetEntry("Example").ReadValue();
// Localization.LocalizeTextMeshPro("Table", "Entry");
// Localization.LocalizeLegacyText("Table", "Entry");
// TableEditor (editor)
// LocalizeTextMeshPro (component)
// LocalizeLegacyText (component)

namespace romanlee17.Localization {
    public static class Localization {
        // Inaccessible properties.
        internal static TableData[] Tables {
            get {
                // Always update tables array in editor.
                if (Application.isEditor) {
                    return Resources.LoadAll<TableData>(_folderName);
                }
                // Load tables array only once in build.
                if (_tables == null) {
                    _tables = Resources.LoadAll<TableData>(_folderName);
                }
                return _tables;
            }
        }
        // Properties.
        public static Settings Settings {
            get {
                // Always update settings in editor.
                if (Application.isEditor) {
                    return Resources.LoadAll<Settings>(_folderName)[0];
                }
                // Load settings only once in build.
                if (_settings == null) {
                    _settings = Resources.LoadAll<Settings>(_folderName)[0];
                }
                return _settings;
            }
        }
        // Inaccessible fields.
        private const string _folderName = "Localization";
        private static TableData[] _tables = null;
        private static Settings _settings = null;
        // Globally accessible functions.
        public static TableData GetTable(string key) {
            TableData table = Tables.First(table => table.Key == key);
            // Check if table with specified key exists.
            if (table != null) {
                // Table with specified key exists.
                return table;
            }
            // Table with specified key does NOT exist.
            else {
                Debug.LogError($"Localization: There is no table with key ({key}).");
                return null;
            }
        }
        public static bool IsTableExists(string key) {
            return Tables.Any(table => table.Key == key);
        }
    }
}