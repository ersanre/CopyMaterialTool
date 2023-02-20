using System.IO;
using System.Collections.Generic;
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

        private static Material TempMaterial;
        private GUIContent mGUIContent;
        private bool IsGood;
        private static string LastPath;

        public override void OnEnable() //材质球面板第一次绘制才会调用
        {
            base.OnEnable();
            if (TempMaterial == null)
            {
                TempMaterial = Resources.Load<Material>("TempCopyMatetalValue");
            }
            IsGood = TempMaterial != null;
            if (!IsGood)
            {
                mGUIContent = new GUIContent(EditorGUIUtility.IconContent("CollabConflict"));
                mGUIContent.tooltip = Language.LanguageString[6];
            }

            IsGameobject = Selection.activeTransform;
            if (!IsGameobject) return;
            //判断一下是不是选中了project里的材质球
            material = target as Material;

            if (material.hideFlags != HideFlags.NotEditable)
            {

                PaseFavoritePathExtension.stringName[0] = Language.LanguageString[2];

                PaseFavoritePathExtension.stringPath[0] = AssetDatabase.GetAssetPath(material);
            }
            else
            {
                LastPath = EditorPrefs.GetString("CopyMaterialKey1");
                if (LastPath != null && LastPath != "")
                {

                    PaseFavoritePathExtension.stringName[0] = Language.LanguageString[4] + "- " + Path.GetFileName(Path.GetDirectoryName(LastPath));

                    PaseFavoritePathExtension.stringPath[0] = LastPath;
                }
            }


            //低版本不能用TryGetComponet
            MeshRenderer = Selection.activeTransform.GetComponent<Renderer>();
            Materials = MeshRenderer.sharedMaterials;

        }


        public override void OnInspectorGUI()
        {

            GUILayout.BeginHorizontal();
            if (!IsGood)
            {
                GUILayout.Label(mGUIContent, GUILayout.Width(20));
            }

            GUI.enabled = IsGood;
            if (GUILayout.Button(Language.LanguageString[0]))
            {
                var tempMateral = target as Material;
                TempMaterial.shader = tempMateral.shader;
                TempMaterial.CopyPropertiesFromMaterial(tempMateral);
            }

            if (GUILayout.Button(Language.LanguageString[1]))
            {
                var tempMateral = target as Material;
                tempMateral.shader = TempMaterial.shader;
                tempMateral.CopyPropertiesFromMaterial(TempMaterial);
            }

            GUILayout.EndHorizontal();
            GUI.enabled = true;
            if (IsGameobject)
            {
                try
                {
                    Materials = MeshRenderer.sharedMaterials; //将材质球 赋值给新的数组
                }
                catch (Exception e)
                {

                }

                for (int i = 0; i < Materials.Length; i++)
                {
                    GUILayout.BeginHorizontal();
                    if (Materials[i] != null) //可能数组里有null
                    {
                        if (material.GetInstanceID() ==
                            MeshRenderer.sharedMaterials[i].GetInstanceID()) //材质球只显示自己的框
                        {
                            Undo.RecordObject(MeshRenderer, "CopyAndUse"); //设置可撤销
                            Materials[i] = (Material)EditorGUILayout.ObjectField(MeshRenderer.sharedMaterials[i],
                                typeof(Material), true, GUILayout.MaxWidth(100)); //材质选取框 可被赋值
                            index = EditorGUILayout.Popup(index, PaseFavoritePathExtension.stringName.ToArray(), GUILayout.MaxWidth(150)); //下拉选框
                            var tempColor = GUI.color;
                            GUI.color = Color.green;
                            if (material.hideFlags != HideFlags.NotEditable)
                            {
                                if (GUILayout.Button(Language.LanguageString[3])) //复制材质并引用
                                {
                                    EditorPrefs.SetString("CopyMaterialKey1", PaseFavoritePathExtension.stringPath[index]);
                                    // LastPath =FolderPaths[index];//最后一次使用路径
                                    Materials[i] = Copy.CopyAsset(MeshRenderer.sharedMaterials[i],
                                       PaseFavoritePathExtension.stringPath[index]) as Material;
                                }
                            }
                            else
                            {
                                if (GUILayout.Button(Language.LanguageString[5])) //不可编辑的默认材质球 复制临时 材质并引用
                                {
                                    EditorPrefs.SetString("CopyMaterialKey1", PaseFavoritePathExtension.stringPath[index]);
                                    Materials[i] = Copy.CopyAsset(TempMaterial,
                                      PaseFavoritePathExtension.stringPath[index]) as Material;
                                }
                            }
                            GUI.color = tempColor;
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