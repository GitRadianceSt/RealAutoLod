using UnityEngine;
using System.Collections;

[System.Serializable]
public class ImposterTexture
{
    public int size;
    public RenderTexture texture;
    public ImposterProxy owner;
    public int x, y, h, w;
    public int createdTime, lastUsedTime;

    public float getMemoryAmount()
    {
        return 12.0f * (size * size) / (1024 * 1024); 
    }
}