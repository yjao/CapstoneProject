using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TileSprite))]
public class TileSpriteEditor : Editor {

	TileSprite myTarget;

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();


		myTarget = (target as TileSprite);
		Draw();
	}

	private void Draw()
	{
		GUILayout.Label("SPRITE IMAGE: " + (myTarget.sprite != null ? myTarget.sprite.name : "NULL"), EditorStyles.boldLabel);

		myTarget.sprite = (EditorGUILayout.ObjectField(myTarget.sprite,typeof(Sprite),true) as Sprite);
		if (myTarget.sprite == null)
		{
			EditorGUILayout.HelpBox("ERROR: Sprite Image has not been set",MessageType.Error);
			myTarget.CleanUp();
		}
		else
		{
			EditorGUILayout.HelpBox("CLEAR: Sprite image " + myTarget.sprite.name + " has been set",MessageType.Info);
			GUILayout.Label("SPRITE PREVIEW", EditorStyles.boldLabel);
			GUILayout.Box(myTarget.sprite.texture,GUILayout.Width(125), GUILayout.Height(125));
			DrawVariables();
		}


	}

	private void DrawVariables()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label("X Axis:", EditorStyles.boldLabel);
		myTarget.x = EditorGUILayout.IntField(myTarget.x, GUILayout.Width(75));
		GUILayout.Label("Y Axis:", EditorStyles.boldLabel);
		myTarget.y = EditorGUILayout.IntField(myTarget.y, GUILayout.Width(75));
		GUILayout.EndHorizontal();

		GUILayout.Space(30);

		GUILayout.Label("OFFSET VARIABLES");
		EditorGUILayout.HelpBox("Offsets for per sprite positioning",MessageType.Info);
		GUILayout.BeginHorizontal();
		GUILayout.Label("Offset X:", EditorStyles.boldLabel);
		myTarget.offsetX = EditorGUILayout.FloatField(myTarget.offsetX,GUILayout.Width(75));
		GUILayout.Label("Offset Y:", EditorStyles.boldLabel);
		myTarget.offsetY = EditorGUILayout.FloatField(myTarget.offsetY,GUILayout.Width(75));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		myTarget.offsetX = GUILayout.HorizontalSlider(myTarget.offsetX,-1.00000f,1.00000f,GUILayout.Width(100));
		GUILayout.Space(30);
		myTarget.offsetY = GUILayout.HorizontalSlider(myTarget.offsetY,-1.00000f,1.00000f,GUILayout.Width(100));
		GUILayout.EndHorizontal();

		if (GUILayout.Button(new GUIContent("SPAWN")))
		{
			myTarget.CleanUp();
			myTarget.CreateSprites(myTarget.x,myTarget.y);
		}

		if (GUILayout.Button(new GUIContent("CLEAN UP")))
		{
			myTarget.CleanUp();
		}
	}
}
