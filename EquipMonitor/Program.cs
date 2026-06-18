using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMonitor
{
    static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [STAThread]
        static void Main()
        {
            AllocConsole();

            // ThreadException 핸들러는 SetUnhandledExceptionMode 보다 먼저 등록해야 함
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += UIThread_Exception;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void UIThread_Exception(object sender, ThreadExceptionEventArgs e)
        {
            LogException("UI 스레드 예외", e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogException("비UI 스레드 예외", e.ExceptionObject as Exception);
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            LogException("Task 미처리 예외", e.Exception);
            e.SetObserved();
        }

        private static void LogException(string errorType, Exception ex)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{errorType}]\r\n" +
                                $"Message: {ex?.Message}\r\n" +
                                $"StackTrace:\r\n{ex?.StackTrace}\r\n" +
                                $"--------------------------------------------------\r\n";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(logMessage);
            Console.ResetColor();

            try
            {
                string logDir = Application.StartupPath + @"\Log\";
                if (!Directory.Exists(logDir))
                    Directory.CreateDirectory(logDir);
                File.AppendAllText(logDir + "crash_log.txt", logMessage);
            }
            catch { }
        }
    }
}
