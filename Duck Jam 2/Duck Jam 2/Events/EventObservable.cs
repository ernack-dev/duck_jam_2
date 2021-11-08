using System;
using System.Collections;

namespace Duck_Jam_2
{
	public class EventObservable
	{
		private ArrayList observers;

		public EventObservable()
		{
			this.observers = new ArrayList();
		}

		public void AddObserver(EventObserver observer)
        {
			this.observers.Add(observer);
        }

		public void Notify(Event my_event)
        {
			foreach (EventObserver observer in this.observers)
            {
				observer.OnEvent(my_event);
            }
        }
	}
}