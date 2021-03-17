using MonoMod;
using UnityEngine;
using UnityEngine.UI;

class patch_VersionViewer : VersionViewer
{
    public static readonly string[] FlavorText = new string[]
    {
        // got some funny quips? add em here.
        "Too Hod to handle.",
    };

    [MonoModReplace]
    public void Start()
	{
        // gotta move the version viewer up
        GetComponent<Transform>().localPosition = new Vector3(-830f, -460f);

        GetComponent<Text>().text = $"{GlobalGameManager.Instance.ver}\n" +
            $"Seshat v{Seshat.Seshat.VersionString}\n" +
            NewFlavorText();
	}

    public string NewFlavorText()
    {
        // haha this was going to be funny
        return FlavorText[Random.Range(0, FlavorText.Length)];
    }
}
