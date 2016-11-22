using UniRx;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Managers {
	//Implement means to dilate/contract time
	public class TimeManager : IManager
	{
		public FloatReactiveProperty frameToMinuteConversion { get; set; }

		//TODO most of the below is fucked and out of date. This should just be pushing ticks to clock and other observers that care.
		public TimeManager (float conversionFactor)
		{
			Debug.Log ("Loaded TimeManager");

			this.frameToMinuteConversion = new FloatReactiveProperty(conversionFactor);
		}

		public IObservable<long> TickStream ()
		{
			return Observable.IntervalFrame (20);
		}
	}
}