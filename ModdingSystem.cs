using System.Reflection;
// ===============================================================
//  IMPORTANT: CHANGE THIS NAMESPACE TO MATCH YOUR PROJECT
//  If you do NOT change this, your mod WILL NOT LOAD.
// ===============================================================
namespace Placeholder //CHANGE THIS TO YOUR NAMESPACE 
{
	public interface IMod
	{
		string Name { get; } // name of the mod (Read only to provide clean detials)
							 //↓Register Command (the mod calls this to do this)↓
		void Init(ModdingSystems api); //TODO: Learn more 

		bool TryExecute(string input); //Did the mod do what it needed to?

// Add any custom API methods here that your mods will call.
// This section is intentionally left empty for developers to expand.
// ===================== MOD EXTENSION METHODS =======================



  
// ===================================================================

	}
	public class ModdingSystems
	{
		// Stores all successfully loaded mods
		private readonly List<IMod> _mods = new();
		public bool HasMods => _mods.Count > 0;

		public void LoadMods(string folder)
		{
			// Look for every .dll file in the folder
			foreach (var file in Directory.GetFiles(folder, "*.dll"))
			{
				// Load the DLL so we can inspect its types
				var assem = Assembly.LoadFrom(file);

				// Check every type inside the DLL
				foreach (var type in assem.GetTypes())
				{
					// Only load real mod classes:
					// - must implement IMod
					// - must NOT be an interface
					// - must NOT be abstract
					if (typeof(IMod).IsAssignableFrom(type) &&
						!type.IsInterface &&
						!type.IsAbstract)
					{
						// Create the mod instance
						var mod = (IMod)Activator.CreateInstance(type);

						// Let the mod initialize itself
						mod.Init(this);

						// Store it for later use
						_mods.Add(mod);
					}
				}
			}
      //comment out this block if you want to hide the loaded mods
			foreach (IMod mod in _mods)
			{
         //↓comment out this line if you want to hide the loaded mods↓
				Console.WriteLine($" - {mod.Name} loaded");//or any custom text ouput method
			}
		}
		public bool Execute(string input)
		{
			foreach (var mod in _mods)
			{
				if (mod.TryExecute(input))
					return true;
			}

			return false;
		}

	}

}
