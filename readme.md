## DataDog Redis Null Result Repo

```csharp
 var db = GetRedisCon().GetDatabase();
 var key =(RedisKey) Guid.NewGuid().ToString();
 var res = await db.HashKeysAsync(key); //res should be a non null empty RedisValue array.   similar behavior with HashGetAllAsync

```

### steps to repo
* edit the 'redisCon' value in the web.config ( tested with 3 node cluster)
* build and run the webapi
* navigate to /api/values
* should return

```xml
<ArrayOfstring xmlns="http://schemas.microsoft.com/2003/10/Serialization/Arrays" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
   <string>value1</string>
   <string>value2</string>
   <string>correct result</string>
   <string>result:StackExchange.Redis.HashEntry[], size is:0,isnull:False</string>
</ArrayOfstring>
```
the result we see if AMP 

```xml
<ArrayOfstring xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://schemas.microsoft.com/2003/10/Serialization/Arrays">
<string>value1</string>
<string>value2</string>
<string>apm oops</string>
<string>result:, size is:,isnull:True</string>
</ArrayOfstring>
```
