// extern function with no attribute is patched by MonoMod
#pragma warning disable CS0626 

using Seshat;

class patch_GlobalGameManager : GlobalGameManager
{
    public extern void orig_Init();
    public new void Init()
    {
        SeshatLoader.LoadAuto();
        orig_Init();
    }
}
