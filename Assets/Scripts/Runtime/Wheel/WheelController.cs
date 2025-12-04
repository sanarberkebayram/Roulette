using Runtime.Wheel.UI;
using Zenject;

namespace Runtime.Wheel
{
    public class WheelController
    {
        private readonly WheelUI _wheelUI;

        public WheelController(WheelUI wheelUI)
        {
            _wheelUI = wheelUI;
        }
    }
}