using UniRx;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Managers {
//	public class CommunityManager : IManager
//	{
//		public Dictionary<string, object> providedStreams { get; private set; }
//		public List<string> requestedStreams { get; private set; }
//		public static string managerKey = "CommunityManager";
//
//		private IDisposable departuresSubscription;
//		private IDisposable romancesSubscription;
//		private IDisposable arrivalsSubscription;
//		private IObservable<List<Character>> communityStream;
//
//		private Dictionary<string, ReactiveDictionary> communityRegistry;
//		private IObservable<Arrival> arrivalsStream;
//		private IObservable<Departure> departuresStream;
//
//		public CommunityManager ()
//		{
//			Debug.Log ("Loaded CommunityManager");
//			this.providedStreams = new Dictionary<string, object> ();
//			this.requestedStreams = new List<string> ();
//
//			this.providedStreams.Add("CommunityStream
//		}
//		
//		public void SubscribeToStreams(Dictionary<string, object> streamsBank) 
//		{
//			this.requestedStreams.ForEach (delegate(string streamKey) {
//				object stream = streamsBank.Keys.Where(key => key.Equals(streamKey)).ToList().FirstOrDefault();
//				
//				if (stream != null) {
//					switch (streamKey) 
//					{
//					case "departures":
//						this.SubscribeToDepartures(stream);
//						break;
//					case "arrivals":
//						this.arrivalsSubscription = this.SubscribeToArrivals(stream);
//					default:
//						break;
//					}
//				}
//			});
//		}
//		
//		private IDisposable SubscribeToDepartures (IObservable<Departure> stream)
//		{
//
//		}
//
//		private IDisposable SubscribeToRomance(IObservable<Romance> stream)
//		{
//
//		}
//
//		private IDisposable SubscribeToArrivals(IObservable<Arrival> stream)
//		{
//			
//			return stream.Subscribe(
//				arrival => {
//					
//				}, 
//				() => {
//					this.DisposeStream(this.arrivalsSubscription);
//				}, 
//				ex => {
//					this.ErroredStream(this.arrivalsSubscription, ex)
//				}
//			);
//		}
//
//		public IObservable CommunityMembers()
//		{
//
//		}
//
//		private void CreateCommunityMemberStream ()
//		{
//			if (this.communityStream == null) {
//				Observable.Create<List<CommunityMemberStream>>(
//					(IObserver<List<CommunityMemberStream>> observer => 
//			 		{
//						return Disposable.create(() => Debug.Log("Observer has unsubscribed from Community"));
//					});
//			 }
//		}
//
//		private void DisposeStream (IDisposable subscription)
//		{
//			subscription.Dispose ();
//		}
//		
//		private void ErroredStream (IDisposable subscription, Exception ex)
//		{
//			subscription.Dispose ();
//		}
//	} 
}