using System.IO;
using UnityEditor;
using UnityEngine;

namespace CopyMaterial.Editor
{
    public static class Copy
    {

        /// <summary>
        /// 将object复制保存到指定路径
        /// </summary>
        /// <param name="materials"></param>objecxt
        /// <param name="path"></param>全路径，
        /// <returns></returns>New objecxt
        public static Object CopyAsset(Object obj,string path) 
        {
            if (!Path.HasExtension(path))
            {
                path= path+'/'+Path.GetFileName(AssetDatabase.GetAssetPath(obj));
            }
            Object mObj =Object.Instantiate(obj);
           // path = PathExtension.GetOnlyPath(path);
            path= AssetDatabase.GenerateUniqueAssetPath(path);//获取唯一路径
            AssetDatabase.CreateAsset(mObj, path);
            return AssetDatabase.LoadAssetAtPath<Object>(path);
        }
    }
}