# 🧩 **__RSUnityFramework__**
> _Reusable modular foundation for Unity projects — by RS Studio_  
> _Last updated: October 22, 2025_

🧱 Design Philosophy
> _“Write once, reuse everywhere.”_  
> `__RSUnityFramework__` is built for **scalability**, **modularity**, and **reusability**.  
> Any feature that could be reused in another project should be developed here.

---

## 📚 Table of Contents
1. [Overview](#-overview)
2. [Dependencies & Installation](#-dependencies-&-installation)
3. [Project Setup](#-project-setup)
4. [Resource Manager](#-resource-manager)
5. [Folder Structure](#-folder-structure)
6. [Integration Guidelines](#-integration-guidelines)
7. [Coding Standards](#-coding-standards)
8. [Notes](#-notes)

---

## 🌐 Overview
`__RSUnityFramework__` is the **core shared framework** used across all Unity projects at RS Studio.  
It provides a **modular architecture**, standardized **service layer**, and **utility toolkit** to accelerate development and ensure consistency between projects.

---
## 🧭 Dependencies & Installation

| Module | Repository Link | Git URL | How to install |
|:-------|:----------------|:----------------|:----------------|
| **Screen Navigation** | [UnityScreenNavigator](https://github.com/Haruma-K/UnityScreenNavigator) | `https://github.com/Haruma-K/UnityScreenNavigator.git?path=/Assets/UnityScreenNavigator` |Git URL (Built-in)|
| **VContainer** | [VContainer](https://github.com/hadashiA/VContainer) | `https://github.com/hadashiA/VContainer.git?path=VContainer/Assets/VContainer` |Git URL (Built-in)|
| **UniTask** | [UniTask](https://github.com/Cysharp/UniTask) | `https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask` |Git URL|
| **DOTween Pro** | [Link](https://drive.google.com/drive/u/1/folders/1BsvEVum8kThfDMFv2a6l8T-oSsscgraA) ||UnityPackage|
| **Rewired** | [Link](https://drive.google.com/drive/u/1/folders/1BsvEVum8kThfDMFv2a6l8T-oSsscgraA) ||UnityPackage|
| **The Ultimate Best Bundle** | [Link](https://drive.google.com/drive/u/1/folders/1BsvEVum8kThfDMFv2a6l8T-oSsscgraA) ||Extract to root/Packages|

Initiation Project:
1. Get an SSH key (if you haven't done so before) [Guide](https://docs.github.com/en/authentication/connecting-to-github-with-ssh/generating-a-new-ssh-key-and-adding-it-to-the-ssh-agent)
1. Connect PC to github with ssh (if you haven't done so before): [Guide](https://docs.github.com/en/authentication/connecting-to-github-with-ssh/generating-a-new-ssh-key-and-adding-it-to-the-ssh-agent)
2. Add submodule: Open cmd at the root project folder: 

   ```c#
   # Install RSUnityFramework
   # GO to project root folder
   # Add as a submodule via SSH
   git submodule add git@github.com:rulethesea/RSUnityFramework.git Assets/__RSUnityFramework__

   # Initialize and pull contents
   git submodule update --init --recursive
   ```

### Update package:

>> Install required modules using git urls and download links

---

## 🎮 Project Setup

#### A.Setup VContainer
1. Setup scene like DemoGameScene.scene 
2. You should create the LifeTimeScope look like this to install framework funcitons.
```c#
public class DemoGameLifetimeScope:  LifetimeScope  
{
    protected override void Configure(IContainerBuilder builder)
    {
        RSFrameworkInstaller.FrameworkInstaller(builder);
        UIManagerInstaller.Install(builder);
    }
}
```
#### B. Add a page to asset loader
1. Create a class PageA: Page
2. Create a prefab of PageA, set name same as class name
3. Add prefab to any folder (not resources), set addressable index same as class name
4. Create/Screen Navigator Settings
5. Create/Resource Loader/Addressable Asset Loader
6. Put Addressable Asset Loader in missing field in Screen Navigator Settings
   
#### C. Call a page from any class
PageA haven't exist on scene, it can call by UIManager.
1. Create Class DemoGeneralClass, with method PushPageA. Put the DemoGeneralClass to scene.
```c#
    public class DemoGeneralClass : MonoBehaviour
    {
        [Inject] UIManager _UIManager;  
        void Start()
        {
            PushPageA();
        }
        public void PushPageA()
        {
            _UIManager.PushPage<PageA>();
        }
    }
```
2. The DemoGeneralClass not yet exist in lifetimescope, so you need to register it first. Reopen the DemoGameLifetimeScope and modify it.
```c#
    public class DemoGameSceneInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RSFrameworkInstaller.Install(builder);
            UIManagerInstaller.Install(builder);
            // Find and register the existed class in scene
            builder.RegisterComponentInHierarchy<DemoGeneralClass>();
        }
    }
```

## 🗄️ Resource Manager

<details>
  <summary><strong>SHOW >>> </strong></summary>
   
#### A. Install via GameInstaller

   Inside `GameInstaller`, register the Resource Loader installer with your other modules:
   
   ```c#
   public class GameInstaller : LifetimeScope
   {
       protected override void Configure(IContainerBuilder builder)
       {
           ResourceLoaderInstaller.Install(builder);
       }
   }
   ```

#### B. Inject the correct loader
`RSResourceLoaderInstaller` binds two `IAssetLoader` implementations keyed by `ResourceLoaderType`:

| Enum key | Loader | Usage |
| --- | --- | --- |
| `ResourceLoaderType.AddressableAsset` | `AddressableAssetLoader` | For Addressables-backed assets. |
| `ResourceLoaderType.ResourceAsset` | `ResourcesAssetLoader` | For assets in a `Resources/` folder. |

Inject the loader you need with the matching key:

```c#
public class ResourceDemo : MonoBehaviour
{
    [Inject, Key(ResourceLoaderType.AddressableAsset)] IAssetLoader _addressableLoader;
    [Inject, Key(ResourceLoaderType.ResourceAsset)] IAssetLoader _resourcesLoader;

    async void Start()
    {
        // Load an addressable by its ID (ensure it matches your Addressables entry)
        var handle = await _addressableLoader.LoadAsync<GameObject>("MyResourceID");
        var instance = Instantiate(handle.Result);

        // Release when done to free memory
        _addressableLoader.Release(handle);
    }
}
```

> 🔑 Pick the correct `ResourceLoaderType` key for each injection so the container resolves the intended loader.
</details>

## 📁 Folder Structure

<details>
  <summary><strong>SHOW >>> </strong></summary>
   
### **RSUnityFramework/**
- **📂 Common/**  
  Base types, extensions, and helper utilities.
- **📂 Core/**  
  Service Locator, BaseManager, and optional Dependency Injection system.
- **📂 Editor/**  
  Custom editor tools, inspectors, and menu utilities.
- **📂 Managers/**  
  Central managers (GameManager, UIManager, SceneManager, etc.).
- **📂 Services/**  
  Global systems such as Audio, Save, Analytics, RemoteConfig, and Localization.
- **📂 UI/**  
  Base UI logic and reusable components (screens, popups, etc.).
- **📂 Utilities/**  
  Generic tools for Math, Event, Coroutine, Tween, and FileIO.
- **📂 Plugins/**  
  Third-party dependencies (DOTween, Odin, Firebase, etc.).

#### Folder Overview
   
| Folder | Description |
|--------|--------------|
| **Common** | Shared base types, helpers, and extension methods used across the framework. |
| **Core** | Foundation layer containing Service Locator, BaseManager, and optional DI system. |
| **Editor** | Custom Unity Editor tools, inspectors, and utilities for internal workflows. |
| **Managers** | Core managers that handle game state, UI flow, and scene transitions. |
| **Services** | Independent service modules for audio, saving, analytics, remote configs, and localization. |
| **UI** | Base UI architecture and shared components for screens and popups. |
| **Utilities** | Lightweight reusable tools for math, file IO, events, and coroutines. |
| **Plugins** | External libraries integrated into the framework (DOTween, Odin, Firebase, etc.). |

#### Module Detail

### 🧱 **Common/**
Contains shared definitions and basic helper classes.
- `Extensions/` – Common C# and Unity extension methods.  
- `Helpers/` – Generic helper functions (math, IO, string, etc.).  
- `Constants/` – Shared constants and enums.

---

### 🧠 **Core/**
Foundation systems for dependency management and initialization.
- `ServiceLocator` – Centralized access to global services.  
- `BaseManager` – Common base for managers.  
- `DependencyInjection` – Optional DI or lifetime management system.

> 🧩 This layer connects services, managers, and gameplay systems.

---

### 🧰 **Editor/**
Editor-time utilities and tools.  
- Custom inspectors and property drawers.  
- Odin integration scripts.  
- Menu and asset creation tools.  

> ⚠️ Automatically excluded from runtime builds.

---

### 🎮 **Managers/**
Core managers that control runtime systems.  
- `GameManager` – Entry point for the main game loop.  
- `UIManager` – Handles screen transitions and popup flow.  
- `SceneManager` – Manages scene transitions and loading.

> Each manager should remain **game-agnostic** and **reusable**.

---

### 🌍 **Services/**
Independent systems that manage cross-game data or logic:  
- `AudioService` – Music and SFX management.  
- `SaveService` – Player save & load logic.  
- `AnalyticsService` – Tracking and analytics wrappers.  
- `RemoteConfigService` – Runtime configuration management.  
- `LocalizationService` – Multi-language text and asset support.

> All services implement a common interface (e.g. `IService`)  
> and register themselves via the `ServiceLocator` or `ServiceManager`.

---

### 🖼️ **UI/**
Base UI components and view logic.  
- `UIBase` – Base class for all UI screens.  
- `UIComponent` – Reusable UI elements (buttons, progress bars, etc.).  
- `PopupBase` – Modal or popup window base class.

> Recommended architecture: **MVVM** or **MVC** pattern.

---

### 🔧 **Utilities/**
General-purpose reusable utilities.  
- `Math/` – Math helpers, interpolation, random utils.  
- `Time/` – Timer, cooldown helpers.  
- `Event/` – Event bus, observer system.  
- `Tween/` – DOTween helpers or custom tweens.  
- `Coroutine/` – Static coroutine runner.  
- `FileIO/` – File read/write, JSON, and serialization tools.

> Should be lightweight and independent of game logic.

---

### 📦 **Plugins/**
Third-party libraries and SDKs integrated into the project.  
- `DOTween/` – Tweening engine.  
- `OdinInspector/` – Editor enhancement tools.  
- `Firebase/`, `GameAnalytics/`, etc.

> 🧭 Keep all plugin code isolated.  
> Use wrapper classes in **Services/** or **Utilities/** for maintainability.
</details>

---

## 🧩 Integration Guidelines
<details>
  <summary><strong>SHOW >>> </strong></summary>

1. **Do not modify** plugin or third-party source code.  
   → Use wrapper classes in `Services/` or `Utilities/` instead.

2. **All services** must implement a shared interface (`IService`)  
   and register themselves via `ServiceLocator`.

3. **Framework updates** must remain **game-agnostic** and **backward compatible**.

4. Keep each module **self-contained** for easier maintenance and versioning.
</details>

---

## 🧭 Coding Standards

<details>
  <summary><strong>SHOW >>> </strong></summary>
| Convention | Example |
|-------------|----------|
| **Namespace format** | `RSUnity.Services.Audio`, `RSUnity.UI.Components` |
| **Script naming** | Match class name exactly (PascalCase) |
| **File organization** | One class per file unless strongly related |
| **Data storage** | Prefer `ScriptableObjects` for configuration |
| **Initialization** | Use `[RuntimeInitializeOnLoadMethod]` for auto-registration |
</details>

---

## 🏷️ Notes
- `__RSUnityFramework__` must remain **independent of game-specific content**.  
- Designed to be imported or version-controlled as a shared framework module.  
- Ensure compatibility with Unity **6000.0.60f1 (LTS)** or higher.

---

### © RS Studio – Internal Framework Documentation
Maintained by the **RS Core Engineering Team**  
_This document is intended for internal use only._
