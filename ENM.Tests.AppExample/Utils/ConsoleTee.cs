using System.Text;

namespace EntityNexus.Tests.AppExample.Utils
{
    public static class ConsoleTee
    {
        private static StreamWriter? _logWriter;
        private static TextWriter? _originalOut;

        public static void StartLogging(string logFilePath = null)
        {
            logFilePath ??= $"log_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            _logWriter = new StreamWriter(logFilePath, append: true) { AutoFlush = true };
            _originalOut = Console.Out;
            Console.SetOut(new TeeWriter(_originalOut, _logWriter));
            Console.WriteLine($"[LOG] Logging started to: {logFilePath}");
        }

        public static void StopLogging()
        {
            if (_logWriter != null)
            {
                Console.SetOut(_originalOut);
                _logWriter.Dispose();
                _logWriter = null;
            }
        }

        private class TeeWriter : TextWriter
        {
            private readonly TextWriter[] _outputs;

            public TeeWriter(params TextWriter[] outputs) => _outputs = outputs;
            public override Encoding Encoding => Encoding.UTF8;

            public override void Write(char value)
            {
                foreach (var output in _outputs)
                    output.Write(value);
            }

            public override void Write(string? value)
            {
                foreach (var output in _outputs)
                    output.Write(value);
            }

            public override void WriteLine(string? value)
            {
                foreach (var output in _outputs)
                    output.WriteLine(value);
            }
        }
    }
}