# GenericPool
## How to use
Replace all:
* `Object.Instantiate(prefab)` with `PoolUnity.Get(prefab)`
* `Object.Destroy(instance)` with `PoolUnity.Put(instance)`
## More features
It's possible to get instance, use and return to pool with delay:
```charp
PoolUnity.GetWith(audioPrefab)
         .Then(audio => audio.Play())
         .ThenWait(audio => TimeSpan.FromSeconds(audio.clip.length))
         .ThenPut()
```