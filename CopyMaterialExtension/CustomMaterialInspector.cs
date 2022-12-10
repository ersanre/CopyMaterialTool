using System;
using UnityEditor;
using UnityEngine;

namespace CopyMaterial.Editor
{
    [CustomEditor(typeof(Material))]
    public class CustomMaterialInspector : MaterialEditor
    {
        private Material material;
        private Renderer MeshRenderer;
        private Material[] Materials;
        private static int index;
        private bool IsGameobject;

        public override void OnEnable() //材质球面板第一次绘制才会调用
        {
            base.OnEnable();
            IsGameobject = Selection.activeTransform;
            if (!IsGameobject) return;
            //判断一下是不是选中了project里的材质球
            material = target as Material;
            PaseFavoritePathExtension.stringPath[0] = AssetDatabase.GetAssetPath(material);
            //低版本不能用TryGetComponet
            MeshRenderer = Selection.activeTransform.GetComponent<Renderer>();
            Materials = MeshRenderer.sharedMaterials;
        }


        public override void OnInspectorGUI()
        {
            if (IsGameobject)
            {
                try
                {
                    Materials = MeshRenderer.sharedMaterials; //将材质球 赋值给新的数组
                }
                catch (Exception e)
                {
                    Debug.Log("组件丢失" + e.Message);
                    return;
                }

                for (int i = 0; i < Materials.Length; i++)
                {
                    GUILayout.BeginHorizontal();
                    if (Materials[i] != null) //可能数组里有null
                    {
                        if (material.GetInstanceID() ==
                            MeshRenderer.sharedMaterials[i].GetInstanceID()) //材质球只显示自己的框
                        {
                            Undo.RecordObject(MeshRenderer, "复制材质球并引用"); //设置可撤销
                            Materials[i] = (Material)EditorGUILayout.ObjectField(MeshRenderer.sharedMaterials[i],
                                typeof(Material), true); //材质选取框 可被赋值
                            index = EditorGUILayout.Popup(index, PaseFavoritePathExtension.stringName.ToArray()); //下拉选框
                            if (GUILayout.Button("复制引用")) //复制材质并引用
                            {
                                Materials[i] = Copy.CopyAsset(MeshRenderer.sharedMaterials[i],
                                    PaseFavoritePathExtension.stringPath[index]) as Material;
                            }
                        }
                    }

                    GUILayout.EndHorizontal();
                }

                MeshRenderer.sharedMaterials = Materials; //最后将新的数组赋值回renderer的material数组
            }
            base.OnInspectorGUI();
        }
    }
}