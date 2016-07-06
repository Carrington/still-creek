using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public interface IManager
{
	Dictionary<string, object> providedStreams { get; }
	List<string> requestedStreams { get; }
}