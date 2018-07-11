using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PickupSpawner))]
[CanEditMultipleObjects]
public class SpawnerEditor : Editor {
    float maxSpecials = 40;
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        PickupSpawner pickupSpawner = target as PickupSpawner;
        List<PickupPercentage> pickupsList = pickupSpawner.specialPickups;
        int size = pickupsList.Count;
        EditorGUILayout.LabelField("Special Pickups:");
        EditorGUI.BeginChangeCheck();
        int newSize = EditorGUILayout.IntField("Types Amount:", size);
        if (newSize < 0) {
            newSize = 0;
        }
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(target, "Modify PickupList Size");
            size = newSize;
            EditorUtility.SetDirty(target);
        }
        if (size > pickupsList.Count) {
            int diff = size - pickupsList.Count;
            for (int i = 0; i < diff; i++) {
                pickupsList.Add(new PickupPercentage());
                EditorUtility.SetDirty(target);
            }
        } else if (size < pickupsList.Count) {
            int diff = pickupsList.Count - size;
            for (int i = 0; i < diff; i++) {
                pickupsList.RemoveAt(pickupsList.Count - 1);
                EditorUtility.SetDirty(target);
            }
        }
        float sum = 0;
        for (int i = 0; i < pickupsList.Count; i++) {
            GUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.LabelField("Pickup:", GUILayout.Width(50));
            Object newObject = EditorGUILayout.ObjectField(
                pickupsList[i].pickup, typeof(GameObject), true);
            EditorGUILayout.LabelField("Percentage:", GUILayout.Width(80));
            float newPercentage = EditorGUILayout.FloatField(
                pickupsList[i].percentage, GUILayout.Width(50));
            sum += newPercentage;
            if (sum > maxSpecials) {
                float diff = sum - maxSpecials;
                newPercentage -= diff;
                sum -= diff;
                string msg = string.Format("La suma no puede exceder {0}", maxSpecials);
                EditorUtility.DisplayDialog("Error", msg, "Ok");
            }
            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(target, "Modify PickupList");
                pickupsList[i].pickup = (GameObject)newObject;
                pickupsList[i].percentage = newPercentage;
                EditorUtility.SetDirty(target);
            }
            GUILayout.EndHorizontal();
        }
    }
}