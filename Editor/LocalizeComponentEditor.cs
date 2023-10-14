using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace romanlee17.Localization {
    public class LocalizeComponentEditor : Editor {

        // Inaccessible properties.
        private GUIStyle Wrapper {
            get {
                return new(EditorStyles.textField) {
                    wordWrap = true
                };
            }
        }

        // Inaccessible fields.
        private const int _space = 10;
        private const int _indent = 8;
        private const int _labelWidth = 120;
        private const int _searchHeight = 120;
        private bool _allowEntryEditor = false;
        private string _tablePrompt = default;
        private string _entryPrompt = default;
        private string _entryChange = default;
        private Vector2 _tableScroll = default;
        private Vector2 _entryScroll = default;
        private TableData[] _tablesResult = null;
        private EntryData[] _entriesResult = null;

        // Override editor.
        public override void OnInspectorGUI() {
            BaseLocalizeComponent component = target as BaseLocalizeComponent;
            base.OnInspectorGUI();
            GUILayout.Space(_space);

            GUILayoutOption proportionalField = GUILayout.Width(Screen.width * 0.6f);

            // Detect component changes.
            EditorGUI.BeginChangeCheck();

            _allowEntryEditor = false;
            // Check if table key is valid and this table exists.
            if (string.IsNullOrEmpty(component.TableKey) || Localization.IsTableExists(component.TableKey) == false) {
                EditorGUILayout.HelpBox("Table key is not valid.", MessageType.Error);
                GUILayout.Space(_space);
                // Search for table key.
                GUILayout.Label("Search table");
                _tablePrompt = GUILayout.TextField(_tablePrompt);
                if (string.IsNullOrEmpty(_tablePrompt) == false) {
                    GUILayout.Space(_space);
                    _tableScroll = GUILayout.BeginScrollView(_tableScroll, EditorStyles.helpBox, GUILayout.MinHeight(_searchHeight));
                    _tablesResult = Localization.Tables.OrderByDescending(table => table.Key.SimilarityScore(_tablePrompt)).ToArray();
                    for (int x = 0; x < _tablesResult.Length; x++) {
                        if (GUILayout.Button(_tablesResult[x].Key)) {
                            component.TableKey = _tablesResult[x].Key;
                        }
                    }
                    GUILayout.EndScrollView();
                }
            }
            // Considering table does exist and is valid,
            // now do the same thing for table entry.
            else if (string.IsNullOrEmpty(component.EntryKey) || Localization.GetTable(component.TableKey).IsEntryExists(component.EntryKey) == false) {
                EditorGUILayout.HelpBox("Table key is valid.", MessageType.Info);
                EditorGUILayout.HelpBox("Entry key is not valid.", MessageType.Error);
                GUILayout.Space(_space);
                // Search for entry key.
                GUILayout.Label("Search entry");
                _entryPrompt = GUILayout.TextField(_entryPrompt);
                if (string.IsNullOrEmpty(_entryPrompt) == false) {
                    GUILayout.Space(_space);
                    _entryScroll = GUILayout.BeginScrollView(_entryScroll, EditorStyles.helpBox, GUILayout.MinHeight(_searchHeight));
                    TableData table = Localization.GetTable(component.TableKey);
                    _entriesResult = table.Entries.OrderByDescending(entry => entry.Key.SimilarityScore(_entryPrompt)).ToArray();
                    for (int x = 0; x < _entriesResult.Length; x++) {
                        if (GUILayout.Button(_entriesResult[x].Key)) {
                            component.EntryKey = _entriesResult[x].Key;
                        }
                    }
                    GUILayout.EndScrollView();
                }
            }
            // Table and entry keys are valid.
            else {
                EditorGUILayout.HelpBox("Table key is valid.", MessageType.Info);
                EditorGUILayout.HelpBox("Entry key is valid.", MessageType.Info);
                GUILayout.Space(_space);
                _allowEntryEditor = true;
            }

            // Mark component as dirty if changes occured.
            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(component);
                // Since Unity has problems with SetDirty, mark currently active
                // scene as dirty too, so changes will be saved for sure.
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }

            // Detect table changes.
            EditorGUI.BeginChangeCheck();

            // Entry editor inside component.
            if (_allowEntryEditor) {
                GUILayout.Label("Entry editor");
                // Edit entry key value.
                GUILayout.BeginHorizontal();
                GUILayout.Label("Key");
                _entryChange = GUILayout.TextField(_entryChange, proportionalField);
                GUILayout.EndHorizontal();
                // Get reference for entry.
                EntryData entryReference = Localization.GetTable(component.TableKey).GetEntry(component.EntryKey);
                // Set entry key value.
                if (string.IsNullOrEmpty(_entryChange) == false) {
                    component.EntryKey = entryReference.Key = _entryChange;
                }
                else {
                    _entryChange = entryReference.Key;
                }
                // Edit entry language pairs.
                for (int x = 0; x < Localization.Settings.Languages.Length; x++) {
                    SystemLanguage language = Localization.Settings.Languages[x].Language;
                    StringData languagePair = entryReference.Get(language);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"{languagePair.Language}");
                    languagePair.Value = GUILayout.TextArea(languagePair.Value, Wrapper, proportionalField);
                    GUILayout.EndHorizontal();
                }
            }

            GUILayout.Space(_space);

            // Mark table as dirty if changes occured.
            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(component);
                TableData table = Localization.GetTable(component.TableKey);
                if (table != null) {
                    EditorUtility.SetDirty(table);
                }
                // Since Unity has problems with SetDirty, mark currently active
                // scene as dirty too, so changes will be saved for sure.
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }

            // Unselect anything when clicking on window background.
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "", GUIStyle.none)) {
                GUI.FocusControl(null);
            }

        }

    }
}