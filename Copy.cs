using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CopyMaterial.Editor
{
    public static class Copy
    {

        /// <summary>
        /// 将Asset资产复制保存到指定路径
        /// </summary>
        /// <param name="obj">资产</param>
        /// <param name="path">Asset路径，</param>
        /// <returns>New objecxt</returns>
        public static Object CopyAsset(Object obj, string path)
        {
            if (!Path.HasExtension(path))
            {
                path = path + '/' + Path.GetFileName(AssetDatabase.GetAssetPath(obj));
            }
            Object mObj = Object.Instantiate(obj);
            // path = PathExtension.GetOnlyPath(path);
            path = AssetDatabase.GenerateUniqueAssetPath(path);//获取唯一路径
            AssetDatabase.CreateAsset(mObj, path);
            return AssetDatabase.LoadAssetAtPath<Object>(path);
        }
    }
}