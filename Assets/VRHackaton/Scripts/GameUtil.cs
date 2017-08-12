using System.Linq;
using System.Reflection;
using UnityEngine;

public class GameUtil : MonoBehaviour
{
	private static GameUtil _instance;

	public static GameUtil Instance
	{
		get
		{
			if (!_instance)
			{
				_instance = FindObjectOfType<GameUtil>();
			}
			return _instance;
		}
		private set
		{
			if (_instance)
			{
				Debug.Log(DebugLogText(new System.Diagnostics.StackFrame(),
				                       "warning: multiple VREditorUtiil in scene, " +
				                       "please delte one"));
			}
			else
			{
				_instance = value;
			}
		}
	}

	public SteamVR_PlayArea Playarea;

	public MirrorDudeController
		ControllerLeft, ControllerRight;

	public Transform HeadTransform;

	private void Awake()
	{
		Instance = this;
		if (!Playarea || !ControllerLeft || !ControllerRight)
		{
			Debug.LogError(DebugLogText(new System.Diagnostics.StackFrame(),
			                       "not all values are defined"));
		}
	}

	private static string DebugLogText(string className, string methodName, string message)
	{
		return string.Format("{0} | {1} : \n\t{2} ", className, methodName, message);
	}

	public static string DebugLogText(System.Diagnostics.StackTrace st, string message)
	{
		return DebugLogText(st.GetFrame(0), message);
	}

	public static string DebugLogText(System.Diagnostics.StackFrame sf, string message)
	{
		MethodBase method = sf.GetMethod();
		string fullMethod = method.Name + "(";
		fullMethod = method.GetParameters().Aggregate(
			fullMethod, (current, param) =>
				current + (param.ParameterType.Name + " " + param.Name + ","));
		fullMethod = fullMethod.TrimEnd(',') + ")";
		string methodname = method.ReflectedType != null
			                    ? method.ReflectedType.Name
			                    : "[Method unknown]";
		return DebugLogText(methodname, fullMethod, message);
	}

}
