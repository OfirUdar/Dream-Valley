using UnityEngine;
using Zenject;

namespace Game.DayCycle
{
    public class DayCycleManager : ITickable
    {
        private readonly DayCycleData _dayCycleData;
        private readonly Light _light;
        private readonly Camera _camera;

        private readonly float _timeRateBetweenUpdateColors;
        private float _timer;
        public DayCycleManager(DayCycleData dayCycleData, Light light, Camera camera)
        {
            _dayCycleData = dayCycleData;
            _light = light;
            _camera = camera;

            _timeRateBetweenUpdateColors = dayCycleData.DurationSeconds / 20;
        }

        public void Tick()
        {
            _timer += Time.deltaTime;

            if (_timer >= _dayCycleData.DurationSeconds )
            {
                _timer = 0;
            }

            if (Time.frameCount % 60 == 0)
            {
                var normalizedTimer = _timer / _dayCycleData.DurationSeconds;

                _light.color = _dayCycleData.LightNightGradient.Evaluate(normalizedTimer);
                _camera.backgroundColor = _dayCycleData.CameraNightGradient.Evaluate(normalizedTimer);
            }
        }
    }

}
