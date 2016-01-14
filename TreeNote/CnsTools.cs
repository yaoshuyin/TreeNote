using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;
using Gtk;

namespace CnsTools
{
	public class CnsString
	{
		public static string md5 (string s)
		{
			MD5 objMD5 = MD5.Create ();

			byte[] bytes = objMD5.ComputeHash (Encoding.UTF8.GetBytes (s));

			StringBuilder sb = new StringBuilder ();

			for (int i = 0; i < bytes.Length; i++) {
				sb.Append (bytes [i].ToString ("x2"));
			}
			return sb.ToString ();
		}

		public static bool Win7OrLater ()
		{
			if (Environment.OSVersion.Version.Major >= 6) {
				return true;
			}

			return false;
		}

		public static List<String> SplitText (String s)
		{
			List<String> ls = new List<String> ();

			Regex re = new Regex (@"([^.。?!；？！”]*[.。?!；？！”])", RegexOptions.Multiline);
			MatchCollection ms = re.Matches (s);

			String tmp = "";
			if (ms.Count > 0) {
				foreach (Match match in ms) {
					if (tmp.Length >= 50) {
						ls.Add (tmp);
						tmp = "";
					}

					tmp += match.Value;
				}

				if (tmp.Length > 0)
					ls.Add (tmp);
			} else {
				ls.Add (s);
			}
			return ls;
		}

		public static List<String> getSubString (String s, Char[] sep)
		{
			String[] words = s.Split (sep);
			//s.Substring(0,50)
			List<String> sl = new List<String> ();
			String tmps = "";

			for (int i = 0; i < words.Length; i++) {
				if (i == 0 && words.Length == 1) {
					sl.Add (words [i]);
				} else if (i < words.Length - 1) {
					if (tmps.Length == 0)
						tmps = words [i];
					else if (tmps.Length < 50)
						tmps += words [i];
					else {
						sl.Add (tmps);
						tmps = words [i];
					}
				} else {
					sl.Add (tmps + words [i]);
				}
			}
			return sl;
		}

		public static String getSubString (String s, int len)
		{
			return s.Substring (0, s.Length >= len ? len : s.Length);
		}

		public static String CleanString (String s, int len)
		{
			return s.Substring (0, s.Length >= len ? len : s.Length)
				.Replace (",", "")
				.Replace (".", "")
				.Replace (" ", "")
				.Replace ("。", "")
				.Replace ("?", "")
				.Replace ("!", "")
				.Replace ("，", "")
				.Replace ("（", "")
				.Replace ("）", "")
				.Replace ("(", "")
				.Replace (")", "")
				.Replace ("-", "")
				.Replace ("_", "")
				.Replace ("作者", "-")
				.Replace (":", "")
				.Replace ("：", "")
				.Replace ("\"", "")
				.Replace ("'", "")
				.Replace ("<", "")
				.Replace (">", "")
				.Replace ("\r\n", "。")
				.Replace ("\n", "。")
				.Replace (" ", "。")
				.Replace ("`", " ")
				.Replace ("“", " ")
				;
		}

		public static String CleanString (String s)
		{
			//return Regex.Replace(s," |\u3000|&|'|\"","",RegexOptions.IgnoreCase|RegexOptions.Multiline).Replace ("\r\n", ".").Replace("\n", ".");

			s = s.Replace (" ", "");
			s = s.Replace("\u3000","");
			s = s.Replace ("&", "");
			s = s.Replace ("\"", "");
			s = s.Replace ("'", "");
			s = s.Replace ("\r\n", "。");
			s = s.Replace ("\n", "。");
			s = s.Replace("“","");
			return s;
		}

		public static String CleanStringTitle (String s)
		{
			return s.Replace (",", "")
				.Replace (".", "")
				.Replace (" ", "")
				.Replace ("。", "")
				.Replace ("?", "")
				.Replace ("!", "")
				.Replace ("，", "")
				.Replace ("（", "")
				.Replace ("）", "")
				.Replace ("(", "")
				.Replace (")", "")
				.Replace ("-", "")
				.Replace ("_", "")
				.Replace ("作者", "-")
				.Replace (":", "")
				.Replace ("：", "")
				.Replace ("\"", "")
				.Replace ("'", "")
				.Replace ("<", "")
				.Replace (">", "")
				.Replace ("\r\n", "。")
				.Replace ("\n", "。")
				.Replace (" ", "。")
				.Replace ("`", " ")
				.Replace ("“", " ")
				;
		}

		public static int[] ArrayDiff (int[] arr1, int[] arr2)
		{
			List<int> l = new List<int> ();
			l.AddRange (arr1);

			List<int> l2 = new List<int> ();
			l2.AddRange (arr2);

			List<int> diff = new List<int> ();

			foreach (int i in arr1) {
				if (!l2.Contains (i)) {
					diff.Add (i);
				} 
			}
			return diff.ToArray ();
		}

		public static int[] getArrayFromList (List<System.Object[]> PlayList)
		{
			int[] arr = new int[PlayList.Count];
			for (int i = 0; i < PlayList.Count; i++) {
				arr [i] = Convert.ToInt32 (PlayList [i] [0]);
			}
			return arr;
		}
	}

	class CnsWebClient: System.Net.WebClient
	{
		public  int timeout=60000; //60s

		public CnsWebClient(int _timeout)
		{
			this.timeout = _timeout;


			this.Encoding = Encoding.UTF8;
			this.Headers.Add ("Accept", "audio/mp3,text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
			this.Headers.Add ("Accept-Encoding", "gzip, deflate");
			this.Headers.Add ("Accept-Language", "zh-CN,zh;q=0.8,en-US;q=0.5,en;q=0.3");
			this.Headers.Add ("Cache-Control", "max-age=0");
			//this.Headers.Add ("Connection", "Close");
			this.Headers.Add ("User-Agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:42.0) Gecko/20100101 firefox/42.0");

		}

		protected override WebRequest GetWebRequest(Uri address)
		{
			WebRequest w =(WebRequest) base.GetWebRequest(address);
			w.PreAuthenticate = true;
			w.Timeout = timeout;
			return w;
		}
	}

	public class CnsXML
	{		
		public static bool xml_locked = false;
        public static int xml_depth = 0;
        public static int old_xml_depth = 0;
        public static TreeIter old_tree_iter = TreeIter.Zero;
        public static Dictionary<int,TreeIter> dict_iter = new Dictionary<int,TreeIter> ();

		public static void XmlAddNode (string xml, string ParentXPath, string StrChildNode)
		{
			XmlDocument editableDocument = new XmlDocument ();
			editableDocument.Load (xml);
			XPathNavigator editableNavigator = editableDocument.CreateNavigator ();
			editableNavigator.SelectSingleNode (ParentXPath).AppendChild (StrChildNode);
			editableDocument.Save (xml);
		}

		public static void XmlDelNode (string xml, string XPath)
		{
			//检查文件是否被锁
			while (xml_locked) {
				Thread.Sleep (10);
			}
			xml_locked = true;
			XmlDocument editableDocument = new XmlDocument ();
			editableDocument.Load (xml);
			XPathNavigator editableNavigator = editableDocument.CreateNavigator ();
			foreach (XPathNavigator node in editableNavigator.Select(XPath)) {
				Console.WriteLine (node.ToString ());
				node.DeleteSelf ();
			}
			editableDocument.Save (xml);
			xml_locked = false;
		}

		public static void  XmlEdit (string xml, string path, string value)
		{
			//检查文件是否被锁
			while (xml_locked) {
				Thread.Sleep (10);
			}
			xml_locked = true;
			XmlDocument editableDocument = new XmlDocument ();
			editableDocument.Load (xml);

			XPathNavigator editableNavigator = editableDocument.CreateNavigator ();

			editableNavigator.SelectSingleNode (path).SetValue (value);
			editableDocument.Save (xml);
			xml_locked = false;
		}

		public static string XmlGetValue (string xml, string path)
		{
			XmlDocument editableDocument = new XmlDocument ();
			editableDocument.Load (xml);

			XPathNavigator editableNavigator = editableDocument.CreateNavigator ();

			return editableNavigator.SelectSingleNode (path).Value.ToString ();
		}

		public static XPathNavigator  XmlGetNode (string xml, string path)
		{
			XmlDocument editableDocument = new XmlDocument ();
			editableDocument.Load (xml);

			XPathNavigator editableNavigator = editableDocument.CreateNavigator ();

			return editableNavigator.SelectSingleNode (path);
		}

		public static XPathNodeIterator  XmlGetNodes (string xml, string path)
		{
			XmlDocument editableDocument = new XmlDocument ();
			editableDocument.Load (xml);

			XPathNavigator editableNavigator = editableDocument.CreateNavigator ();

			return editableNavigator.Select (path);
		}

        public static  void Xml2TreeStore(TreeStore ArgTreeStore, String XmlFile)
        {
            XmlReaderSettings settings = new XmlReaderSettings ();
            settings.DtdProcessing = DtdProcessing.Parse;
            XmlReader reader = XmlReader.Create (XmlFile, settings);

            reader.MoveToContent ();

            while (reader.Read ()) {
                switch (reader.NodeType) {
                case XmlNodeType.DocumentType:
                    Console.WriteLine ("<!DOCTYPE {0} [{1}]", reader.Name, reader.Value);
                    break;

                case XmlNodeType.CDATA:
                    Console.WriteLine ("<![CDATA[{0}]]>", reader.Value);
                    break;

                case XmlNodeType.ProcessingInstruction:
                    Console.WriteLine ("<?{0} {1}?>", reader.Name, reader.Value);
                    break;

                case XmlNodeType.Comment:
                    Console.WriteLine ("<!--{0}-->", reader.Value);
                    break;

                case XmlNodeType.XmlDeclaration:
                    Console.WriteLine ("<?xml version='1.0'?>");
                    break;

                case XmlNodeType.Document:
                    break;

                case XmlNodeType.EntityReference:
                    Console.WriteLine (reader.Name);
                    break;

                case XmlNodeType.Element:
                    if (reader.Name.Equals ("a")) {
                        //Console.Write ("   "+reader.GetAttribute ("href").ToString ()+"\n");
                    }
                    //Console.WriteLine ("<{0}>", reader.Name);
                    CnsXML.xml_depth++;

                    break;

                case XmlNodeType.Text:
                    if (CnsXML.old_xml_depth == CnsXML.xml_depth) {
                        if (CnsXML.dict_iter [CnsXML.xml_depth].Equals (TreeIter.Zero))
                            CnsXML.old_tree_iter = ArgTreeStore.AppendValues (reader.Value.Trim (), "");
                        else
                            CnsXML.old_tree_iter = ArgTreeStore.AppendValues (CnsXML.dict_iter [CnsXML.xml_depth], reader.Value.Trim (), "");

                    } else {
                        if (!CnsXML.dict_iter .ContainsKey (CnsXML.xml_depth)) {
                            CnsXML.dict_iter .Add (CnsXML.xml_depth, CnsXML.old_tree_iter);
                        } else if (CnsXML.old_xml_depth  < CnsXML.xml_depth) {                        
                            CnsXML.dict_iter  [CnsXML.xml_depth] = CnsXML.old_tree_iter;
                        }

                        if (reader.Value != null) {
                            if (CnsXML.dict_iter  [CnsXML.xml_depth].Equals (TreeIter.Zero)) {
                                CnsXML.old_tree_iter = ArgTreeStore.AppendValues (reader.Value.Trim (), "");
                            } else {
                                CnsXML.old_tree_iter = ArgTreeStore.AppendValues (CnsXML.dict_iter  [CnsXML.xml_depth], reader.Value.Trim (), "");       
                            }
                        }     
                        CnsXML.old_xml_depth = CnsXML.xml_depth;
                    }
                    //Console.WriteLine (" ".PadLeft (xmldepth*4,' ')+reader.Value);
                    break;
                case XmlNodeType.EndElement:
                    //Console.WriteLine ("</{0}>", reader.Name);
                    CnsXML.xml_depth--;
                    break;
                } 
            }
        }
	}

	public class CnsFile
	{
		public  static bool IsFileInUse (string fileName)
		{
			bool inUse = true; 
			FileStream fs = null; 
			try { 
				fs = new FileStream (fileName, FileMode.Open, FileAccess.Read, FileShare.None); 
				inUse = false;
			} catch {
			} finally { 
				if (fs != null)
					fs.Close (); 
			} 
			return inUse;
		}
	}

	class CnsMPlayer
	{
		public int percent = 0;
		public Process proc;
		string cmd = "";
		public bool Running = false;
		public string FileName = "";

		public CnsMPlayer ()
		{
		}

		public string GetFileName ()
		{
			return FileName;
		}

		public void Play (String f, int TimePos = 0)
		{
			FileName = f;
			Running = true;
			proc = new System.Diagnostics.Process ();
			proc.StartInfo.CreateNoWindow = true;
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.ErrorDialog = false;
			proc.EnableRaisingEvents = true;
			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.RedirectStandardInput = true;
			proc.StartInfo.RedirectStandardError = true;
			proc.StartInfo.FileName = "/usr/bin/mplayer";
			//
			proc.StartInfo.Arguments = " -slave  -vo backend=x11 " + f;


			percent = 0;
			proc.Disposed += (object sender, EventArgs e) => {
				//Console.WriteLine ("Proc Disposed............");
			};
			proc.Exited += (object sender, EventArgs e) => {
				//Console.WriteLine ("Proc exited............");
				Running = false;
			};

			proc.OutputDataReceived += (object sender, DataReceivedEventArgs e) => {
				//Console.WriteLine ("OutputDataReceived: " + e.Data);
			};

			//Console.WriteLine ("Proc Start...");
			proc.Start ();
			if (TimePos > 0) {
				//Console.WriteLine ("Proc Seek...");
				Seek (TimePos);
			}
		}

		public void PlayAsync (String f, int TimePos = 0)
		{
			proc.StartInfo.Arguments = "  -slave -vo backend=x11 " + f;
			proc.Start ();
			if (TimePos > 0)
				Seek (TimePos);

			percent = 0;
			while (GetPercent () < 100) {
				if (cmd.Length > 0) {
					proc.StandardInput.WriteLine (cmd);
					proc.StandardInput.Flush ();

					proc.StandardInput.WriteLine (cmd);
					proc.StandardInput.Flush ();

					proc.StandardInput.WriteLine (cmd);
					proc.StandardInput.Flush ();

					System.Threading.Thread.Sleep (5);
				}
				System.Threading.Thread.Sleep (10);
			}
		}

		public void Pause ()
		{
			try{
			//"pause" 是个开关键，按一次Pause, 再按一次Resume
			if (proc!=null && !proc.HasExited) {
				cmd = "pause";
				proc.StandardInput.WriteLine (cmd);
				proc.StandardInput.Flush ();
			}
			}catch(Exception e){
			}
		}

		public void Resume ()
		{
			if (!proc.HasExited) {
				cmd = "pause";
				//"pause" 是个开关键，按一次Pause, 再按一次Resume
				proc.StandardInput.WriteLine (cmd);
				proc.StandardInput.Flush ();
			}
		}

		public  void Seek (int TimePos)
		{
			if (!proc.HasExited) {
				proc.StandardInput.WriteLine (string.Format ("seek {0} {1}", TimePos, "Seek.Absolute"));
				proc.StandardInput.Flush ();
			}
		}

		public void Stop ()
		{
			if (proc!=null && !proc.HasExited) {
				proc.Kill ();
				//proc.Dispose ();
			}
		}

		public int GetTimePos ()
		{
			if (!proc.HasExited) {
				proc.StandardInput.WriteLine ("get_time_pos");
				proc.StandardInput.Flush ();

				System.Threading.Thread.Sleep (5);

				while (true) {
					string line = proc.StandardOutput.ReadLine (); 
					if (line != null) {
						if (line.StartsWith ("ANS_TIME_POSITION=", StringComparison.Ordinal)) {
							return (int)Convert.ToDouble (line.Substring ("ANS_TIME_POSITION=".Length));
						}
					}
				}
			} else {
				return 0;
			}
		}

		public int GetPercent ()
		{
			if (!proc.HasExited) {
				proc.StandardInput.WriteLine ("get_percent_pos");
				proc.StandardInput.Flush ();
				while (true) {
					string line = proc.StandardOutput.ReadLine ();
					if (line != null) {
						if (line.StartsWith ("ANS_PERCENT_POSITION=", StringComparison.Ordinal)) {
							percent = Convert.ToInt32 (line.Substring ("ANS_PERCENT_POSITION=".Length));
							return percent;
						}
					} else {
						return percent;
					}
				}
			}
			return 0;
		}
	}

	public class CnsNet
	{
		public static bool TcpConnectTest (string hostname, int port, bool first = true)
		{
			try {
			IPAddress[] ips = Dns.GetHostAddresses (hostname);

			foreach (IPAddress ip in ips) {
				
					TcpClient client = new TcpClient (ip.ToString (), port);

					if (client.Connected) {
						//Console.WriteLine ("{0} connect successful ", ip.ToString ());
						if (first)
							return true;
					} else {
						Console.WriteLine ("{0} connect failed ", ip.ToString ());
						if (first)
							return false;
					}	
				}
			}
			catch(System.Net.Sockets.SocketException e){
				Console.WriteLine ("TcpConnectTest(515) connect Exception: {0} ",  e.StackTrace);
				if (first)
					return false;
			}
			catch (Exception e) {
				Console.WriteLine ("TcpConnectTest(522) connect Exception: {0} ",  e.StackTrace);
				if (first)
					return false;
			}
			return false;
		}
	}

	public class CnsWin
	{
		public  static void MessageBox(string title, string msg){
			MessageDialog ms =new MessageDialog (null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, msg);
			ms.Title = title;
			ms.Run();
			ms.Destroy();
		}

		public  static bool MessageBoxYesNo(string title, string msg){
			MessageDialog ms =new MessageDialog (null, DialogFlags.Modal, MessageType.Question, ButtonsType.YesNo, msg);
			ms.Title = title;
			ResponseType tp  = (ResponseType)ms.Run();		
			ms.Destroy();

			if (tp == Gtk.ResponseType.Yes) {
				return true;
			} else {
				return false;
			}
		}
	}
}