#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class Docker
{

    #region Reflection Types
    private class _EditorWindow
    {
        private EditorWindow instance;
        private Type type;

        public _EditorWindow( EditorWindow instance ) {
            this.instance = instance;
            type = instance.GetType();
        }

        public object m_Parent {
            get {
                var field = type.GetField( "m_Parent", BindingFlags.Instance | BindingFlags.NonPublic );
                return field.GetValue( instance );
            }
        }
    }

    private class _DockArea
    {
        private object instance;
        private Type type;

        public _DockArea( object instance ) {
            this.instance = instance;
            type = instance.GetType();
        }

        public object window {
            get {
                var property = type.GetProperty( "window", BindingFlags.Instance | BindingFlags.Public );
                return property.GetValue( instance, null );
            }
        }

        public object s_OriginalDragSource {
            set {
                var field = type.GetField( "s_OriginalDragSource", BindingFlags.Static | BindingFlags.NonPublic );
                field.SetValue( null, value );
            }
        }
    }

    private class _ContainerWindow
    {
        private object instance;
        private Type type;

        public _ContainerWindow( object instance ) {
            this.instance = instance;
            type = instance.GetType();
        }


        public object rootSplitView {
            get {
                var property = type.GetProperty( "rootSplitView", BindingFlags.Instance | BindingFlags.Public );
                return property.GetValue( instance, null );
            }
        }
    }

    private class _SplitView
    {
        private object instance;
        private Type type;

        public _SplitView( object instance ) {
            this.instance = instance;
            type = instance.GetType();
        }

        public object DragOver( EditorWindow child, Vector2 screenPoint ) {
            var method = type.GetMethod( "DragOver", BindingFlags.Instance | BindingFlags.Public );
            return method.Invoke( instance, new object[] { child, screenPoint } );
        }

        public void PerformDrop( EditorWindow child, object dropInfo, Vector2 screenPoint ) {
            var method = type.GetMethod( "PerformDrop", BindingFlags.Instance | BindingFlags.Public );
            method.Invoke( instance, new object[] { child, dropInfo, screenPoint } );
        }
    }
    #endregion

    public enum DockPosition
    {
        Left,
        Top,
        Right,
        Bottom
    }

    /// <summary>
    /// Docks the second window to the first window at the given position
    /// </summary>
    public static void Dock( this EditorWindow parentWindow, EditorWindow childWindow, DockPosition position ) {
        var mousePosition = GetFakeMousePosition( parentWindow, position );

        var parent = new _EditorWindow( parentWindow );
        var child = new _EditorWindow( childWindow );
        var dockArea = new _DockArea( parent.m_Parent );
        var containerWindow = new _ContainerWindow( dockArea.window );
        var splitView = new _SplitView( containerWindow.rootSplitView );
        var dropInfo = splitView.DragOver( childWindow, mousePosition );
        dockArea.s_OriginalDragSource = child.m_Parent;
        splitView.PerformDrop( childWindow, dropInfo, mousePosition );
    }

    private static Vector2 GetFakeMousePosition( EditorWindow parentWindow, DockPosition position ) {
        Vector2 mousePosition = Vector2.zero;

        // The 20 is required to make the docking work.
        // Smaller values might not work when faking the mouse position.
        switch ( position ) {
            case DockPosition.Left:
                mousePosition = new Vector2( 20, parentWindow.position.size.y / 2 );
                break;
            case DockPosition.Top:
                mousePosition = new Vector2( parentWindow.position.size.x / 2, 20 );
                break;
            case DockPosition.Right:
                mousePosition = new Vector2( parentWindow.position.size.x - 20, parentWindow.position.size.y / 2 );
                break;
            case DockPosition.Bottom:
                mousePosition = new Vector2( parentWindow.position.size.x / 2, parentWindow.position.size.y - 20 );
                break;
        }

        return GUIUtility.GUIToScreenPoint( mousePosition );
    }
}
#endif