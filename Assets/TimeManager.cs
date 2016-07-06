using UniRx;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Managers {
	//Implement means to dilate/contract time
	public class TimeManager : IManager
	{
		public FloatReactiveProperty frameToMinuteConversion { get; set; }
		private IConnectableObservable<long> frameStream;
		private IntReactiveProperty currentMinute;
		private IntReactiveProperty currentDay;
		public static string managerKey = "TimeManager";
		public Dictionary<string, object> providedStreams { get; private set; }
		public List<string> requestedStreams { get; private set; }
		private IDisposable internalFrameStreamSubscription;


		public TimeManager (float conversionFactor)
		{
			Debug.Log ("Loaded TimeManager");

			this.frameToMinuteConversion = new FloatReactiveProperty(conversionFactor);

			this.currentMinute = new IntReactiveProperty (0);
			this.currentDay = new IntReactiveProperty (0);

			//TODO set multipler as either constant or settable property
			//TODO I hate putting raw lambdas in the subscribe call, but figuring out how to make an Action delegate provide precisely what Rx is looking for is hard.
			this.frameStream = Observable.IntervalFrame ((int)Math.Round ((10 * this.frameToMinuteConversion.Value), 0)).Publish ();

			this.internalFrameStreamSubscription = this.frameStream.Subscribe(
				x => {
					Debug.Log (string.Format ("Frame Update {0}", x));
					this.currentMinute.Value++;
					if (this.currentMinute.Value >= 1440) {
						this.currentMinute.Value = 0;
						this.currentDay.Value++;
					}
					Debug.Log (string.Format("Current Minute {0}", this.currentMinute.Value));
					Debug.Log (string.Format("Current Day {0}", this.currentDay.Value));
				}); 

			this.requestedStreams = new List<string> ();
			this.providedStreams = new Dictionary<string, object> ();

			this.providedStreams.Add ("frames", (object) this.frameStream);
			this.providedStreams.Add ("minutes", (object) this.currentMinute);
			this.providedStreams.Add ("days", (object) this.currentDay);

			this.Start (); //TODO temporary
		}

		public IDisposable Start ()
		{
			return this.frameStream.Connect ();
		}

		public void Stop (IDisposable connection)
		{
			connection.Dispose ();
		}
	}
}