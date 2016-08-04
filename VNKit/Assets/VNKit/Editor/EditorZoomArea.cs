using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VNKit
{
    /// <summary>
    /// A small class designed to do some simple math on Unity Rects
    /// Code based off of article found at:
    /// http://martinecker.com/martincodes/unity-editor-window-zooming/
    /// 
    /// (Site may be down)
    /// </summary>
    public static class RectExtensions
    {
        /// <summary>
        /// Scales a rect by a given amount around its center point
        /// </summary>
        /// <param name="rect">The given rect</param>
        /// <param name="scale">The scale factor</param>
        /// <returns>The given rect scaled around its center</returns>
        public static Rect ScaleSizeBy(this Rect rect, float scale) { return rect.ScaleSizeBy(scale, rect.center); }

        /// <summary>
        /// Scales a rect by a given amount and around a given point
        /// </summary>
        /// <param name="rect">The rect to size</param>
        /// <param name="scale">The scale factor</param>
        /// <param name="pivotPoint">The point to scale around</param>
        /// <returns>The rect, scaled around the given pivot point</returns>
        public static Rect ScaleSizeBy(this Rect rect, float scale, Vector2 pivotPoint) 
        {
            Rect result = rect;

            //"translate" the top left to something like an origin
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;

            //Scale the rect
            result.xMin *= scale;
            result.yMin *= scale;
            result.xMax *= scale;
            result.yMax *= scale;

            //"translate" the top left back to its original position
            result.x += pivotPoint.x;
            result.y += pivotPoint.y;

            return result;
        }
    }

    /// <summary>
    /// A simple class providing static access to functions that will provide a 
    /// zoomable area similar to Unity's built in BeginVertical and BeginArea
    /// Systems. Code based off of article found at:
    /// http://martinecker.com/martincodes/unity-editor-window-zooming/
    ///  
    /// (Site may be down)
    /// </summary>
    public class EditorZoomArea
    {
        private static Stack<Matrix4x4> previousMatrixes = new Stack<Matrix4x4>();

        public static Rect Begin(float zoomScale, Rect screenCoordsArea)
        {
            GUI.EndGroup();

            Rect clippedArea = screenCoordsArea.ScaleSizeBy(1.0f / zoomScale, screenCoordsArea.center);
            clippedArea.y += 21;

            GUI.BeginGroup(clippedArea);

            previousMatrixes.Push(GUI.matrix);
            Matrix4x4 translation = Matrix4x4.TRS(clippedArea.center, Quaternion.identity, Vector3.one);
            Matrix4x4 scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1.0f));
            GUI.matrix = translation * scale * translation.inverse;

            return clippedArea;
        }

        /// <summary>
        /// Ends the zoom area
        /// </summary>
        public static void End()
        {
            GUI.matrix = previousMatrixes.Pop();
            GUI.EndGroup();
            GUI.BeginGroup(new Rect(0, 21, Screen.width, Screen.height));
        }
    }
}
