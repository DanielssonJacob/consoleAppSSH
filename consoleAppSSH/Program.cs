using System;
using Renci.SshNet;


namespace consoleAppSSH
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				try
				{
					Console.Write("ssh-login: ");
					string domnainUser = Console.ReadLine();
					string[] domainUserArray = domnainUser.Split("@");
					Console.Write("password for " + domainUserArray[0] + ": ");
					string password = Console.ReadLine();
					Console.WriteLine();


					SshClient sshclient = new SshClient(domainUserArray[1], domainUserArray[0], password);
					sshclient.Connect();
					ShellStream stream = sshclient.CreateShellStream("", 80, 24, 800, 600, 1024);
					string s = "";
					while (true)
					{
						string line;
						while ((line = stream.ReadLine(TimeSpan.FromSeconds(0.2))) != null)
						{
							Console.WriteLine(line);
						}
						Console.Write(domainUserArray[0] + ": ");
						string inputL = Console.ReadLine();
						if (inputL == "break")
						{
							Console.WriteLine(s);
							break;
						}
						stream.WriteLine(inputL);

					}

					stream.Close();
					sshclient.Disconnect();
					break;
				}
				catch
				{
					Console.WriteLine("error, please try again");
				}
			}
		}

	}
}
