﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BioAnimation))]
public class BioAnimation_Editor : Editor {

		public BioAnimation Target;

		void Awake() {
			Target = (BioAnimation)target;
		}

		public override void OnInspectorGUI() {
			Undo.RecordObject(Target, Target.name);

			//DrawDefaultInspector();

			using(new EditorGUILayout.VerticalScope ("Box")) {
				if(GUILayout.Button("Controller")) {
					Target.Controller.Inspect = !Target.Controller.Inspect;
				}

				if(Target.Controller.Inspect) {
					using(new EditorGUILayout.VerticalScope ("Box")) {
						Target.Controller.MoveForward = (KeyCode)EditorGUILayout.EnumPopup("Move Forward", Target.Controller.MoveForward);
						Target.Controller.MoveBackward = (KeyCode)EditorGUILayout.EnumPopup("Move Backward", Target.Controller.MoveBackward);
						Target.Controller.MoveLeft = (KeyCode)EditorGUILayout.EnumPopup("Move Left", Target.Controller.MoveLeft);
						Target.Controller.MoveRight = (KeyCode)EditorGUILayout.EnumPopup("Move Right", Target.Controller.MoveRight);
						Target.Controller.TurnLeft = (KeyCode)EditorGUILayout.EnumPopup("Turn Left", Target.Controller.TurnLeft);
						Target.Controller.TurnRight = (KeyCode)EditorGUILayout.EnumPopup("Turn Right", Target.Controller.TurnRight);
					}
				}
			}

			using(new EditorGUILayout.VerticalScope ("Box")) {
				if(GUILayout.Button("Character")) {
					Target.Character.Inspect = !Target.Character.Inspect;
				}

				if(Target.Character.Inspect) {
					using(new EditorGUILayout.VerticalScope ("Box")) {
						EditorGUILayout.LabelField("Joints");

						for(int i=0; i<Target.Character.Joints.Length; i++) {
							using(new EditorGUILayout.VerticalScope ("Box")) {
								EditorGUILayout.BeginHorizontal();
								EditorGUILayout.LabelField((i+1).ToString(), GUILayout.Width(20));
								Target.Character.Joints[i].Transform = (Transform)EditorGUILayout.ObjectField(Target.Character.Joints[i].Transform, typeof(Transform), true);
								EditorGUILayout.EndHorizontal();
							}
						}
						
						if(GUILayout.Button("+")) {
							Target.Character.AddJoint(Target.Character.Joints.Length);
						}
						if(GUILayout.Button("-")) {
							Target.Character.RemoveJoint(Target.Character.Joints.Length);
						}
					}
				}
			}

			using(new EditorGUILayout.VerticalScope ("Box")) {
				if(GUILayout.Button("Trajectory")) {
					Target.Trajectory.Inspect = !Target.Trajectory.Inspect;
				}

				if(Target.Trajectory.Inspect) {
					using(new EditorGUILayout.VerticalScope ("Box")) {
						Target.Trajectory.Size = EditorGUILayout.IntField("Size", Target.Trajectory.Size);
						Target.Trajectory.Width = EditorGUILayout.FloatField("Width", Target.Trajectory.Width);
					}
				}
			}

			using(new EditorGUILayout.VerticalScope ("Box")) {
				if(GUILayout.Button("PFNN")) {
					Target.PFNN.Inspect = !Target.PFNN.Inspect;
				}

				if(Target.PFNN.Inspect) {
					using(new EditorGUILayout.VerticalScope ("Box")) {
						Target.PFNN.XDim = EditorGUILayout.IntField("XDim", Target.PFNN.XDim);
						Target.PFNN.YDim = EditorGUILayout.IntField("YDim", Target.PFNN.YDim);
						Target.PFNN.HDim = EditorGUILayout.IntField("HDim", Target.PFNN.HDim);
						EditorGUILayout.BeginHorizontal();
						if(GUILayout.Button("Load Parameters")) {
							Target.PFNN.LoadParameters();
						}
						Target.PFNN.Parameters = (NetworkParameters)EditorGUILayout.ObjectField(Target.PFNN.Parameters, typeof(NetworkParameters), true);
						EditorGUILayout.EndHorizontal();
					}
				}
			}

			if(GUI.changed) {
				EditorUtility.SetDirty(Target);
			}
		}

		void OnSceneGUI() {
			
		}

		private void DrawSphere(Vector3 position, float radius, Color color) {
			Handles.color = color;
			Handles.SphereHandleCap(0, position, Quaternion.identity, radius, EventType.Repaint);
		}

		private void DrawCube(Vector3 position, Quaternion rotation, float size, Color color) {
			Handles.color = color;
			Handles.CubeHandleCap(0, position, rotation, size, EventType.Repaint);
		}

		private void DrawLine(Vector3 a, Vector3 b, float width, Color color) {
			Handles.color = color;
			Handles.DrawAAPolyLine(width, new Vector3[2] {a,b});
		}

		private void DrawDottedLine(Vector3 a, Vector3 b, float width, Color color) {
			Handles.color = color;
			Handles.DrawDottedLine(a, b, width);
		}

		private void DrawArrow(Vector3 position, Quaternion rotation, float length, Color color) {
			Handles.color = color;
			Handles.ArrowHandleCap(0, position, rotation, length, EventType.repaint);
		}

		private void DrawSolidArc(Vector3 position, Vector3 normal, Vector3 from, float angle, float radius, Color color) {
			Handles.color = color;
			Handles.DrawSolidArc(position, normal, from, angle, radius);
		}

		private void SetGUIColor(Color color) {
			GUI.backgroundColor = color;
		}
}