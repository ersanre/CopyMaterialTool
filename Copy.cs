using System.Collections.Generic;
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
        /// <param name="materials">objecxt</param>
        /// <param name="path">全路径，</param>
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
    public static class Language
    {
        public static List<string> Chinese = new List<string>{
            "复制属性","粘贴属性","*此材质球路径","复制并引用","*上次路径","复制临时材质","缺失原始材质,请在脚本所在文件夹中的Resources文件夹创建一个名字叫TempCopyMatetalValue的材质球"
        };
        public static List<string> English = new List<string>{
            "CopyValue","PasteValue","*This Material Path","Copy and use","*LastPath","Copy Temp Material","The original material is missing, please create a material ball named TempCopyMatetalValue in the Resources folder in the folder where the script is located"
        };
        public static List<string> LanguageString = new List<string>();
        [InitializeOnLoadMethod]
        public static void GetLanguageString()
        {
            LanguageString = IsChinese ? Chinese : English;
        }

        public static bool IsChinese//是繁体中文或者简体中文返回true
        {
            get
            {
                if (IsChineseTW() || IsChineseSimple())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //当前操作系统是否为简体中文
        public static bool IsChineseSimple()
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN";
        }

        //当前操作系统是否为繁体中文
        public static bool IsChineseTW()
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.Name == "Zh-TW";
        }
    }
}