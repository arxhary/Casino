using System.Diagnostics;

namespace Casino.Models
{
    public class Wheel
    {
        public readonly WheelNumber[] WheelNumbers =
   {
        new WheelNumber(0, WheelNumColorEnum.Green),
        new WheelNumber(32, WheelNumColorEnum.Red),
        new WheelNumber(15, WheelNumColorEnum.Black),
        new WheelNumber(19, WheelNumColorEnum.Red),
        new WheelNumber(4, WheelNumColorEnum.Black),
        new WheelNumber(21, WheelNumColorEnum.Red),
        new WheelNumber(2, WheelNumColorEnum.Black),
        new WheelNumber(25, WheelNumColorEnum.Red),
        new WheelNumber(17, WheelNumColorEnum.Black),
        new WheelNumber(34, WheelNumColorEnum.Red),
        new WheelNumber(6, WheelNumColorEnum.Black),
        new WheelNumber(27, WheelNumColorEnum.Red),
        new WheelNumber(13, WheelNumColorEnum.Black),
        new WheelNumber(36, WheelNumColorEnum.Red),
        new WheelNumber(11, WheelNumColorEnum.Black),
        new WheelNumber(30, WheelNumColorEnum.Red),
        new WheelNumber(8, WheelNumColorEnum.Black),
        new WheelNumber(23, WheelNumColorEnum.Red),
        new WheelNumber(10, WheelNumColorEnum.Black),
        new WheelNumber(5, WheelNumColorEnum.Red),
        new WheelNumber(24, WheelNumColorEnum.Black),
        new WheelNumber(16, WheelNumColorEnum.Red),
        new WheelNumber(33, WheelNumColorEnum.Black),
        new WheelNumber(1, WheelNumColorEnum.Red),
        new WheelNumber(20, WheelNumColorEnum.Black),
        new WheelNumber(14, WheelNumColorEnum.Red),
        new WheelNumber(31, WheelNumColorEnum.Black),
        new WheelNumber(9, WheelNumColorEnum.Red),
        new WheelNumber(22, WheelNumColorEnum.Black),
        new WheelNumber(18, WheelNumColorEnum.Red),
        new WheelNumber(29, WheelNumColorEnum.Black),
        new WheelNumber(7, WheelNumColorEnum.Red),
        new WheelNumber(28, WheelNumColorEnum.Black),
        new WheelNumber(12, WheelNumColorEnum.Red),
        new WheelNumber(35, WheelNumColorEnum.Black),
        new WheelNumber(3, WheelNumColorEnum.Red),
        new WheelNumber(26, WheelNumColorEnum.Black)
    };
        public int CurrentNumberIndex { get; protected set; }

        public WheelNumber WinningNumber { get; protected set; }

        public WheelNumColorEnum? Colour { get; set; }

        public bool Running { get; protected set; }

        public event Func<Task> OnStartAsync;

        public event Func<Task> OnFinishAsync;

        public event Func<Task> OnNumberChangedAsync;


        public async Task RollTheBallAsync()
        {
            WinningNumber = null;
            Running = true;

            if (OnStartAsync != null)
            {
                await OnStartAsync.Invoke();
            }

            var running = true;
            var random = new Random();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var lengthOfSpin = new TimeSpan(0, 0, random.Next(5, 15));

            random = new Random();
            var speed = new TimeSpan(random.Next(30000, 40000));

            while (running)
            {
                CurrentNumberIndex += 1;

                if (CurrentNumberIndex > WheelNumbers.GetUpperBound(0))
                {
                    CurrentNumberIndex = 0;
                }
                if (OnNumberChangedAsync != null)
                {
                    await OnNumberChangedAsync.Invoke();
                }

                await Task.Delay(speed);

                if (stopwatch.Elapsed.TotalSeconds > lengthOfSpin.TotalSeconds - 5)
                {
                    random = new Random();
                    speed = new TimeSpan(random.Next(100000, 200000));
                }
                if (stopwatch.Elapsed.TotalSeconds > lengthOfSpin.TotalSeconds - 2)
                {
                    random = new Random();
                    speed = new TimeSpan(random.Next(500000, 700000));
                }
                if (stopwatch.Elapsed.TotalSeconds > lengthOfSpin.TotalSeconds)
                {
                    running = false;
                }
            }

            WinningNumber = WheelNumbers[CurrentNumberIndex];

            Running = false;

            if (OnFinishAsync != null)
            {
                await OnFinishAsync.Invoke();
            }
        }
    }
}
