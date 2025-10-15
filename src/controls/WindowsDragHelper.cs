using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WaifuAI.Controls
{
    public static partial class WindowDragHelper
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 2;

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool ReleaseCapture();
        [LibraryImport("user32.dll")] private static partial IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        public static void MakeDraggable(Control dragSurface, Form? targetForm = null, bool onlyLeftButton = true)
        {
            if (dragSurface == null) return;
            targetForm ??= dragSurface.FindForm();
            if (targetForm == null) return;

            dragSurface.MouseDown += (s, e) =>
            {
                if (!dragSurface.Enabled) return;
                if (onlyLeftButton && e.Button != MouseButtons.Left) return;

                // Avoid starting drag when interacting inside certain input controls (optional)
                if (s is Control c && c is TextBoxBase) return;

                ReleaseCapture();
                SendMessage(targetForm.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            };
        }
    }
}