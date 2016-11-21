using UnityEngine;
using UniRx;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Managers {

	public class SystemManager : MonoBehaviour {
		public TimeManager timeManager { get; private set; }
		public ClockManager clockManager { get; private set; }
		public CalendarManager calendarManager { get; private set; }
		public WeatherManager weatherManager { get; private set; }

		void Awake() 
		{
			DontDestroyOnLoad(transform.gameObject);
		}

		void Start() 
		{
			float timeConversionFactor = 2.0f;

			this.timeManager = new TimeManager(timeConversionFactor);

			this.clockManager = new ClockManager(this.timeManager.currentMinute);

			var clockConnectable = this.clockManager.ClockStream.Publish();

			this.calendarManager = new CalendarManager(clockConnectable);

			var calendarConnectable = this.calendarManager.CalendarStream.Publish();

			this.weatherManager = new WeatherManager(calendarConnectable);

			var weatherConnectable = this.weatherManager.WeatherStream.Publish();

			//create communtiy stream for town (tick, clock, calendar)
				//community stream reads community manifest for town
				//community stream instantiates character streams for town (tick, character file location)
					//characters instantiate schedules (clock), action (tick, schedule), tolerances (), outlooks (), party profile ()
						//schedule runs
						//action receives tick and schedule and reduces to current action
				//community stream informs characters about other characters
					//characters instatiate relationships (other character)
			//create community manager for wilderness/other locales (tick) repeat above
			//create interaction manager (communities)
			//create political manager (town communtiy) expansion
			//create plot manager (communities, interactions)
				//plot manager determines volatile roles and informs those characters to mutate
			//create map manager
				//populate tiles
			//create input manager
				//input into combat informs plot manager, informs combat manager
				//input into conversation 
				//input to plant
				//input to water/maintain?
				//input to build
				//input to purchase
				//input to pilot
				//input to menu
			
			clockConnectable.Connect();
			calendarConnectable.Connect();
			weatherConnectable.Connect();
		}
	}
 
}