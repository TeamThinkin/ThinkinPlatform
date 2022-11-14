using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoadAssets : MonoBehaviour
{
    void Start()
    {
        loadItem();
    }

    private async void loadItem()
    {
        // https://storage.googleapis.com/matriculate-assets/{PLATFORM}/art#TestFX
        var bundle = await loadAssetBundle("https://storage.googleapis.com/matriculate-assets/Windows64/art");
        var prefab = bundle.LoadAsset<GameObject>("TestFX");
        var instance = Instantiate(prefab, this.transform);
        instance.transform.localPosition = Vector3.zero;
        instance.transform.localRotation = Quaternion.identity;
        instance.transform.localScale = Vector3.one;
        Debug.Log("Load complete");
    }

    private async Task<AssetBundle> loadAssetBundle(string BundleUrl)
    {
        var request = UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(BundleUrl, 0);
        await request.SendWebRequest().GetTask(); // .GetMyTask();
        var bundle = UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(request);

        return bundle;
    }
}