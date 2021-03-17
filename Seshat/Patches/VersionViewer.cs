using MonoMod;
using UnityEngine.UI;

class patch_VersionViewer : VersionViewer
{
    [MonoModReplace]
    public void Start()
	{
		GetComponent<Text>().text = $"{GlobalGameManager.Instance.ver}\n" +
            $"Seshat v{Seshat.Seshat.VersionString}\n" +
            NewFlavorText();
	}

    public string NewFlavorText()
    {
        // haha this was going to be funny
        return "Too Hod to handle.";
    }
}
