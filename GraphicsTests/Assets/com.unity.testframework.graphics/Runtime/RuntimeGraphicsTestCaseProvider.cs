using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnityEngine.TestTools.Graphics
{
    internal class RuntimeGraphicsTestCaseProvider : IGraphicsTestCaseProvider
    {
        public ColorSpace ColorSpace
        {
            get
            {
                return QualitySettings.activeColorSpace;
            }
        }

        public RuntimePlatform Platform
        {
            get
            {
                return Application.platform;
            }
        }

        public GraphicsDeviceType GraphicsDevice
        {
            get
            {
                return SystemInfo.graphicsDeviceType;
            }
        }

        public IEnumerable<GraphicsTestCase> GetTestCases()
        {
            AssetBundle referenceImagesBundle = null;
            string[] scenePaths;

            // need the ToLower here for Unix path case sensitivity (android, linux, OSX, iOS)
            var bundleName = string.Format("referenceimages-{0}-{1}-{2}", ColorSpace, Platform, GraphicsDevice).ToLower();

            var referenceImagesBundlePath = Path.Combine(Application.streamingAssetsPath, bundleName);

            try
            {
                referenceImagesBundle = AssetBundle.LoadFromFile(referenceImagesBundlePath);
            }
            catch (NullReferenceException)
            {
                Debug.Log("Refrence Asset bundle does not exist at " + referenceImagesBundlePath);
            }

            if (Platform == RuntimePlatform.Android)
            {
                if (referenceImagesBundle == null)
                    Debug.Log("reference image asset bundle not found!");

                using (var webRequest = new WWW(Application.streamingAssetsPath + "/SceneList.txt"))
                {
                    while (!webRequest.isDone)
                    {
                        // wait for download
                    }
                    scenePaths = webRequest.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
                }
            }
            else
            {
                scenePaths = File.ReadAllLines(Application.streamingAssetsPath + "/SceneList.txt");
            }

            foreach (var scenePath in scenePaths)
            {
                var imagePath = Path.GetFileNameWithoutExtension(scenePath);

                Texture2D referenceImage = null;

                // The bundle might not exist if there are no reference images for this configuration yet
                if (referenceImagesBundle != null)
                {
                    referenceImage = referenceImagesBundle.LoadAsset<Texture2D>(imagePath);
                }

                yield return new GraphicsTestCase(scenePath, referenceImage);
            }
        }
    }
}
