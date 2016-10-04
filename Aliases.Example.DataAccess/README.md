This is an example of a basic data access layer for automation tests.



Using it would look similar to:

```c#
var StandardUser = RepositoryProvider.Get<IUserRepository>().Get(new UserKey() { Site = "ExampleSite", Environment = TestConfiguration.TestEnvironmentType.ToString() });
```