<p align="center">
  <img src="Properties/thumb.png" width="120" alt="EmbyProxy" />
</p>

<h1 align="center">EmbyProxy</h1>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-6.0-512BD4?logo=dotnet&style=flat-square" />
  <img src="https://img.shields.io/badge/Emby-4.8+-52B54B?logo=emby&style=flat-square" />
  <img src="https://img.shields.io/badge/license-MIT-blue?style=flat-square" />
  <img src="https://img.shields.io/badge/dependencies-zero-success?style=flat-square" />
  <img src="https://img.shields.io/github/v/release/tardlk/EmbyProxy?style=flat-square" />
</p>

<p align="center">
  <b>Emby 网络优化插件</b><br>
  解决刮削元数据时 TMDB / TVDB / FanArt 的网络连通问题<br>
  支持 Emby 4.8.11+
</p>

---

## ✨ 功能

<table>
<tr>
<td width="33%">

### 🌐 选择性代理
仅对白名单域名走 HTTP 代理，其余流量直连，不影响内网访问速度。

</td>
<td width="33%">

### 🔄 替代 TMDB
将 `api.themoviedb.org` 和 `image.tmdb.org` 请求透明重定向到自建镜像站。

</td>
<td width="33%">

### 📡 强制 IPv4
对指定域名跳过 IPv6 解析，避免 IPv6 路由不通导致的超时。

</td>
</tr>
</table>

---

## ⚡ 快速开始

```bash
# 1. 下载插件
从 Releases 下载 EmbyProxy.dll

# 2. 放入 Emby 插件目录
cp EmbyProxy.dll /path/to/emby/plugins/

# 3. 重启 Emby
systemctl restart emby-server
```

配置路径：**Emby 管理后台 → 插件 → EmbyProxy**

> ⚠️ 修改配置后需重启 Emby 生效。

---

## 🛠 配置说明

### 代理服务器

| 配置项 | 说明 |
|--------|------|
| 启用代理服务器 | 开关 |
| 代理服务器地址 | 格式：`http://user:pass@host:port` |
| 代理域名列表 | 一行一个域名，默认已填 TMDB / TVDB / FanArt |

### 替代 TMDB

| 配置项 | 说明 |
|--------|------|
| 启用替代 TMDB | 开关 |
| 替代 TMDB API 地址 | 如 `https://api.tmdb.org` |
| 替代 TMDB 图片地址 | 如 `https://image.tmdb.org` |

### 强制 IPv4

| 配置项 | 说明 |
|--------|------|
| 启用强制 IPv4 | 开关 |
| IPv4 域名列表 | 一行一个域名，默认 `image.tmdb.org` |

---

## 🔧 技术架构

```
HttpClient.DefaultProxy       → 代理：白名单域名路由
_httpClientHandlerFactory    → TMDB 改写 + IPv4 强制
  ├─ DelegatingHandler       →     拦截请求，实时替换 URL
  └─ ConnectCallback         →     接管 DNS，锁定 IPv4 解析
```

**纯 .NET 6 实现，零外部依赖，无 Harmony。**  
通过 `System.Linq.Expressions` 动态编译委托实现零侵入拦截，无需修改 Emby 源码。

---

## 📦 本地编译

```bash
git clone https://github.com/tardlk/EmbyProxy.git
cd EmbyProxy
dotnet build -c Release
```

需要 .NET 8 SDK，产物在 `bin/Release/net6.0/EmbyProxy.dll`。

---

## 📄 License

MIT © [tardlk](https://github.com/tardlk)
