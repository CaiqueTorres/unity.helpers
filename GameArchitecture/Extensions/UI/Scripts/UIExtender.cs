#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace homehelp.Extenders
{
    public static class UIExtender
    {
        /// <summary>
        /// Static method that makes the anchors get the same position as the corners
        /// </summary>
        [MenuItem("uGUI/Anchors To Corners")]
        public static void AnchorsToCorners()
        {
            var selectionTransforms = Selection.transforms;
            for (var i = 0; i < selectionTransforms.Length; i++)
            {
                var transform = selectionTransforms[i] as RectTransform;
                Undo.RecordObject((Object) transform, "anchorsToCorners");
                
                var pt = Selection.activeTransform.parent as RectTransform;

                if (transform == null || pt == null) return;

                var rect = pt.rect;

                var newAnchorsMin = new Vector2(transform.anchorMin.x + transform.offsetMin.x / rect.width,
                    transform.anchorMin.y + transform.offsetMin.y / rect.height);
                var newAnchorsMax = new Vector2(transform.anchorMax.x + transform.offsetMax.x / rect.width,
                    transform.anchorMax.y + transform.offsetMax.y / rect.height);

                transform.anchorMin = newAnchorsMin;
                transform.anchorMax = newAnchorsMax;
                transform.offsetMin = transform.offsetMax = new Vector2(0, 0);
            }
        }
        
        /// <summary>
        /// Static method that makes the corners get the same position as the anchors
        /// </summary>
        [MenuItem("uGUI/Corners to Anchors")]
        public static void CornersToAnchors()
        {
            var selectionTransforms = Selection.transforms;
            for (var i = 0; i < selectionTransforms.Length; i++)
            {
                var transform = selectionTransforms[i] as RectTransform;
                Undo.RecordObject((Object) transform, "anchorsToCorners");

                if (transform == null)
                    return;
                transform.offsetMin = transform.offsetMax = new Vector2(0, 0);
            }
        }
    }
}
#endif
