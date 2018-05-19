using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Imposter))]
public class ImposterEditor : Editor
{
    public static string[] textureSizeOptions = new string[] { "512x512 pixel", "256x256 pixel", "128x128 pixel", "64x64 pixel", "32x32x pixel" };
    public static int[] textureSizeInts = new int[] { 512, 256, 128, 64, 32 };

    public override void OnInspectorGUI()
    {
        Imposter imposter = (Imposter)target;

        imposter.lodMethod = (Imposter.ImposterLodMethod)EditorGUILayout.EnumPopup("LOD Metric", imposter.lodMethod);

        switch (imposter.lodMethod)
        {
            case Imposter.ImposterLodMethod.Distance:
                imposter.maxDistance = EditorGUILayout.FloatField("Distance to Camera", imposter.maxDistance);
                break;
            case Imposter.ImposterLodMethod.ScreenSize:
                break;
        }

        imposter.zOffset = EditorGUILayout.Slider("Z Offset", imposter.zOffset, 0.0f, 1.0f);

        imposter.castShadow = EditorGUILayout.Toggle("Cast Shadow", imposter.castShadow);
        if (imposter.castShadow)
        {
            imposter.maxShadowDistance = EditorGUILayout.Slider("Max Shadow Distance", imposter.maxShadowDistance, 0.0f, 5.0f);
            imposter.shadowDownSampling = EditorGUILayout.IntSlider("Shadow Downsampling", imposter.shadowDownSampling, 0, 3);
            imposter.shadowZOffset = EditorGUILayout.Slider("Shadow Offset", imposter.shadowZOffset, 0.0f, 1.0f);
        }
        
        imposter.maxTextureSize = EditorGUILayout.IntPopup("Texture Size", imposter.maxTextureSize,
            new string[] { "512x512 pixel", "256x256 pixel", "128x128 pixel", "64x64 pixel", "32x32x pixel" },
            new int[] { 512, 256, 128, 64, 32 });

        imposter.angleTolerance = EditorGUILayout.Slider("Max. Angle Error (°)", imposter.angleTolerance, 0.1f, 45.0f);
        imposter.distanceTolerance = EditorGUILayout.Slider("Max. Distance Error (%)", imposter.distanceTolerance, 1.0f, 100.0f);

        imposter.dynamic = EditorGUILayout.Toggle("Dynamic", imposter.dynamic);
        if (imposter.dynamic)
        {
            imposter.updateInterval = EditorGUILayout.Slider("Update Intervall (sec.)", imposter.updateInterval, 0.1f, 60.0f);
        }

        EditorUtility.SetDirty(imposter);
    }
}