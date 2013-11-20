using System;
using System.Runtime.InteropServices;
using System.Threading;
using Electra.MouseHelpers;

namespace Electra.Executors
{
	public class MouseExecutor
	{
		[DllImport("user32.dll")]
		private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);
		
		[DllImport("user32.dll", EntryPoint = "SetCursorPos")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetCursorPos(int X, int Y);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetCursorPos(out MousePoint lpMousePoint);

		public void SetCursorPosition(int X, int Y)
		{
			SetCursorPos(X, Y);
		}

		public MousePoint GetCursorPosition()
		{
			MousePoint currentMousePoint;
			GetCursorPos(out currentMousePoint);
			return currentMousePoint;
		}

		public void MouseEvent(MouseEventFlags value)
		{
			MouseEvent(value, 0);
		}

		public void MouseEvent(MouseEventFlags value, long data)
		{
			var position = GetCursorPosition();
			mouse_event((uint)value, (uint)position.X, (uint)position.Y, (uint) data, (UIntPtr) 0);
		}

		public void ExecuteClick()
		{
			new Thread(() =>
			{
				MouseEvent(MouseEventFlags.LeftDown);
				Thread.Sleep(100);
				MouseEvent(MouseEventFlags.LeftUp);
			}).Start();
		}

		public void ExecuteDoubleClick()
		{
			new Thread(() =>
			{
				MouseEvent(MouseEventFlags.LeftDown);
				Thread.Sleep(100);
				MouseEvent(MouseEventFlags.LeftUp);
				Thread.Sleep(100);
				MouseEvent(MouseEventFlags.LeftDown);
				Thread.Sleep(100);
				MouseEvent(MouseEventFlags.LeftUp);
			}).Start();
		}
	}
}