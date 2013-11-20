using System.Linq;
using Leap;

namespace Electra.Controllers
{
	public class LeapCustomController : Controller
	{
		public Hand RightHand
		{
			get
			{
				var frame = this.Frame();
				return frame.Hands.FirstOrDefault() ?? Hand.Invalid;
			}
		}

		public bool HasRightHand
		{
			get
			{
				var frame = this.Frame();
				return frame.Hands.FirstOrDefault() != null;
			}
		}

		public Pointable RightIndexFinger
		{
			get
			{
				var finger = RightHand.Fingers.FirstOrDefault();
				return HasRightIndexFinger ? finger : Pointable.Invalid;
			}
		}

		public bool HasRightIndexFinger
		{
			get
			{
				return HasRightHand && RightHand.Fingers.FirstOrDefault() != null;
			}
		}

		public bool HasScreen
		{
			get
			{
				return HasRightHand && HasRightIndexFinger && CalibratedScreens.ClosestScreenHit(RightIndexFinger) != null ;
			}
		}

		public Screen Screen
		{
			get
			{
				var screen =  CalibratedScreens.ClosestScreenHit(RightIndexFinger) ;
				return HasScreen ? screen : Screen.Invalid;
			}
		}
	}
}
