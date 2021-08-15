using UnityEngine;

public class SingleBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	public static T instance {
		get {

			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(T)) as T;
			}

			if (_instance == null)
			{
				GameObject o = new GameObject("SingleBehaviour<" + typeof(T).ToString() + ">");
				_instance = o.AddComponent<T>();
			}

			return _instance;
		}
	}
}