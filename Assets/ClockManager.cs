using UniRx;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Managers {
	//Implement means to dilate/contract time
	public class ClockManager : IManager
	{
		public Dictionary<string, object> providedStreams { get; private set; }
		public List<string> requestedStreams { get; private set; }
		public static string managerKey = "ClockManager";
		public IntReactiveProperty minutes { get; private set; }
		public IntReactiveProperty hours { get; private set; }
		
		public ClockManager ()
		{
			Debug.Log ("Loaded ClockManager");
			this.providedStreams = new Dictionary<string, object> ();
			this.requestedStreams = new List<string> ();

			this.requestedStreams.Add ("minutes");

			this.minutes.Value = 0;
			this.hours.Value = 0;
		}

		public void SubscribeToStreams(Dictionary<string, object> streamsBank) 
		{
			this.requestedStreams.ForEach (delegate(string streamKey) {
				object stream = streamsBank.Keys.Where(key => key.Equals(streamKey)).ToList().FirstOrDefault();

				if (stream != null) {
					switch (streamKey) 
					{
						case "minutes":
							this.SubscribeToMinutes((IntReactiveProperty) stream);
							break;
						default:
							break;
					}
				}
			});
		}

		private IDisposable SubscribeToMinutes (IntReactiveProperty minutesStream)
		{
			return minutesStream.Subscribe(minute => {
				this.minutes.Value = minute;
				if (this.minutes.Value >= 60) {
					this.minutes.Value = 0;
					this.hours.Value += 1;
					if (this.hours.Value >= 24) {
						this.hours.Value = 0;
					}
				}
			});
		}
	}
}