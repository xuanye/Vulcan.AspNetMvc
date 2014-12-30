# Vulcan.AspNetMvc

标签（空格分隔）： AspNetMvc,IOC

---

Vulcan.AspNetMvc 是一个ASP.NET MVC4的帮助类库，集成Ioc（使用structuremap3.0），使用ServiceStack.Text替换默认的Newtonsoft.Json来序列化Json. 同时提供一些常用的帮助类和基类，协助快速开发

## 安装

### 从NuGet安装
```
Install-Package Vulcan.AspNetMvc
```

## 如何使用

### 如何集成structuremap
> [structuremap][1] 是一个非常优秀轻量级的开源Ioc容器。


使用Vulcan集成structuremap非常简单只需在应用系统启动时添加如下注册的代码即可。
```
protected void Application_Start()
{
    AreaRegistration.RegisterAllAreas();

    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    RouteConfig.RegisterRoutes(RouteTable.Routes);

    Bootstraper.Initialise();
}
```

其中Bootstraper代码如下
```
 public class Bootstraper
{
    public static void Initialise()
    {
        List<Registry> rlist = new List<Registry>();
        //... add Service Registry
        rlist.Add(new ServiceRegistry());
        
        //add more Registry
        //...
        
        IContainer container =  ConfigureDependencies.InitContainer(rlist);

        //Register for MVC
        DependencyResolver.SetResolver(new StructureMapDependencyScope(container));

        //Register for Web API
        //GlobalConfiguration.Configuration.DependencyResolver = new StructureMapResolver(container);
       
    }
}
```
其中的Registry 向容器中注册服务的注册器，可添加多个，关于Registry更详细的内容可参考 [Registry DSL][2]

在Controller中使用
```
public class TestController : Controller
{
    private IHelloService service;
    public TestController(IHelloService service)
    {
        this.service = service;
    }
    
    public ActionResult Index()
    {
        ViewBag.Message =  this.service.Hello("admin");
        return View();
    }

}
```


### 使用ServiceStack.Text 替换Newtonsoft.Json 作为默认的Json序列化组件

#### 在MVC4 WebApp中使用ServiceStack.Text
**方法1**
自定义Controller基类，并重写父类的Json方法,如下所示

```
protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
{
    return new ServiceStackJsonResult
    {
        Data = data,
        ContentType = contentType,
        ContentEncoding = contentEncoding
    };
}
```
**方法2**
直接使用Vulcan.AspNetMvc 提供的 VulcanController

#### 在WebApi2中使用ServiceStack.Text
在WebApi2中默认使用ServiceStack.Text需要在应用系统启动时替换，代码如下所示
```
   protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;         
            var jqueryFormatter = config.Formatters.FirstOrDefault(x => x.GetType() == typeof(System.Web.Http.ModelBinding.JQueryMvcFormUrlEncodedFormatter));
            // 删除Json序列格式
            config.Formatters.Remove(config.Formatters.JsonFormatter);
            
            // 删除其他无用的格式，如果需要请保留
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);
            config.Formatters.Remove(jqueryFormatter);
            // 在第一个位置插入新的格式化方式，使用ServiceStack.Text
            config.Formatters.Insert(0, new ServiceStackTextFormatter(ServiceStack.Text.JsonDateHandler.TimestampOffset));
            //AreaRegistration.RegisterAllAreas(); 
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
```

## 使用上下文服务和认证过滤器
如果想使用上下文服务，并且使用VulcanController基类，那么有2个接口必须在你的应用程序中实现，并注册到容器中，如下所示

接口IAppContextService主要用于获得用户信息，判断权限和角色相关，应根据项目实际情况予以实现。
```
namespace Vulcan.AspNetMvc.Interfaces
{
    public interface IAppContextService
    {
        // 根据用户标识获取用户详细信息
        IUser GetUserInfo(string Identity);
        // 根据权限标识 判断用户是否有权限
        bool HasPrivilege(string identity, string privilegeCode);
        // 根据角色标识，判断用户是否属于某角色
        bool IsInRole(string identity, string roleCode);
    }
}
```
接口IUser ，实体接口，用于限定上下文用户信息的主要字段。
```
namespace Vulcan.AspNetMvc.Interfaces
{

    public interface IUser
    {
        string UserId { get; }
        string FullName { get; }
        string EmployID { get; }

        string GroupCode { get; set; }
        string GroupName { get; set; }

        string DeptCode { get; set; }
        string DeptName { get; set; }

        string OrgCode { get; set; }
        string OrgName { get; set; }

    }
}
```

将IAppContextService 的实现注册到容器中
```
public class AppContextService : IAppContextService
{

    public bool HasPrivilege(string identity, string privilegeCode)
    {
        return true; //需要实现
    }

    public bool IsInRole(string identity, string roleCode)
    {
        return true; //需要实现
    }

    public IUser GetUserInfo(string Identity)
    {
        //需要实现
        return new IdentityUser()
        {
            UserId = "admin",
            FullName= "管理员",
            EmployID ="100001"
        };
    }
}

public class IdentityUser : IUser
{
    // ... 省略实现代码
}
```
声明注册器，或者注册在其他注册器中
```
public class AppContextRegistry : Registry
{
    public AppContextRegistry()
    {
        For<IAppContextService>().Use<AppContextService>();        
    }
}
```
**应用系统启动时注册到容器中，参考之前的说明**  

配置Web.config,在`system.web` 节点添加表单认证，注意不同的项目设置不同的name，否则会有冲突
```
  <system.web>
    <authentication mode="Forms">
      <forms loginUrl="~/Home/Login" name=".VULCANCOOKIES" timeout="28800" />
    </authentication>
 </system.web>
```
在登录成功的时候使用`    FormsAuthentication.SetAuthCookie(userid, false);` 注册。

使用`VulcanAuthorizeAttribute` 配置认证和权限控制

在需要认证和权限控制Controller类上或者Action 方法上配置`VulcanAuthorizeAttribute`即可

可以配置在Controller上,表示整个Controller都需要登录用户才能方法
```
// 该Controller（/Test2）下所有的登录用户才能访问
[VulcanAuthorize]
public class Test2Controller : VulcanController
{

}
```

也可配置在单独的Action上，表示这个Action对应的Url需要登录用户能够访问
```
// 该Action（/Test/Index）必须是登录用户才能访问
[VulcanAuthorize]
public ActionResult Index()
{
  
    return View();
}
```
可以配置指定角色，多个用逗号分隔
```
[VulcanAuthorize( Roles="Admin,System")]
public ActionResult Index()
{
    return View();
}
```
可以配置指定用户，多个用逗号分隔
```
[VulcanAuthorize( Users="adminUser,xuanye")]
public ActionResult Index()
{
    return View();
}
```

可以配置拥有某个权限，**推荐这种方式做权限控制**
```
//"TEST_INDEX" 实际可以是权限系统中的权限标识，可在代码中配置成常量
[VulcanAuthorize(PagePrivilegeCode="TEST_INDEX")]
public ActionResult Index()
{
    return View();
}
```

### 使用资源服务和HtmlHelper
要使用资源服务必须再实现2个接口 `IResourceService`和`IResource`,如下所示
```
public class ResourceService : IResourceService
{
    public List<IResource> GetResourceByCode(string code)
    {
        return GetResourceByCode(code, false);
    }

    public List<IResource> GetResourceByCode(string code, bool hasAllOption)
    {
        return GetResourceByCode(code,"" , false);
    }

    public List<IResource> GetResourceByCode(string code, string parentCode, bool hasAllOption)
    {
        // 这里可实现成自己的缓存机制或者读取数据中的数据字典
        List<IResource> list = null;
        switch (code)
        {

            case "GENDER": //性别
                list = new List<IResource>();
                list.Add(new DefaultResource() { Code="1",Name="男",Value="1" });
                list.Add(new DefaultResource() { Code = "0", Name = "女", Value = "0" });
                list.Add(new DefaultResource() { Code = "9", Name = "保密", Value = "9" });
                break;
        }
        if (list == null)
        {
            list = new List<IResource>();
        }
        return list;
    }
}

public class DefaultResource : IResource
{
    public string Code { get;set; }
    public string Name { get; set; }
    public string Value { get; set; }
}
```

在web.config中配置命名空间,在`system.web\pages\namespaces` 最后添加`Vulcan.AspNetMvc.Extensions`
```
<system.web>
    <pages>
      <namespaces>
        <add namespace="System.Web.Helpers" />
        <add namespace="System.Web.Mvc" />
        <add namespace="System.Web.Mvc.Ajax" />
        <add namespace="System.Web.Mvc.Html" />
        <add namespace="System.Web.Routing" />
        <add namespace="System.Web.WebPages" />
        <add namespace="Vulcan.AspNetMvc.Extensions" />
      </namespaces>
    </pages>
</system.web>
```
在Views目录的web.config中添加命名空间
```
  <system.web.webPages.razor>
    <host factoryType="System.Web.Mvc.MvcWebRazorHostFactory, System.Web.Mvc, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" />
    <pages pageBaseType="System.Web.Mvc.WebViewPage">
      <namespaces>
        <add namespace="System.Web.Mvc" />
        <add namespace="System.Web.Mvc.Ajax" />
        <add namespace="System.Web.Mvc.Html" />
        <add namespace="System.Web.Routing" />
        <add namespace="Vulcan.AspNetMvc.Extensions"/>
      </namespaces>
    </pages>
  </system.web.webPages.razor>
```

在View中使用ResourceHtmlHelper,支持下拉列表，单选和多选框，显示文本等。
```
 @Html.ResourceDropDownList("DpList1", "GENDER", null, new {style="width:180px;" })
 
 @Html.ResourceCheckBoxList("CbList1", "GENDER", null, new { @class="checkbox"})
```

## 示例程序
待完成

  [1]: http://structuremap.github.io/
  [2]: http://structuremap.github.io/registration/registry-dsl/