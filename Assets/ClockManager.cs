using UniRx;
using UniRx.Operators;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Managers {
	public struct Clock 
	{
		public readonly int hour;
		public readonly int minute;

		public Clock(int hour, int minute) {
			this.hour = hour;
			this.minute = minute;
		}
	}

	//Implement means to dilate/contract time
	public class ClockManager : IManager
	{
		private IObservable<long> ticks;

		public ClockManager (IObservable<long> tickStream)
		{
			Debug.Log ("Loaded ClockManager");

			this.ticks = tickStream;
		}

		public IObservable<Clock> ClockStream () {
			return Observable.Create<Clock> (observer => {
				var minute = 0;
				var hour = 0;

				//TODO get time from save
				//TODO pause and unpause time
				//TODO should the time multiplier and stuff go here?
				var tickSub = this.ticks.Subscribe(_ => {
					minute++;
					if (minute > 59) {
						minute = 0;
						hour++;
					}
					if (hour > 23) {
						hour = 0;
					}

					Clock clock = new Clock(hour, minute);
					Debug.Log(string.Format ("Clock Minute {0}", clock.minute));
					Debug.Log(string.Format ("Clock Hour {0}", clock.hour));
					observer.OnNext(clock);
				},
				observer.OnError,
				observer.OnCompleted);

				return Disposable.Create(() => {
					tickSub.Dispose();
				});
			});
		} 
	}
}