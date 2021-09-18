using System;
using System.Collections.Generic;
using System.Text;
using Renci.SshNet;

namespace consoleAppSSH
{
	class SSHConnection
	{
		// declaration of class variables
		private string _sshString;
		private string _domain;
		private string _username;
		private string _password;

		// some getters and setters
		public string SshString
		{
			get { return _sshString; }
			set { _sshString = value; }
		}
		public string Password
		{
			// removed getter to prevent password leakage
			set { _password = value; }
		}
		// constructor which creates a more interactive environment
		public SSHConnection()
		{
			while (true)
			{
				try
				{
					Console.Write("ssh-login: ");
					this._sshString = Console.ReadLine();
					// splits the _sshString into _domain and _username
					this._domain = this._sshString.Split("@")[1];
					this._username = this._sshString.Split("@")[0];
					// asks for password
					Console.Write("password for " + this._username + ": ");
					this._password = HidePassword();
					//breaks out of the loop when string has been entered correct
					break;
				}
				catch (Exception)
				{
					Console.WriteLine("username should have the format: username@domain.com");
				}
			}

		}

		// if a less interactive constructor is prefered,
		// also making the class more reusable in other programs
		public SSHConnection(string sshString, string password)
		{
			this._sshString = sshString;
			this._domain = this._sshString.Split("@")[1];
			this._username = this._sshString.Split("@")[0];
			this._password = password;
		}

		// Connect() spawns a interactive ssh shell
		public void Connect()
		{
				try
				{
					SshClient sshclient = new SshClient(this._domain, this._username, this._password);
					sshclient.Connect();
					ShellStream stream = sshclient.CreateShellStream("", 80, 24, 800, 600, 1024);

					while (true)
					{
						string line;

						while ((line = stream.ReadLine(TimeSpan.FromSeconds(0.2))) != null)
						{
							Console.WriteLine(line);
						}

						Console.Write(this._username + ": ");
						string inputL = Console.ReadLine();

						if (inputL == "break")
						{
							break;
						}

						stream.WriteLine(inputL);
					}
					stream.Close();
					sshclient.Disconnect();
				}
				catch
				{
					Console.WriteLine("the entered credentials was not correct");
				}
			
		}

		// hides password input
		public static string HidePassword()
		{
			string passwordInput = "";
			ConsoleKeyInfo keyPressed;
			
			do
			{
				keyPressed = Console.ReadKey(true);
				if (keyPressed.Key != ConsoleKey.Backspace && keyPressed.Key != ConsoleKey.Enter)
				{
					passwordInput += keyPressed.KeyChar;
				}
				else
				{
					if (keyPressed.Key == ConsoleKey.Backspace && passwordInput.Length > 0)
					{
						
						passwordInput = passwordInput.Substring(0, (passwordInput.Length - 1));
					}
				}


			} while(keyPressed.Key != ConsoleKey.Enter);
			return passwordInput;
		}

	}
}
