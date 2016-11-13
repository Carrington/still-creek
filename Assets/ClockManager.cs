using UniRx;
using UniRx.Operators;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Managers {
	public struct Clock {
		public readonly hour;
		public readonly minute;

		public Clock(hour, minute) {
			this.hour = hour;
			this.minute = minute;
		}
	}

	//Implement means to dilate/contract time
	public class ClockManager : IManager
	{
		private IObservable<int> ticks;

		public ClockManager (IObservable<int> tickStream)
		{
			Debug.Log ("Loaded ClockManager");

			this.ticks = tickStream;
		}

		public IConnectableObservable<Clock> ClockStream () {
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
						hours = 0;
					}

					Clock clock = new Clock(hour, minute);
					observer.OnNext(clock);
				},
				observer.OnError,
				observer.OnCompleted);

				return Disposable.Create(() => {
					tickSub.Dispose();
				});
			})
		} 
	}
}