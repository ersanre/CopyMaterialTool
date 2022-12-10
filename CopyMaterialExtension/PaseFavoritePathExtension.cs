using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace CopyMaterial.Editor
{
    public static class PaseFavoritePathExtension
    {
        public static List<string> stringName = new List<string>();
        public static List<string> stringPath = new List<string>();

        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                     @"\Unity\Editor-5.x\Preferences\SearchFilters";

        [InitializeOnLoadMethod]
        public static void ReadYAML()
        {
            string[] s = File.ReadAllLines(path);
            stringName.Clear(); //先清一下
            stringPath.Clear();
            PaseYAML(s, ref stringName, ref stringPath); //解析文件 把名字 和路径放入表里
            stringName.Insert(0, "此材质文件夹"); //在最前面占位  
            stringPath.Insert(0, "Asset");     
        }

        static void PaseYAML(string[] text, ref List<string> stringName, ref List<string> stringPath)
        {
            //int s = 0;
            for (int i = 3; i < text.Length; i++) //从3行开始，
            {
                if (text[i].StartsWith("  - m_Name"))
                {
                    stringName.Add(text[i].Substring(12)); //前面固定长度所以可以这样去掉没用的
                
                    for (int j = i+1; j < text.Length; j++)
                    {
                        
                        if ((text[j].StartsWith("      m_ClassNames")&& !text[j].Contains("[]"))||text[j].StartsWith("      m_Folders")&&text[j].Contains("[]"))
                        {
                            stringName.RemoveAt(stringName.Count - 1);//如果不是真实的收藏路径就把名字表去掉 
                            i = j;
                            break;
 
                        }
                        if (text[j].StartsWith("      - Assets"))
                        {
                            var str = text[j].Substring(8);
                            if (Directory.Exists(str))
                            {
                                stringPath.Add(text[j].Substring(8));
                                
                            }
                            else
                            {
                                stringName.RemoveAt(stringName.Count - 1);
                            }
                            i = j;
                            break;
                        } 
                    }
                    
                }
            }
        }
    }
}