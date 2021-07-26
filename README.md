# XunitDependencyInjection.Samples

## Intro

Samples for <https://github.com/pengweiqhca/Xunit.DependencyInjection>

之所以想分享这个话题是因为我觉得在我们开发过程中测试是非常重要的一部分，高质量项目的一个重要指标就是测试覆盖率，同时依赖注入已经是一个现代化应用中不可缺少的一部分，我们的 .NET Core 也是从一开始就集成了依赖注入，依赖注入对于测试项目也是不能缺席的。

xunit 是 .net 里目前使用的最多的测试组件，`Xunit.DependencyInjection` 是大师写的一个 xunit 依赖注入的扩展，它是基于微软的 `GenericHost`（通用主机） 来实现的，使用它我们可以很轻松的实现依赖注入，很好的和 .NET Core 做集成。

## How it works

那它是如何工作的呢？我们一起来看一下它的执行流程，它的执行流程分为四步

首先需要构建一个 Host，然后启动这个 Host，启动完成后执行测试用例，最后终止这个 Host

![执行流程](https://img2020.cnblogs.com/blog/489462/202012/489462-20201226174736685-232454613.png)

Host 又是如何构建的呢？我们一起看一下，Host 的构建也是分为四步

第一步，创建一个 `HostBuilder`，大多数情况下我们不需要用这个方法，使用默认的实现就好

第二步，Host 配置，对 Host 做一些自定义配置

第三步，服务配置，注册需要的服务

第四步，`Configure`，可以做一些初始化的配置，比如配置初始化以及测试数据的初始化等

![Host构建流程](https://img2020.cnblogs.com/blog/489462/202012/489462-20201226174850455-1969870223.png)

我们可以在测试项目里创建一个 `Startup` 类来控制 `Host` 的构建过程

## Samples

接着我们来看一些实际的测试示例，示例分为三部分，首先是一些基本用法，然后是和其他组件的集成，最后是一些扩展用法

![](https://img2020.cnblogs.com/blog/489462/202012/489462-20201226174934683-796917960.png)

### Get Started

首先来看一下 `Startup` 的用法，这个 `Startup` 和 asp.net core 里的 `Startup` 是很像的，无论是使用方式上还是实现上都是类似的，有兴趣的可以看一下源码对比一下，我们来看一下使用方式，通过下面的示例来感受一下

![](https://img2020.cnblogs.com/blog/489462/202012/489462-20201226175035842-328182987.png)

如果你只需要注册服务，直接在 `Startup` 中添加一个 `ConfigureServices` 方法，在这个方法中注册自己需要的服务即可，和 asp.net core 并无太多不同

如果你需要做一些初始化的工作，可以加一个 `Configure` 方法，在这个方法中实现自己的初始化逻辑就可以了，如果初始化的时候需要获取注入的服务实例，直接作为方法参数就可以，类似于 asp.net core 中 `Configure` 方法，只是不需要配置 Http 请求管道

如果你需要使用的配置，需要使用 Configuration，可以在 `ConfigureHost` 方法中通过 `ConfigureHostConfiguration` 扩展方法注册自己的配置

如果需要在注册服务的时候用到配置，可以在 `ConfigureServices` 方法中添加一个 `HostBuildContext` 的参数，`HostBuilderContext` 中的 `Configuration` 对象就是在 `ConfigureHost` 中注册的配置

如果需要在 `Configure` 方法中使用配置，直接添加一个 `IConfiguration` 的方法参数就可以了


我们再来看一下，如何在测试用例中使用注入的服务，一般情况下我们会直接通过构造器注入，在构造方法中添加需要注入的服务即可，除此之外我们还可以通过方法参数注入，结合 `InlineData` 和 `MemeberData` 使用，来看一下这个示例

![](https://img2020.cnblogs.com/blog/489462/202012/489462-20201226175122323-250152192.png)

### IoC/AOP Integration

接着我们来看一下和其他组件的集成，`AutoFac` 是一个很流行的 IOC 组件，`AspectCore` 是柠檬大佬写的一个 AOP 框架，我们以这两个为例子来看一下如何集成第三方的依赖注入和 AOP 组件，前面我们已经提到它是基于微软的 `GenericHost` 实现的，而 asp.net core 从 3.0 开始也是基于 `GenericHost` 实现的，所以在 asp.net core 里怎么集成，在这里也是一样的，来看一下示例，只需要使用对应的 `ServiceProviderFactory` 就可以了，是不是很简单呢

![](https://img2020.cnblogs.com/blog/489462/202012/489462-20201226175239689-585675884.png)

![](https://img2020.cnblogs.com/blog/489462/202012/489462-20201226175248964-1294477549.png)

### Test Server Integration

然后我们来看一下如何和 `TestServer` 做集成，`TestServer` 主要用于集成测试，使用 `TestServer` 的好处在于它是基于内存进行交互的没有真正的 HTTP 请求和 TCP 链接，会非常的高效，而且也不会监听某一个端口，所以不会有端口权限的问题。

`TestServer` 的使用主要有两步，首先是服务的注册，可以使用 `IHostBuilder` 或 `IWebHostBuilder` 的  `UseTestServer` 扩展方法注册 `TestServer`，可以使用 `IHost` 的 `GetTestClient` 扩展方法来注册和 `TestServer` 进行交互的 `HttpClient`

服务注册好之后就可以在测试用例里通过注入的 `HttpClient` 请求 API 或页面了，可以参考这个例子

![](https://img2020.cnblogs.com/blog/489462/202012/489462-20201226175352070-590239071.png)

## Extensions

### Hosted Service

然后我们来看一些扩展用法，`IHostedService` 可以用来实现一些初始化的操作或者后台服务，我们可以使用 `IHostedService` 来实现对应用的 Ready 检查，应用 Ready 之后再开始执行测试用例，这在有些场景下是很有用的

我们在 k8s 中部署的应用一般都会有一个 `HealthCheck`/`ReadinessCheck` 的接口来供 k8s 的 liveness/readiness 探针来探测应用的状态，只有应用 Ready 之后才会对外部提供服务

这个示例就是一个使用 `IHostedService` 来实现等待应用 Ready 后再开始执行测试用例的一个 demo

![](https://img2020.cnblogs.com/blog/489462/202012/489462-20201226175453709-2043321536.png)

> 注意：这里的等待不能在 `Startup` 的 `Configure` 方法中执行，因为 `Configure` 的执行是在调用 Host 的 `StartAsync` 方法之前执行的，而此时 webServer 还没有启动，所以是不能获取到 `TestClient` 的，而我们通过 `HostedService` 就可以在 Web Server 启动之后再执行我们的等待 Ready 逻辑

### ITestOutputHelperAccessor

在测试中如果想要输出一个日志的话只能借助于 `ITestOutputHelper` 来输出，直接使用 `Console.Write[Line]` 是看不到任何输出的，`ITestOutputHelper` 只能在测试用例中使用，在测试服务中是不能使用的，`Xunit.DependencyInjection` 提供了一个 `ITestOutputHelperAccessor` 的服务，类似于 `IHttpContextAccessor`，我们可以借助它来在自定义的服务中获取 `ITestOutputHelper` 来输出日志

这里是一个简单的示例

![](https://img2020.cnblogs.com/blog/489462/202012/489462-20201226175559391-991295986.png)

### Logging

再来看一个 `OutputHelperAccessor` 的实际应用，`Xunit.DependencyInjection` 提供了一个 Logging  的扩展，使得我们可以把测试过程中的日志输出出来，更好的帮助我们调试

集成方式也比较简单，可以参考这个示例，引用 `Xunit.DependencyInjection.Logging` 之后，在 `LoggerFactory` 中注册 `XunitTestOutputLoggerProvider` 即可

可以看到我们的日志直接输出出来了，默认的日志级别是 `Information` ，所以 `Debug` 级别的日志没有输出出来，有需要的话可以在注册的时候提供一个委托来控制是否要输出日志

![](https://img2020.cnblogs.com/blog/489462/202012/489462-20201226175632045-1929802990.png)


## Project Template

为了方便大家使用，我们提供了一个项目模板，可以通过一个命令就可以直接创建好一个测试项目，会包含一个默认的 `Startup` 不再需要自己去写方法了，使用的时候只需要根据需要做删减就可以了

默认的 `TargetFramework` 使用的是 `netcoreapp3.1`，可以通过 `-f`/`--franework` 指定自己想要使用的目标框架，比如说想要生成 net 5.0 的项目只需要指定 `-f net5.0` 就可以了

![](https://img2020.cnblogs.com/blog/489462/202012/489462-20201226175825704-27417913.png)

生成的内容如下所示：

![](https://img2020.cnblogs.com/blog/489462/202012/489462-20201226181243993-571863347.png)


## More

最后列出来了一些可能会有帮助的链接，第一个是项目的源代码，第二个是上面所有示例的源代码，后面的是使用到的 Nuget 包。

这个 xunit 扩展的代码实现是非常值得学习的，有很多和 asp.net core 的实现是很像的，有需要的可以去看看源码学习一下。

希望我的分享对大家有所帮助，大家在使用过程中有遇到任何问题都可以随时联系我或者直接在 Github 上建 issue。

## Reference

- <https://github.com/pengweiqhca/Xunit.DependencyInjection>
- <https://github.com/WeihanLi/XunitDependencyInjection.Samples>
- <https://www.nuget.org/packages/Xunit.DependencyInjection>
- <https://www.nuget.org/packages/Microsoft.AspNetCore.TestHost>
- <https://www.nuget.org/packages/Xunit.DependencyInjection.Logging>
- <https://www.nuget.org/packages/Xunit.DependencyInjection.Template>
