#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;

namespace GameAudio
{
    [CustomEditor(typeof(AbstractAudioPlayer<>), true)]
    public class AbstractAudioPlayerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Update the serialized object
            serializedObject.Update();

            // Draw all properties except the one(s) we want to handle manually
            DrawPropertiesExcluding(serializedObject, "m_Script", "audioClipPairs");

            // Now draw the audioClipPairs property manually
            SerializedProperty audioClipPairs = serializedObject.FindProperty("audioClipPairs");
            EditorGUILayout.PropertyField(audioClipPairs, true);

            // Check for duplicate enum values
            CheckForDuplicateEnums(audioClipPairs);

            // Apply changes to the serialized object
            serializedObject.ApplyModifiedProperties();
        }

        private void CheckForDuplicateEnums(SerializedProperty audioClipPairs)
        {
            Dictionary<int, int> enumValueCounts = new();

            for (int i = 0; i < audioClipPairs.arraySize; i++)
            {
                SerializedProperty audioClipPair = audioClipPairs.GetArrayElementAtIndex(i);
                SerializedProperty keyProperty = audioClipPair.FindPropertyRelative("key");
                int enumValueIndex = keyProperty.enumValueIndex; // Directly use the index

                // Count the occurrences of enum values
                if (enumValueCounts.TryGetValue(enumValueIndex, out int count))
                {
                    enumValueCounts[enumValueIndex] = count + 1;
                }
                else
                {
                    enumValueCounts[enumValueIndex] = 1;
                }
            }

            // Check if any enum value occurs more than once
            foreach (KeyValuePair<int, int> kvp in enumValueCounts)
            {
                if (kvp.Value > 1) // If any enum value is used more than once
                {
                    string enumName = audioClipPairs.GetArrayElementAtIndex(0)
                                            .FindPropertyRelative("key")
                                            .enumNames[kvp.Key]; // Get the enum name using the index

                    // Display the error message for duplicates
                    EditorGUILayout.HelpBox($"Duplicate enum value '{enumName}' used {kvp.Value} times. Each enum value should only be used once.", MessageType.Error);
                }
            }
        }

    }
}
#endif