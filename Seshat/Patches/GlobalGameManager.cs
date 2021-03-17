// extern function with no attribute is patched by MonoMod
#pragma warning disable CS0626 

class patch_GlobalGameManager : GlobalGameManager
{
    public extern void orig_Init();
    public new void Init()
    {
        orig_Init();
        // run load for all modules
        Seshat.Seshat.RunLoad();
    }

    public extern void orig_OnApplicationQuit();
    public void OnApplicationQuit()
    {
        orig_OnApplicationQuit();
        // run unload for all modules
        Seshat.Seshat.RunUnload();
    }
}
