using UnityEngine;
using System.Diagnostics;

namespace __RSUnityFramework__.Core.Utilities
{
    public class RSGizmos
    {
        private const string CONDITIONAL_KEY = "RS_GIZMOS";

        // Color constants
        public static readonly Color COLOR_RED = Color.red;
        public static readonly Color COLOR_GREEN = Color.green;
        public static readonly Color COLOR_BLUE = Color.blue;
        public static readonly Color COLOR_YELLOW = Color.yellow;
        public static readonly Color COLOR_CYAN = Color.cyan;
        public static readonly Color COLOR_MAGENTA = Color.magenta;
        public static readonly Color COLOR_WHITE = Color.white;
        public static readonly Color COLOR_BLACK = Color.black;
        public static readonly Color COLOR_ORANGE = new Color(1f, 0.5f, 0f);
        public static readonly Color COLOR_PURPLE = new Color(0.5f, 0f, 1f);

#region Basic Shapes

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawWireSphere(Vector3 center, float radius, Color? color = null)
        {
            Gizmos.color = color ?? COLOR_GREEN;
            Gizmos.DrawWireSphere(center, radius);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawSphere(Vector3 center, float radius, Color? color = null)
        {
            Gizmos.color = color ?? COLOR_GREEN;
            Gizmos.DrawSphere(center, radius);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawWireCube(Vector3 center, Vector3 size, Color? color = null)
        {
            Gizmos.color = color ?? COLOR_BLUE;
            Gizmos.DrawWireCube(center, size);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawCube(Vector3 center, Vector3 size, Color? color = null)
        {
            Gizmos.color = color ?? COLOR_BLUE;
            Gizmos.DrawCube(center, size);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawLine(Vector3 from, Vector3 to, Color? color = null)
        {
            Gizmos.color = color ?? COLOR_WHITE;
            Gizmos.DrawLine(from, to);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawRay(Vector3 origin, Vector3 direction, Color? color = null)
        {
            Gizmos.color = color ?? COLOR_YELLOW;
            Gizmos.DrawRay(origin, direction);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawRay(Ray ray, Color? color = null, float length = 1f)
        {
            Gizmos.color = color ?? COLOR_YELLOW;
            Gizmos.DrawRay(ray.origin, ray.direction * length);
        }

#endregion

#region Advanced Shapes

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawWireCircle(Vector3 center, float radius, Vector3 normal, Color? color = null,
            int segments = 32)
        {
            Gizmos.color = color ?? COLOR_GREEN;

            Vector3 forward = Vector3.Slerp(normal, -normal, 0.5f);
            if (forward == Vector3.zero) forward = Vector3.up;

            Quaternion rot = Quaternion.LookRotation(forward, normal);

            Vector3 prevPoint = center + rot * new Vector3(radius, 0, 0);

            for (int i = 1; i <= segments; i++)
            {
                float angle = (float)i / segments * Mathf.PI * 2f;
                Vector3 newPoint = center + rot * new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
                Gizmos.DrawLine(prevPoint, newPoint);
                prevPoint = newPoint;
            }
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawCircle(Vector3 center, float radius, Color? color = null, int segments = 32)
        {
            DrawWireCircle(center, radius, Vector3.up, color, segments);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawWireCapsule(Vector3 point1, Vector3 point2, float radius, Color? color = null)
        {
            Gizmos.color = color ?? COLOR_CYAN;

            Vector3 forward = point2 - point1;
            Quaternion rot = Quaternion.LookRotation(forward);
            float height = forward.magnitude;

            // Draw spheres at ends
            DrawWireSphere(point1, radius, color);
            DrawWireSphere(point2, radius, color);

            // Draw connecting lines
            Vector3 right = rot * Vector3.right * radius;
            Vector3 up = rot * Vector3.up * radius;

            Gizmos.DrawLine(point1 + right, point2 + right);
            Gizmos.DrawLine(point1 - right, point2 - right);
            Gizmos.DrawLine(point1 + up, point2 + up);
            Gizmos.DrawLine(point1 - up, point2 - up);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawArrow(Vector3 from, Vector3 to, Color? color = null, float arrowHeadLength = 0.3f,
            float arrowHeadAngle = 20f)
        {
            Gizmos.color = color ?? COLOR_YELLOW;
            Gizmos.DrawLine(from, to);

            Vector3 direction = (to - from).normalized;
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) *
                            Vector3.forward;
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) *
                           Vector3.forward;

            Gizmos.DrawLine(to, to + right * arrowHeadLength);
            Gizmos.DrawLine(to, to + left * arrowHeadLength);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawCross(Vector3 position, float size = 0.5f, Color? color = null)
        {
            Gizmos.color = color ?? COLOR_RED;
            float halfSize = size * 0.5f;

            Gizmos.DrawLine(position + Vector3.right * halfSize, position - Vector3.right * halfSize);
            Gizmos.DrawLine(position + Vector3.up * halfSize, position - Vector3.up * halfSize);
            Gizmos.DrawLine(position + Vector3.forward * halfSize, position - Vector3.forward * halfSize);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawBounds(Bounds bounds, Color? color = null)
        {
            DrawWireCube(bounds.center, bounds.size, color);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawPath(Vector3[] points, Color? color = null, bool closed = false)
        {
            if (points == null || points.Length < 2) return;

            Gizmos.color = color ?? COLOR_WHITE;

            for (int i = 0; i < points.Length - 1; i++)
            {
                Gizmos.DrawLine(points[i], points[i + 1]);
            }

            if (closed && points.Length > 2)
            {
                Gizmos.DrawLine(points[points.Length - 1], points[0]);
            }
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawGrid(Vector3 center, int rows, int columns, float cellSize, Color? color = null)
        {
            Gizmos.color = color ?? new Color(1, 1, 1, 0.3f);

            float width = columns * cellSize;
            float height = rows * cellSize;

            Vector3 startPos = center - new Vector3(width / 2f, 0, height / 2f);

            // Draw rows
            for (int i = 0; i <= rows; i++)
            {
                Vector3 rowStart = startPos + new Vector3(0, 0, i * cellSize);
                Vector3 rowEnd = rowStart + new Vector3(width, 0, 0);
                Gizmos.DrawLine(rowStart, rowEnd);
            }

            // Draw columns
            for (int i = 0; i <= columns; i++)
            {
                Vector3 colStart = startPos + new Vector3(i * cellSize, 0, 0);
                Vector3 colEnd = colStart + new Vector3(0, 0, height);
                Gizmos.DrawLine(colStart, colEnd);
            }
        }

#endregion

#region Transform Helpers

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawTransform(Transform transform, float size = 1f)
        {
            if (transform == null) return;

            Vector3 pos = transform.position;

            // X axis - Red
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pos, pos + transform.right * size);

            // Y axis - Green
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pos, pos + transform.up * size);

            // Z axis - Blue
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(pos, pos + transform.forward * size);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawWireBox(Transform transform, Vector3 size, Color? color = null)
        {
            Gizmos.color = color ?? COLOR_BLUE;
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, size);
            Gizmos.matrix = oldMatrix;
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawBox(Transform transform, Vector3 size, Color? color = null)
        {
            Gizmos.color = color ?? COLOR_BLUE;
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, size);
            Gizmos.matrix = oldMatrix;
        }

#endregion

#region Collider Helpers

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawCollider(Collider collider, Color? color = null)
        {
            if (collider == null) return;

            if (collider is BoxCollider box)
            {
                Matrix4x4 oldMatrix = Gizmos.matrix;
                Gizmos.matrix = collider.transform.localToWorldMatrix;
                DrawWireCube(box.center, box.size, color);
                Gizmos.matrix = oldMatrix;
            }
            else if (collider is SphereCollider sphere)
            {
                DrawWireSphere(sphere.transform.TransformPoint(sphere.center),
                    sphere.radius * Mathf.Max(sphere.transform.lossyScale.x, sphere.transform.lossyScale.y,
                        sphere.transform.lossyScale.z),
                    color);
            }
            else if (collider is CapsuleCollider capsule)
            {
                // Simplified capsule drawing
                Vector3 center = collider.transform.TransformPoint(capsule.center);
                float radius = capsule.radius *
                               Mathf.Max(collider.transform.lossyScale.x, collider.transform.lossyScale.z);
                float height = capsule.height * collider.transform.lossyScale.y;

                Vector3 offset = Vector3.up * (height * 0.5f - radius);
                DrawWireCapsule(center - offset, center + offset, radius, color);
            }
        }

#endregion

#region Label Helpers

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawLabel(Vector3 position, string text, Color? color = null)
        {
#if UNITY_EDITOR
            UnityEditor.Handles.color = color ?? COLOR_WHITE;
            UnityEditor.Handles.Label(position, text);
#endif
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawLabelWithBackground(Vector3 position, string text, Color? textColor = null,
            Color? backgroundColor = null)
        {
#if UNITY_EDITOR
            GUIStyle style = new GUIStyle();
            style.normal.textColor = textColor ?? COLOR_WHITE;
            style.normal.background = MakeTex(2, 2, backgroundColor ?? new Color(0, 0, 0, 0.5f));
            style.padding = new RectOffset(5, 5, 2, 2);

            UnityEditor.Handles.Label(position, text, style);
#endif
        }

#if UNITY_EDITOR
        private static Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
#endif

#endregion

#region Physics Helpers

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawRaycastHit(RaycastHit hit, float normalLength = 1f, Color? hitColor = null,
            Color? normalColor = null)
        {
            DrawSphere(hit.point, 0.1f, hitColor ?? COLOR_RED);
            DrawArrow(hit.point, hit.point + hit.normal * normalLength, normalColor ?? COLOR_YELLOW);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawPhysicsRay(Vector3 origin, Vector3 direction, float distance, bool hit,
            Color? color = null)
        {
            Color rayColor = color ?? (hit ? COLOR_RED : COLOR_GREEN);
            DrawRay(origin, direction * distance, rayColor);
        }

#endregion

#region Frustum & Camera

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawFrustum(Vector3 center, float fov, float maxRange, float minRange, float aspect,
            Color? color = null)
        {
            Gizmos.color = color ?? COLOR_YELLOW;
            Gizmos.DrawFrustum(center, fov, maxRange, minRange, aspect);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawCameraFrustum(Camera camera, Color? color = null)
        {
            if (camera == null) return;

            DrawFrustum(camera.transform.position,
                camera.fieldOfView,
                camera.farClipPlane,
                camera.nearClipPlane,
                camera.aspect,
                color);
        }

#endregion

#region Utility

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawIcon(Vector3 position, string iconName)
        {
            Gizmos.DrawIcon(position, iconName, true);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void SetColor(Color color)
        {
            Gizmos.color = color;
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void SetMatrix(Matrix4x4 matrix)
        {
            Gizmos.matrix = matrix;
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void ResetMatrix()
        {
            Gizmos.matrix = Matrix4x4.identity;
        }

#endregion

#region Distance & Measurement

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawDistance(Vector3 from, Vector3 to, Color? color = null)
        {
            DrawLine(from, to, color ?? COLOR_WHITE);
            Vector3 midPoint = (from + to) * 0.5f;
            float distance = Vector3.Distance(from, to);
            DrawLabel(midPoint, $"{distance:F2}m", color);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void DrawAngle(Vector3 origin, Vector3 direction1, Vector3 direction2, float radius = 1f,
            Color? color = null)
        {
            Gizmos.color = color ?? COLOR_CYAN;

            DrawRay(origin, direction1.normalized * radius, color);
            DrawRay(origin, direction2.normalized * radius, color);

            float angle = Vector3.Angle(direction1, direction2);
            DrawLabel(origin + (direction1.normalized + direction2.normalized).normalized * radius * 0.5f,
                $"{angle:F1}°", color);
        }

#endregion
    }
}