using UnityEngine;

namespace MatchTile.Utils
{
    public abstract class SingletonBase<T> : MonoBehaviour where T : class
    {
        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType(typeof(T)) as T;
                    if (instance == null)
                        Debug.LogError("SingletonBase<T>: Could not found GameObject of type " + typeof(T).Name);
                }
                return instance;
            }
        }
    }
}
