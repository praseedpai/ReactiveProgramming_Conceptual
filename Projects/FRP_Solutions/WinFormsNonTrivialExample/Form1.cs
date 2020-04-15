using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace WinFormsNonTrivialExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            Ticker();
            WordCount();
        }

        void Ticker()
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .ObserveOn(System.Reactive.Concurrency.Scheduler.Default)
                .Subscribe(x => textBox1.Text = DateTime.Now.ToLongTimeString());
        }

        void WordCount()
        {
            var textChanged = Observable.FromEventPattern
                <EventHandler, EventArgs>(
                handler => handler.Invoke,
                h => textBox3.TextChanged += h,
                h => textBox3.TextChanged -= h);

            textChanged
                .ObserveOn(System.Reactive.Concurrency.Scheduler.Default)
                .Subscribe(x => textBox2.Text =
                    textBox3.Text
                    .Split()
                    .DefaultIfEmpty()
                    .Count(word => !string.IsNullOrWhiteSpace(word))
                    .ToString());
        }
    }
}
