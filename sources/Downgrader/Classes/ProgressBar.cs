using System;
using System.Text;
using System.Threading;

public class ProgressBar : IDisposable, IProgress<double>
{
	const int blockCount = 10;
	readonly TimeSpan animationInterval = TimeSpan.FromSeconds(1.0 / 8);
	const string animation = @"|/-\";
	readonly Timer timer;
	bool workdo = true;
	string work;
	double currentProgress = 0;
	string currentText = string.Empty;
	bool disposed = false;
	int animationIndex = 0;
	public ProgressBar() { timer = new Timer(TimerHandler); if (!Console.IsOutputRedirected){ ResetTimer(); } }
	public void Report(double value) { value = Math.Max(0, Math.Min(1, value)); Interlocked.Exchange(ref currentProgress, value); }
	public void DoText(string work1) { work = work1; }
	public void DoThis(bool pbd) { workdo = pbd; }
	void TimerHandler(object state)
	{
		lock (timer)
		{
			if (workdo == true)
			{
				if (disposed) return;
				int progressBlockCount = (int)(currentProgress * blockCount);
				int percent = (int)(currentProgress * 100);
				string text = string.Format(work + ": [{0}{1}] {2,3}% {3}", new string('#', progressBlockCount), new string('-', blockCount - progressBlockCount), percent, animation[animationIndex++ % animation.Length]);
				UpdateText(text);
				ResetTimer();
			}
		}
	}
	void UpdateText(string text)
	{
		int commonPrefixLength = 0;
		int commonLength = Math.Min(currentText.Length, text.Length);
		while (commonPrefixLength < commonLength && text[commonPrefixLength] == currentText[commonPrefixLength]) { commonPrefixLength++; }
		StringBuilder outputBuilder = new StringBuilder();
		outputBuilder.Append('\b', currentText.Length - commonPrefixLength);
		outputBuilder.Append(text.Substring(commonPrefixLength));
		int overlapCount = currentText.Length - text.Length;
		if (overlapCount > 0)
		{
			outputBuilder.Append(' ', overlapCount);
			outputBuilder.Append('\b', overlapCount);
		}
		Console.ForegroundColor = ConsoleColor.Blue;
		Console.Write(outputBuilder);
		currentText = text;
	}
	void ResetTimer() { timer.Change(animationInterval, TimeSpan.FromMilliseconds(-1)); }
	public void Dispose()
	{
		lock (timer)
		{
			disposed = true;
			UpdateText(string.Empty);
		}
	}
}