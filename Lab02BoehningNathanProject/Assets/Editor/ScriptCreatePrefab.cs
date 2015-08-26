﻿using UnityEngine;
using System.Collections;
using UnityEditor;

public class ScriptCreatePrefab : MonoBehaviour {

	[MenuItem("Project Tools/Create Prefab")]
	public static void CreatePrefab()
	{
		GameObject[] selectedObjects = Selection.gameObjects;

		foreach(GameObject curObject in selectedObjects)
		{
			string name = curObject.name;
			string assetPath = "Assets/" + name + ".prefab";

			if(AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)))
			{
				//Debug.Log ("Asset exists!");

				if(EditorUtility.DisplayDialog("Caution", "Prefab already exists. " + 
				                               "Do you want to overwrite?",
				                               "Yes", "No"))
				{
					CreateNew(curObject, assetPath);
				}
			}
			else
			{
				CreateNew(curObject, assetPath);
			}
		}
	}

	public static void CreateNew(GameObject obj, string location)
	{
		Object prefab = PrefabUtility.CreateEmptyPrefab(location);
		PrefabUtility.ReplacePrefab(obj, prefab);
		AssetDatabase.Refresh();

		DestroyImmediate(obj);
		GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
	}
}