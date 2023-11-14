﻿using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace ButtSaber.Classes
{
    class Utilities
    {
        public static IEnumerable GetAllClasses(string nameSpace)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            if (!String.IsNullOrWhiteSpace(nameSpace))
                return asm.GetTypes().Where(x => x.Namespace == nameSpace).Select(x => x.Name);
            else return asm.GetTypes().Select(x => x.Name);
        }

        public static Texture2D LoadTextureRaw(byte[] file)
        {
            if (file.Count() > 0)
            {
                Texture2D Tex2D = new Texture2D(2,2);
                Tex2D.LoadImage(file);
                return Tex2D;
            }
            return null;
        }

        public static Texture2D LoadTextureFromFile(string FilePath)
        {
            if (File.Exists(FilePath))
                return LoadTextureRaw(File.ReadAllBytes(FilePath));

            return LoadTextureRaw((File.ReadAllBytes("ButtSaber.Resources.Sprites.logo_noType.png")));
        }

        public static Texture2D LoadTextureFromResources(string resourcePath)
        {
            return LoadTextureRaw(GetResource(Assembly.GetCallingAssembly(), resourcePath));
        }

        public static Sprite LoadSpriteRaw(byte[] image, float PixelsPerUnit = 100.0f)
        {
            return LoadSpriteFromTexture(LoadTextureRaw(image), PixelsPerUnit);
        }

        public static Sprite LoadSpriteFromTexture(Texture2D SpriteTexture, float PixelsPerUnit = 100.0f)
        {
            if (SpriteTexture)
                return Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);
            return null;
        }

        public static Sprite LoadSpriteFromFile(string FilePath, float PixelsPerUnit = 100.0f)
        {
            return LoadSpriteFromTexture(LoadTextureFromFile(FilePath), PixelsPerUnit);
        }

        public static Sprite LoadSpriteFromResources(string resourcePath, float PixelsPerUnit = 100.0f)
        {
            return LoadSpriteRaw(GetResource(Assembly.GetCallingAssembly(), resourcePath), PixelsPerUnit);
        }

        public static byte[] GetResource(Assembly asm, string ResourceName)
        {
            System.IO.Stream stream;
            byte[] data;
            try
            {
                stream = asm.GetManifestResourceStream(ResourceName);
                data = new byte[stream.Length];
            }
            catch
            {
                stream = asm.GetManifestResourceStream("ButtSaber.Resources.Sprites.logo_noType.png");
                data = new byte[stream.Length];
            }
            stream.Read(data, 0, (int)stream.Length);
            return data;
        }
    }
}
