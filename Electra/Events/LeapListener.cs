using System;
using Leap;

namespace Electra.Events
{
	public class LeapListener : Listener
	{
		public event Action<Controller> FrameEvent = controller => { };
		public event Action<Controller> InitEvent = controller => { };
		
		public override void OnInit(Controller controller)
		{
			InitEvent(controller);
		}

		public override void OnFrame(Controller controller)
		{
			FrameEvent(controller);
		}
	}
	 
}

