using System;
using UnityEngine;
using UniRx;
using System.IO;

namespace Managers
{

	public enum WeatherPattern
	{
		Clear, Overcast, LightRain, HeavyRain, Thunderstorm, Hurricane, LightSnow, HeavySnow, Blizzard
	}

	public enum HeatPattern
	{
		Freezing, Cold, Cool, Temperate, Warm, Hot, Searing
	}

	public struct Weather
	{
		private WeatherPattern weatherPattern;
		private HeatPattern heatPattern;

		public Weather (WeatherPattern wp, HeatPattern hp)
		{
			this.weatherPattern = wp;
			this.heatPattern = hp;
		}
	}

	public class WeatherManager
	{

		private readonly IConnectableObservable<EngineDate> dates;

		public WeatherManager (IConnectableObservable<EngineDate> dates)
		{
			Debug.Log ("Loaded WeatherManager");

			this.dates = dates;
		}

		public IObservable<Weather> WeatherStream () {
			return Observable.Create<Weather> (observer => {
				//TODO weather should be configurable
				//TODO weather should be more erratic based on how you're doing against the Forgotten One
				//TODO weather should be more harsh with higher difficulty
				var datesSub = this.dates.Subscribe(date => {
					WeatherPattern weather = WeatherPattern.Clear;
					HeatPattern heat = HeatPattern.Temperate;
					var rnd = new System.Random();
					var weatherRoll = rnd.Next(1, 100);

					//TODO all seasons after Spring!
					if (date.Month == 1) {
						if (weatherRoll < 20) {
							weather = WeatherPattern.HeavyRain;
						} else if (weatherRoll < 40) {
							weather = WeatherPattern.Thunderstorm;
						} else if (weatherRoll < 60) {
							weather = WeatherPattern.LightRain;
						} else if  (weatherRoll < 80) {
							weather = WeatherPattern.Overcast;
						} else if (weatherRoll < 100) {
							weather = WeatherPattern.Clear;
						}
					}

					Debug.Log(String.Format("Weather is doing {0}", weather));
					Debug.Log(String.Format("Hot is doing {0}", heat));

					observer.OnNext(new Weather(weather, heat));
				},
				observer.OnError,
				observer.OnCompleted);

				return Disposable.Create(() => {
					datesSub.Dispose();
				});
			});
		}
	}
}

