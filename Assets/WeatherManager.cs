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

		private readonly IConnectableObservable<Calendar> calendar;

		public WeatherManager (IConnectableObservable<Caendar> calendar)
		{
			Debug.Log ("Loaded WeatherManager");

			this.calendar = calendar;
		}

		public IConnectableObservable<Weather> WeatherStream () {
			return Observable.Create<Weather> (observer => {
				//TODO weather should be configurable
				//TODO weather should be more erratic based on how you're doing against the Forgotten One
				//TODO weather should be more harsh with higher difficulty
				var calendarSub = this.calendar.Subscribe(date => {
					WeatherPattern weather = 0;
					HeatPattern heat = 3;
					var rnd = new Random();
					var weatherRoll = rnd.Next(1, 100);

					//TODO all seasons after Spring!
					if (date.Month == 1) {
						if (weatherRoll < 20) {
							weather = 4;
						} else if (weatherRoll < 40) {
							weather = 3;
						} else if (weatherRoll < 60) {
							weather = 2;
						} else if  (weatherRoll < 80) {
							weather = 1;
						} else if (weatherRoll < 100) {
							weather = 0;
						}
					}

					observer.OnNext(new Weather(weather, heat));
				},
				observer.OnError,
				observer.OnCompleted);

				return Disposable.Create(() => {
					calendarSub.Dispose();
				});
			});
		}
	}
}

