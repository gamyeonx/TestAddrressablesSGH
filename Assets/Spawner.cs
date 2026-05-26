using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Spawner : MonoBehaviour
{
    private GameObject Character = null;

    [Header("캐릭터의 어드레스를 입력해 주세요!")]
    [SerializeField] private string CharacterAddress = string.Empty;

    private void Start()
    {
        Character = null;
    }

    public void _ClickSpawn()
    {

        //Character가 null포인터가 아니라면
        //해당 인스턴스를 제거.

        if (!ReferenceEquals(Character, null))
        {
            ReleaseObj();
        }


        //캐릭터를 스폰 한다.
        Spawn();

    }


    void Spawn()
    {
        Addressables.InstantiateAsync(CharacterAddress, new Vector3(Random.Range(-2f, 2f), 5, 0), Quaternion.identity).Completed +=
            (AsyncOperationHandle<GameObject> obj) =>
            {
                Character = obj.Result;
            };

    }

    void ReleaseObj()
    {
        //객체가 삭제될 때 메모리 해제를 위해 레퍼런스 카운트 -1 및 오브젝트인스턴스 제거.
        Addressables.ReleaseInstance(Character);
    }

}