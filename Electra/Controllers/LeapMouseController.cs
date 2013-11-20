using Leap;
using Electra.Events;
using Electra.Executors;

namespace Electra.Controllers
{
	public class LeapMouseController
	{
		private readonly LeapListener leapListener; 
		private readonly LeapCustomController customController;
		private readonly MouseExecutor mouseFacade;

		public float CursorPositionX { get; set; }
		public float CursorPositionY { get; set; }

		public float VelocityResponse { get; set; }  
		public float VelocityPressition { get; set; }

		public LeapMouseController(LeapListener leapListener, LeapCustomController customController, MouseExecutor mouseFacade)
		{
			this.leapListener = leapListener;
			this.customController = customController;
			this.mouseFacade = mouseFacade;
		}

		public void Init( )
		{ 
			leapListener.FrameEvent += LeapListener_FrameEvent;

			VelocityResponse = 0.5f;
			VelocityPressition = 0.01f;

			customController.AddListener(leapListener);
		}

		public void Finalize()
		{
			customController.RemoveListener(leapListener);
			customController.Dispose();
		}


		private void LeapListener_FrameEvent(Controller controller)
		{
			if (!customController.HasRightIndexFinger) return;

			var rightHand = customController.RightHand;
			var rightFinger = customController.RightIndexFinger;
			var screen = customController.Screen;

			var intersection = screen.Intersect(rightFinger, true);

			switch (rightHand.Pointables.Count)
			{
				case 1:
					CalculateRealScreenPosition(screen, intersection);
					break;
				case 5:
					CalulateSlowScreenPosition(rightFinger);
					break;
			}
		}
		
		private void CalulateSlowScreenPosition(Pointable rightFinger)
		{
			CursorPositionX += rightFinger.TipVelocity.x * VelocityPressition;
			CursorPositionX += (1 - rightFinger.TipVelocity.y) * VelocityPressition;

			mouseFacade.SetCursorPosition((int)CursorPositionX, (int)CursorPositionY);
		}

		private void CalculateRealScreenPosition(Screen screen, Vector intersection)
		{
			CursorPositionX += (intersection.x * screen.WidthPixels - CursorPositionX) * VelocityResponse;
			CursorPositionY += ((1 - intersection.y) * screen.HeightPixels - CursorPositionY) * VelocityResponse;

			mouseFacade.SetCursorPosition((int)CursorPositionX, (int)CursorPositionY);
		}
	}
}
