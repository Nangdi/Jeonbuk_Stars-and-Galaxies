using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class AltasControl 
{
    [MenuItem("Game/UI/Atlas/SpriteAtlas Exporter")]
    private static void ExportAltas()
    {
        if (Selection.objects != null && Selection.objects.Length > 0)
        {
           
            foreach (var obj in Selection.objects)
            {
                string path = AssetDatabase.GetAssetPath(obj);

                Debug.Log(path);
                if (obj.GetType() == typeof(Sprite))
                {
                    Sprite objSprite= obj as Sprite;
                    Debug.Log(objSprite.rect);
                    Texture2D texture = objSprite.texture;
                    Rect _rect=objSprite.rect;
                    Color[] c = texture.GetPixels((int)_rect.x, (int)_rect.y, (int)_rect.width, (int)_rect.height);
                    Texture2D m2Texture = new Texture2D((int)_rect.width, (int)_rect.height);
                    m2Texture.SetPixels(c);
                    m2Texture.Apply();

                    string customPath = Path.GetDirectoryName(path);
                    string fileName = customPath + Path.DirectorySeparatorChar + objSprite.name + ".png";
                    File.WriteAllBytes(fileName, m2Texture.EncodeToPNG());

                   
                }
            }
            AssetDatabase.Refresh();
        }
        
    }
}
