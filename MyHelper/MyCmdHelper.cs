using System.Diagnostics;

namespace MyHelper4Web
{
    /// <summary>
    /// 调用cmd类
    /// </summary>
    public class MyCmdHelper
    {
        /// <summary>
        /// 调用cmd控制台的方法
        /// </summary>
        /// <param name="cmdArgs">输入字符串（dos命令）</param>
        /// <returns>返回cmd屏显结果</returns>
        public static string InvokeCmd(string cmdArgs)
        {
            string Tstr = "";
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            p.StandardInput.WriteLine(cmdArgs);
            p.StandardInput.WriteLine("exit");
            Tstr = p.StandardOutput.ReadToEnd();
            p.Close();
            return Tstr;
        }

    }
}