using System.IO;
using UnityEngine;

namespace Completed
{
    public class RogueUtils
    {
        public static Sprite generateSpriteFromTexture(Texture2D tex)
        {
            return generateSpriteFromTexture(tex, tex.width, tex.height);
        }

        public static Sprite generateSpriteFromTexture(Texture2D tex, int width, int height)
        {
            return Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        }

        public static Sprite generateSpriteFromTexture(Texture2D tex, float width, float height)
        {
            return Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        }

        public static string getEquipmentIconDir()
        {
            return "Assets/Sprites/equipment";
        }

        public static string getUIIconDir()
        {
            return "Assets/Sprites/ui";
        }

        public static Texture2D getTextureFromPath(string filePath)
        {
            Texture2D tex = null;
            byte[] fileData;

            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2);
                tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            }
            return tex;
        }

        public static Rect GUIRectWithObject(GameObject go)
        {
            Vector3 cen = go.GetComponent<Renderer>().bounds.center;
            Vector3 ext = go.GetComponent<Renderer>().bounds.extents;
            Vector2[] extentPoints = new Vector2[8]
            {
                WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y-ext.y, cen.z-ext.z)),
                WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y-ext.y, cen.z-ext.z)),
                WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y-ext.y, cen.z+ext.z)),
                WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y-ext.y, cen.z+ext.z)),
                WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y+ext.y, cen.z-ext.z)),
                WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y+ext.y, cen.z-ext.z)),
                WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y+ext.y, cen.z+ext.z)),
                WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y+ext.y, cen.z+ext.z))
            };
            Vector2 min = extentPoints[0];
            Vector2 max = extentPoints[0];
            foreach (Vector2 v in extentPoints)
            {
                min = Vector2.Min(min, v);
                max = Vector2.Max(max, v);
            }
            return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
        }

        public static Vector2 WorldToGUIPoint(Vector3 world)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(world);
            screenPoint.y = (float)Screen.height - screenPoint.y;
            return screenPoint;
        }
    }
}
