using System;
using System.IO;
using UnityEditor;

namespace CopyMaterial.Editor
{
    class FileChange
    {
        //监控配置文件是否有改动
        [InitializeOnLoadMethod]
        static void FileChangeer()
        {
            var watcher =
                new FileSystemWatcher(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Unity\Editor-5.x\Preferences");//必须是文件夹 不然有的机器会有问题
            watcher.BeginInit();
            watcher.IncludeSubdirectories = false;
            watcher.EnableRaisingEvents = true;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += OnChange;
            watcher.EndInit();
        }

        private static void OnChange(object sender, FileSystemEventArgs e)
        {
            PaseFavoritePathExtension.ReadYAML();
        }
    }


}