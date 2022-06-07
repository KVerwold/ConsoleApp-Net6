using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;

namespace ConsoleApp.Apps
{
	[Command("run", Description = "Execute run command")]
	public class RunApp
	{
		[Required(ErrorMessage = "You must specify the Argument")]
		[Argument(0, Description = "Argument description")]
		public string Argument { get; }

		[Option("--option", Description = "Option description")]
		public string Option { get; }

		private void OnExecute(IConsole console)
		{
			console.WriteLine(
					$"Would have run with argument {Argument} and option {Option}");
		}
	}
}