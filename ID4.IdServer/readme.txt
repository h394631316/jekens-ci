首先编写一个提供应用列表、账号列表的Config类
如果允许在数据库中配置账号等信息，那么可以从数据库中读取然后返回这些内容。

openssl 官网
https://slproweb.com/products/Win32OpenSSL.html

cmd>openssl req -newkey rsa:2048 -nodes -keyout cas.clientservice.key -x509 -days 365 -out cas.clientservice.cer

下面将生成的证书和Key封装成一个文件，以便IdentityServer可以使用它们去正确地签名tokens

cmd>openssl pkcs12 -export -in cas.clientservice.cer -inkey cas.clientservice.key -out cas.clientservice.pfx



ID4.IdServer+MvcClient = id4+id4QuickstartUi+oidc