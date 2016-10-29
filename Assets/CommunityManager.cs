using UniRx;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Managers {
	//Implement means to dilate/contract time
	public class CommunityManager : IManager
	{
		public Dictionary<string, object> providedStreams { get; private set; }
		public List<string> requestedStreams { get; private set; }
		public static string managerKey = "CommunityManager";

		private IDisposable departuresSubscription;
		private IDisposable romancesSubscription;
		private IDisposable arrivalsSubscription;

		private Dictionary<string, ReactiveDictionary> communityRegistry;

		public CommunityManager ()
		{
			Debug.Log ("Loaded CommunityManager");
			this.providedStreams = new Dictionary<string, object> ();
			this.requestedStreams = new List<string> ();

			this.communityRegistry = new Dictionary<string, ReactiveDictionary> ();
			this.communityRegistry.Add ("townsfolk", ReactiveDictionary<string, Character>);
			this.communityRegistry.Add ("wilderness", ReactiveDictionary<string, Character>);
		}
		
		public void SubscribeToStreams(Dictionary<string, object> streamsBank) 
		{
			this.requestedStreams.ForEach (delegate(string streamKey) {
				object stream = streamsBank.Keys.Where(key => key.Equals(streamKey)).ToList().FirstOrDefault();
				
				if (stream != null) {
					switch (streamKey) 
					{
					case "deaths":
						this.SubscribeToDepartures(stream);
						break;
					case "romances":
						this.SubscribeToRomance(stream);
						break;
					case "arrivals":
						this.SubscribeToArrivals(stream);
					default:
						break;
					}
				}
			});
		}
		
		private IDisposable SubscribeToDepartures (IObservable<Departure> stream)
		{
			return stream.Subscribe (
				departure => {
					this.communityRegistry[departure.communityLabel].Remove(departure.character.guid);
				},
				exception => {},
				complete => {}
			);
		}

		private IDisposable SubscribeToRomance(IObservable<Romance> stream)
		{

		}

		private IDisposable SubscribeToArrivals(IObservable<Arrival> stream)
		{
			return stream.Subscribe(
				arrival => {
					this.communityRegistry[arrival.communityLabel].Add(arrival.character.guid, arrival.character);
				}, 
				exception => {

				}, 
				complete => {
	
				}
			);
		}
	} 
}