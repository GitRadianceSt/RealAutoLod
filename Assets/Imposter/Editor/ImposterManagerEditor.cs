using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ImposterManager))]
public class ImposterManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ImposterManager mgr = (ImposterManager)target;

        EditorGUILayout.LabelField("General Settings", EditorStyles.boldLabel);
        mgr.active = EditorGUILayout.Toggle("Active", mgr.active);
        mgr.imposterLayer = EditorGUILayout.LayerField("Imposter Layer", mgr.imposterLayer);
        mgr.useMainCamera = EditorGUILayout.Toggle("Use MainCamera", mgr.useMainCamera);
        if (!mgr.useMainCamera)
        {
            mgr.mainCamera = (Camera)EditorGUILayout.ObjectField("Camera", mgr.mainCamera, typeof(Camera), true);
        }
        mgr.antialiasing = EditorGUILayout.IntPopup("Texture Antialiasing", mgr.antialiasing, new string[] { "off", "2x", "4x", "8x" }, new int[] { 1, 2, 4, 8 });

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Shadows", EditorStyles.boldLabel);
        mgr.castShadow = EditorGUILayout.Toggle("Cast Shadows", mgr.castShadow);
        if (mgr.castShadow)
        {
            mgr.mainLight = (Light)EditorGUILayout.ObjectField("Directional Light", mgr.mainLight, typeof(Light), true);
        }

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Caching & Preload", EditorStyles.boldLabel);

        mgr.cachingBehaviour = (ImposterManager.CachingBehaviour)EditorGUILayout.EnumPopup("Caching Behaviour", mgr.cachingBehaviour);

        if (mgr.cachingBehaviour != ImposterManager.CachingBehaviour.discardInvisibleImpostors)
        {
            mgr.preLoadFovFactor = EditorGUILayout.Slider("Caching FOV Factor", mgr.preLoadFovFactor, 1.25f, 4.0f);

            if (mgr.cachingBehaviour == ImposterManager.CachingBehaviour.preloadAndCacheInvisibleImposters)
            {
                mgr.preloadRate = EditorGUILayout.IntField("Preloads per Frame", mgr.preloadRate);
            }
        }

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Runtime Memory Consumption", EditorStyles.boldLabel);

        if(!Application.isPlaying)
        {
            EditorGUILayout.HelpBox("Info will be avaiable at runtime", MessageType.Info);
            GUI.enabled = false;
        }

        EditorGUILayout.LabelField("Textures", "" + (mgr.imposterTextures.Count - mgr.freeImposterTextures.Count) + " / " + mgr.imposterTextures.Count.ToString());
        EditorGUILayout.LabelField("Texture Memory", mgr.textureMemory.ToString() + " MB");

        if (GUILayout.Button("Clear Unused Textures"))
        {
            mgr.garbageCollect();
        }

        EditorGUILayout.HelpBox("Use these to update the imposters when the light conditions change", MessageType.Info);
        if (GUILayout.Button("Update all Imposters instantly"))
        {
            mgr.ForceImposterUpdate();
        }

        if (GUILayout.Button("Update all Imposters slowly"))
        {
            mgr.ForceImposterUpdate(3);
        }

        EditorGUILayout.Separator();

        GUI.enabled = true;

        EditorUtility.SetDirty(mgr);
    }
}
