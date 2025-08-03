using UnityEngine;
using UnityEditor;

public class RenderTextureCreator
{
    [MenuItem("Assets/Create/Force Render Texture")]
    public static void CreateRenderTexture()
    {
        RenderTexture rt = new RenderTexture(1920, 1080, 0);
        AssetDatabase.CreateAsset(rt, "Assets/VideoRender.renderTexture");
        AssetDatabase.SaveAssets();
        Debug.Log("✅ RenderTexture created at Assets/VideoRender.renderTexture");
    }
}
