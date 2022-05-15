可以使用工具协助更新 EF 版本，这个工具能更新所有工具的版本，使用方法如下

通过下面代码安装 dotnet tool install -g dotnetCampus.UpdateAllDotNetTools 此后使用不用再次安装
通过下面代码更新所有工具 dotnet updatealltools


生成数据库
//Admin第一次生成
Add-Migration AdminDbSchame -c AdminContext
//更新数据结构
update-database -Context AdminContext

//再次跟新，每次都用不同的名字啦

Add-Migration AdminDbSchame20220415 -c AdminContext

--删除第一个 AdminDbSchame 生成的文件在执行
update-database AdminDbSchame20220415 -Context AdminContext




//Cms第一次生成
Add-Migration CmsDbSchame -c CmsContext
//更新数据结构
update-database -Context CmsContext

//再次跟新，每次都用不同的名字啦 
Add-Migration CmsDbSchame2 -c CmsContext 


sql-cache

dotnet sql-cache create "Data Source=.;Initial Catalog=Dev_Udx_Admin;Persist Security Info=True;User ID=devUdx;Password=devUdx;MultipleActiveResultSets=True" dbo udx_cache
