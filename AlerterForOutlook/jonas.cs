using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using jonas;



namespace jonas

{

    /// <summary>
    /// This is a collection of utilities 
    /// </summary>
    /// <remarks>
    /// Use this class for logging, tracing and process management
    /// </remarks>

    public static class process_util
    {

        /// <summary>
        /// check if the OS is LINUX
        /// </summary>

        public static bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        /// <summary>
        /// execute an application and wait for application to complete
        /// </summary>
        /// <param name="assembly_name">name of assembly</param>
        /// <param name="command_line">command line parameters</param>
        /// <returns></returns>


        public static int process_exec(string assembly_name, string command_line)
        {
            int rc = 0;

            try
            {

                Process theApplication;
                theApplication = new Process();
                theApplication.StartInfo.FileName = assembly_name;
                theApplication.StartInfo.Arguments = command_line;
                theApplication.StartInfo.UseShellExecute = false;
                theApplication.StartInfo.CreateNoWindow = false;
                //theApplication.StartInfo.RedirectStandardOutput = true;
                //theApplication.StartInfo.RedirectStandardError = true;
                //theApplication.StartInfo.RedirectStandardInput = true;
                theApplication.Start();
                theApplication.WaitForExit();
                rc = theApplication.Id;
            }
            catch (Exception ex)
            {
                rc = -1;
            }

            return (rc);
        }

        /// <summary>
        /// execute an application
        /// </summary>
        /// <param name="assembly_name">name of assembly</param>
        /// <param name="command_line">command line parameters</param>
        /// <returns></returns>


        public static int process_create(string assembly_name, string command_line)

        {
            int rc = 0;

            try
            {

                Process theApplication;
                theApplication = new Process();
                theApplication.StartInfo.FileName = assembly_name;
                theApplication.StartInfo.Arguments = command_line;
                theApplication.StartInfo.UseShellExecute = false;
                //theApplication.StartInfo.CreateNoWindow = true;
                //theApplication.StartInfo.RedirectStandardOutput = true;
                //theApplication.StartInfo.RedirectStandardError = true;
                //theApplication.StartInfo.RedirectStandardInput = true;
                theApplication.Start();
                rc = theApplication.Id;
            }
            catch (Exception ex)
            {
                rc = -1;
            }

            return (rc);
        }

        /// <summary>
        /// kill all processes by a given name
        /// </summary>
        /// <param name="name">name of process</param>
        /// <returns></returns>
        /// <remarks>
        /// <para> Normally, process name is the name of the executable without extension .EXE </para>
        /// <para> Example : process_kill("Excel") kills all Excel Processes </para>
        /// </remarks>

        public static int process_kill(string name)
        {
            int rc = 0;

            System.Threading.Thread.Sleep(300);

            Process[] processlist = Process.GetProcesses();

            foreach (Process theprocess in processlist)
            {

                Console.WriteLine(" ProcessName : {0} ", theprocess.ProcessName);
            }

            foreach (Process theprocess in processlist)
            {

                Console.WriteLine(" ProcessName : {0} ", theprocess.ProcessName);
                if (theprocess.ProcessName == name)
                {
                    Console.WriteLine("Kill existing process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
                    theprocess.Kill();
                }
            }
            return (rc);
        }

        /// <summary>
        /// kill a process by a given process ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public static int process_kill(int id)
        {
            int rc = 0;

            System.Threading.Thread.Sleep(300);

            Process[] processlist = Process.GetProcesses();

            foreach (Process theprocess in processlist)
            {

                if (theprocess.Id == id)
                {
                    Console.WriteLine("Kill existing process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
                    theprocess.Kill();
                }
            }
            return (rc);
        }


        /// <summary>
        /// count all processes by a given name
        /// </summary>
        /// <param name="name">name of process</param>
        /// <returns>number of processes running</returns>
        /// <remarks>
        /// Normally, process name is the name of the executable without extension .EXE
        /// </remarks>

         public static int process_count(string name)
        {
            int rc = 0;

            System.Threading.Thread.Sleep(300);

            Process[] processlist = Process.GetProcesses();

            foreach (Process theprocess in processlist)
            {

                if (theprocess.ProcessName == name)
                {
                    Console.WriteLine("Found existing process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
                    rc++;
                }
            }
            return (rc);
        }

         /// <summary>
         /// get a commandline argument at a given index
         /// </summary>
         /// <param name="index"></param>
         /// <returns></returns>

         public static string get_argument(int index, string[] args)
         {
             string value = "";

             if (args.Length > index)
             {
                 value = args[index];
             }
             return (value);
         }

         /// <summary>
         /// search a specific argument in the commandline
         /// </summary>
         /// <param name="argument"></param>
         /// <returns>true is argument exists</returns>

         public static bool check_argument(string argument, string[] args)
         {
             bool found = false;

             foreach (string a in args)
             {
                 if (a == argument) found = true;
             }

             return (found);
         }


        /// <summary>
        /// convert a commandline style string to an array of arguments
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>

         public static string[] stringToArgs(string line)
         {
             string[] arg;

             line = "assembly_path " + line;

             int k = 0;
             k = line.Length;
             bool quoted = false;

             // example "d:\labview projects\excalibur.exe" user="donald duck" update

             char[] charline = line.ToCharArray();

             // replace quoted whitespace by % 

             for (k = 0; k < charline.Length; k++)
             {
                 if (charline[k] == '"') quoted = !quoted;

                 if ((charline[k] == ' ') && (quoted == true))
                 {
                     charline[k] = '%';
                 }

             }

             string s = new string(charline);

             // split using remaining whitespaces

             s = s.TrimStart(' ');
             arg = s.Split(' ');

             // replace % by whitespace

             for (k = 0; k < arg.Length; k++)
             {
                 arg[k] = arg[k].Replace('%', ' ');
             }
             return (arg);

         }



    }

    /// <summary>
    /// write trace and log information to UPD port. 
    /// </summary>

    public static class TraceWriter
    {
        private static bool init_done = false;
        private static UdpClient client = new UdpClient();
        private static IPEndPoint remoteEndPoint = new IPEndPoint(0,0);

        /// <summary>
        /// Initialisation of trace writer
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">UDP port number</param>
        /// <param name="appl_id">name of your application</param>

        public static void init(string address, int port, string appl_id)
        {

            if (init_done == false)
            {
                remoteEndPoint.Port = port;
                remoteEndPoint.Address = (IPAddress.Parse(address));
                init_done = true;
            }
        }

        /// <summary>
        /// send text via UPD port for SW tracing and logging
        /// </summary>
        /// <param name="text"></param>

        public static void write(string text)
        {
            if ( String.IsNullOrEmpty(text) == false && init_done == true)
            {
                //byte[] data = Encoding.UTF8.GetBytes(text);
				
                //if (remoteEndPoint.Port != 0)
                //{
                //    client.Send(data, data.Length, remoteEndPoint);
                //}
            }

        }

        /// <summary>
        /// write text inluding timestamp and application name
        /// </summary>
        /// <param name="text"></param>
        /// <param name="AppName">name of application</param>

        public static void writeline(string AppName, string text)
        {
            if (String.IsNullOrEmpty(text) == false && init_done == true)
            {
                text = "[" + DateTime.Now.ToString() + "]" + AppName + text;
                write(text);

            }

        }

    }

    /// <summary>
    /// write trace and log information to UPD port. Destintion is 127.0.0.1
    /// </summary>

    public class UdpTraceWriter
    {
        private static bool init_done = false;
        private static UdpClient client = new UdpClient();
        private static IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Broadcast, 0);


        public UdpTraceWriter(string ip_address, int udp_port)

        {
            remoteEndPoint.Port = udp_port;
            remoteEndPoint.Address = (IPAddress.Parse(ip_address));
            init_done = true;
        }


        /// <summary>
        /// send text via UPD port for SW tracing and logging
        /// </summary>
        /// <param name="text"></param>

        public void write(string text)
        {
            try
            {
                if (String.IsNullOrEmpty(text) == false && init_done == true)
                {
                    byte[] data = Encoding.UTF8.GetBytes(text);
                    client.Send(data, data.Length, remoteEndPoint);
                }
            }
            catch (Exception ex)
            {

            }


        }

        /// <summary>
        /// write text 
        /// /// </summary>
        /// <param name="text"></param>

        public void writeline(string text)
        {
            if (String.IsNullOrEmpty(text) == false && init_done == true)
            {
                write(text);
            }
        }

    }


    /// <summary>
    /// provides logging to file
    /// </summary>

    public static class logger
    {

        private static string log_file_name = "";

        private static StreamWriter re;


        /// <summary>
        /// Set log file name
        /// </summary>
        /// <param name="file_name">name of log file</param>

        public static void init(string file_name, bool delete_old_logfile)
        {
            if (log_file_name == "")
            {
                log_file_name = file_name;


                if (delete_old_logfile == true & File.Exists(log_file_name))
                {
                    File.Delete(log_file_name);
                }

            }
        }

        /// <summary>
        /// Change log file name.
        /// </summary>
        /// <param name="file_name">name of log file</param>

        public static void change_filename (string file_name)
        {
                log_file_name = file_name;

                //if (File.Exists(log_file_name))
                //{
                //    File.Delete(log_file_name);
                //}

        }


        /// <summary>
        /// writeln : write text to log file
        /// </summary>
        /// <param name="text">text to be written</param>
        /// <remarks>
        /// <para>Open log file, append text to file, flush and closes log file. </para>
        /// <para> </para>
        /// </remarks>

        public static void writeline(string AppName, string text)
        {
            DateTime time;
            time = DateTime.Now;
			//Console.WriteLine(text);			
            TraceWriter.writeline(AppName,text);
            
            try
            {
                if (String.IsNullOrEmpty(text) == false)
                {

                    if (File.Exists(log_file_name))
                    {
                        re = new StreamWriter(log_file_name, append: true);
                    }
                    else
                    {
                        re = new StreamWriter(log_file_name);
                    }


                    if (re != null)
                    {

                        Console.WriteLine("[" + time.ToShortDateString() + " " + time.ToShortTimeString() + "] " + AppName + " " + text);
                        re.WriteLine("[" + time.ToShortDateString() + " " + time.ToShortTimeString() + "] " + AppName + " " + text);
                        re.Flush();
                        re.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                TraceWriter.write(" cannot write to log file" + ex.Message);
            }
        }


    }

    /// <summary>
    /// read elements from XML file
    /// </summary>

    public class my_xml_reader
    {

        public void my_parse_all(string xml_file)
        {

            XElement xe = XElement.Load(xml_file);

            foreach (XElement a in xe.Elements())
            {
                Console.WriteLine(a.Name.LocalName);

                if (xe.HasElements)
                {
                    my_parse(a);
                }
            }

        }

        public void my_parse(XElement xin)
        {
            foreach (XElement b in xin.Elements())
            {
                Console.WriteLine(b.Name.LocalName + " " + b.Value);
            }
        }


        /// <summary>
        /// get subelement from XML file. example: xepath = "input_parameter/traceport"
        /// </summary>
        /// <param name="xml_file"></param>
        /// <param name="xepath"></param>
        /// <returns></returns>

        public string get_xml_element(string xml_file, string xepath)
        {
            try
            {
                string[] names = xepath.Split('/');
                string result = "undefined";

                XElement xe = XElement.Load(xml_file);

                result = xe.Element(names[0]).Element(names[1]).Value;
                Console.WriteLine(xepath + ":" + result);

                return (result);
            }
            catch (Exception ex)
            {
                return (ex.Message + "XML File=" + xml_file + " XEPath =" + xepath);
            }

        }

    } // class 

}
