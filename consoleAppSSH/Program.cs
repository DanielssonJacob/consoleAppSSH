using System;
using Renci.SshNet;


namespace consoleAppSSH
{
	class Program
	{
		static void Main(string[] args)
		{
			// logic moved to class
			while(true){
				SSHConnection sshShell = new SSHConnection();
				sshShell.Connect();
				Console.Write("make a second connection? [y/n] ");
				string secondConnection = Console.ReadLine().ToLower();
				if (secondConnection == "n"){
					break;
				}

			
			}

		}

	}
}
