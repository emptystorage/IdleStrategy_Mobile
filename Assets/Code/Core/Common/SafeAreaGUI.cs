using UnityEngine;

namespace Code.Core.Common
{
    public abstract class SafeAreaGUI : MonoBehaviour
    {
        private void Awake()
        {
            var rectTransform = GetComponent<RectTransform>();
            var minAnchor = Screen.safeArea.position;
            var maxAnchor = minAnchor + Screen.safeArea.size;
            minAnchor.x /= Screen.width;
            minAnchor.y /= Screen.height;
            maxAnchor.x /= Screen.width;
            maxAnchor.y /= Screen.height;
            rectTransform.anchorMin = minAnchor;
            rectTransform.anchorMax = maxAnchor;
        }
    }
}
