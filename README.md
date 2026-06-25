***TO USE***

*Note: This system is intended for CLI programs. It does NOT support Windows Forms or any game engine.*

**1. Fork this code into your own project.**

**2. Add any API methods your modders need inside this block:**
```cs
   // Add any custom API methods here that your mods will call.
   // This section is intentionally left empty for developers to expand.
   // ===================== MOD EXTENSION METHODS =======================
   
   
   
   // ===================================================================
```
   Examples of things you might add:

- CreateObject()
- RegisterItem()
- SpawnEntity()
- SaveData()
- Anything your mods need to interact with your program

**3. In your program’s entry point, call:**

>Ouside your entry point method 
  ```cs
  public static Placeholder.ModdingSystems ModLoader = new Placeholder.ModdingSystems();
  ```
>Inside your Entry point method
 ```cs
 string modFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YourModFolderName");

	if (!Directory.Exists(modFolder))
	{
		Directory.CreateDirectory(modFolder);
		// Optional: tell the user
		Console.WriteLine("Mods folder not found — created a new one.");
	}

	// Now load mods safely
	try
	{
	    ModLoader.LoadMods(modFolder);
	}
	catch (Exception ex)
	{
   Console.WriteLine($"An Error Occured loading mods error details:{ex}");
   Console.WriteLine("press any key to continue:");
		Console.ReadKey(true);
	}

   // Replace "Placeholder" with your actual namespace.
   // Replace "YourModFolderName" with the folder where your mods are stored.

