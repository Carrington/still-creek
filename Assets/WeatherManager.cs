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

			});
		}
	}
}

