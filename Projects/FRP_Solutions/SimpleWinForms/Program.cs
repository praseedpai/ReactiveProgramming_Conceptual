using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Linq;


using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace SimpleWinForms
{
    class Program
    {
        static void Main()
        {
            var evenNumbers = Observable.Range(0, 10)
.Where(i => i % 2 == 0)
.Subscribe(Console.WriteLine);

            var result =
from i in Observable.Range(1, 100)
from j in Observable.Range(1, 100)
from k in Observable.Range(1, 100)
where k * k == i * i + j * j
select new { a = i, b = j, c = k };
            // A Subscriber with
            // A callback (Lambda) which prints value,
            // A callback for Exception
            // A callback for Completion
            IDisposable subscription = result.Subscribe(
            x => Console.WriteLine("OnNext: {0}", x),
            ex => Console.WriteLine("OnError: {0}", ex.Message),
            () => Console.WriteLine("OnCompleted"));
            Console.Read();
            var mylabel = new Label();
            var myform = new Form { Controls = { mylabel } };

            IObservable<EventPattern<MouseEventArgs>> mousemove =
                Observable.
                FromEventPattern<MouseEventArgs>(myform, "MouseMove");

            mousemove.Subscribe(
                (evt) => { mylabel.Text = evt.EventArgs.X.ToString(); },
                () => { });
            Application.Run(myform);

        }
    }
}
