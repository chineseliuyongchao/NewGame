using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    public static class EditorUtils
    {
#if UNITY_EDITOR
        //debug使用
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            SceneManager.LoadScene(0);
        }
#endif
    }
}