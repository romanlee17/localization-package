using UnityEngine;
using static romanlee17.Utils.LocalizationTable;

namespace romanlee17.Utils {
    public sealed class Localization {

        public static LocalizationSettings Settings {
            get {
                // Don't cache LocalizationSettings while in Editor.
                if (Application.isEditor) {
                    return Resources.LoadAll<LocalizationSettings>(string.Empty)[0];
                }
                // Cache LocalizationSettings in Build.
                return settings != null ? settings :
                    settings = Resources.LoadAll<LocalizationSettings>(string.Empty)[0];
            }
        }
        static LocalizationSettings settings = null;

        public static LocalizationTable[] Tables {
            get {
                // Don't cache Tables while in Editor.
                if (Application.isEditor) {
                    return Resources.LoadAll<LocalizationTable>(string.Empty);
                }
                // Cache Tables in Build.
                return tables ??= Resources.LoadAll<LocalizationTable>(string.Empty);
            }
        }
        static LocalizationTable[] tables = null;

        public static SystemLanguage GetLanguage() {
            return Settings.defaultLanguage;
        }

        public static LocalizationTable GetTable(string tableKey) {
            LocalizationTable[] tables = Tables;
            foreach (var table in tables) {
                if (table.key != tableKey) continue;
                return table;
            }
            Debug.LogError($"No table found with this key: <{tableKey}>.");
            return null;
        }

        public static LocalizationEntry GetEntry(string tableKey, string entryKey) {
            return GetTable(tableKey)[entryKey];
        }
        public static LocalizationEntry GetEntry(string address) {
            if (string.IsNullOrEmpty(address)) {
                Debug.LogError("Trying to read null or empty localization address.");
                return LocalizationEntry.empty;
            }
            string[] split = address.Split("/");
            return GetEntry(split[0], split[1]);
        }

    }
}