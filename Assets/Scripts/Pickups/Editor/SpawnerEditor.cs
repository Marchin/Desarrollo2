using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PickupSpawner))]
[CanEditMultipleObjects]
public class SpawnerEditor : Editor {
    int size = 0;
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        PickupSpawner pickupSpawner = target as PickupSpawner;
        List<PickupPercentage> pickupsList = pickupSpawner.specialPickups;

        size = EditorGUILayout.IntField("Size", size);
        //pickupPercentages. = size;
        if (size < 0) {
            size = 0;
        }
        if (size > pickupsList.Count) {
            int diff = size - pickupsList.Count;
            for (int i = 0; i < diff; i++) {
                pickupsList.Add(new PickupPercentage());
                EditorUtility.SetDirty(target);
            }
        } else if (size < pickupsList.Count) {
            int diff = size - pickupsList.Count;
            for (int i = 0; i < diff; i++) {
                pickupsList.Remove(pickupsList[pickupsList.Count - i]);
                EditorUtility.SetDirty(target);
            }
        }
        for (int i = 0; i < pickupsList.Count; i++) {
            GUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            Object newObject = EditorGUILayout.ObjectField(pickupsList[i].pickup, typeof(GameObject), true);
            float newPercentage = EditorGUILayout.FloatField("Percentage", pickupsList[i].percentage);
            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(target, "Modify Pickup List");
                pickupsList[i].pickup = (GameObject)newObject;
                pickupsList[i].percentage = newPercentage;
                EditorUtility.SetDirty(target);
            }
            GUILayout.EndHorizontal();
        }
    }
}