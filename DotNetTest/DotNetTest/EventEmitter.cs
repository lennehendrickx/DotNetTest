using System;

namespace DotNetTest
{
    public class EventEmitter
    {
        public const string SomethingInterestingHappened = "something interesting happened";
        public event Action<EventEmitter, string> Changed;

        //the above definition of Changed is equivalent to this:
        //public delegate void Callback(EventEmitter sender, string message);
        //public event Callback Changed;

        public void DoSomethingThatEmitsEvent()
        {
            Changed?.Invoke(this, SomethingInterestingHappened);
        }
    }
}