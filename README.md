# GenericPool
## How to install
Add in `Packages/manifest.json` to `dependencies`:
```javascript
"com.danand.genericpool": "https://github.com/Danand/GenericPool.git#0.1.1-package-unity"
```
## How to use
Replace all:
* `Object.Instantiate(prefab)` with `PoolUnity.Get(prefab)`
* `Object.Destroy(instance)` with `PoolUnity.Put(instance)`

Notice that `PoolUnity` actually is not a static class. It must be properly injected instance, in the way you liked (DI or just `GetComponent<PoolUnity>()`).
## More features
It's possible to get instance, use and return to pool with delay:
```csharp
PoolUnity.GetWith(audioPrefab)
         .Then(audio => audio.Play())
         .ThenWait(audio => TimeSpan.FromSeconds(audio.clip.length))
         .ThenPut()
```
