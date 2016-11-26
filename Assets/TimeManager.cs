using UniRx;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Managers {
	//Implement means to dilate/contract time
	public class TimeManager : IManager
	{
		public FloatReactiveProperty frameToMinuteConversion { get; set; }

		public TimeManager (float conversionFactor)
		{
			Debug.Log ("Loaded TimeManager");
			//TODO I don't think we need this anymore. Evaluate!
			this.frameToMinuteConversion = new FloatReactiveProperty(conversionFactor);
		}

		public IObservable<long> TickStream ()
		{
			return Observable.IntervalFrame (5);
		}
	}
}