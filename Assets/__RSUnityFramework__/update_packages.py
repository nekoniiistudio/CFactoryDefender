import json
import os
import urllib.request

# --- CONFIG ---
# Tự động xác định đường dẫn project Unity.
# Giả định script này nằm trong .../Project/Assets/..., chúng ta cần đi ngược lên 2 cấp để tới thư mục gốc của project.
PROJECT_ROOT = os.path.abspath(os.path.join(os.path.dirname(__file__), "..", ".."))
MANIFEST_PATH = os.path.join(PROJECT_ROOT, "Packages", "manifest.json")

# List package cần tải về. Mỗi item là một dictionary chứa:
# - url: Link trực tiếp để tải file .tgz
# - destination: Đường dẫn tương đối từ thư mục gốc project để lưu file (ví dụ: "Assets/Plugins/MyPackage.tgz")
DOWNLOADABLE_PACKAGES = [
    {"url": "https://dl.google.com/games/registry/unity/com.google.firebase.app/com.google.firebase.app-13.6.0.tgz", "destination": "Assets/__RSUnityFramework__/Plugins/Firebase/com.google.firebase.app-13.6.0.tgz"},
    {"url": "https://dl.google.com/games/registry/unity/com.google.firebase.analytics/com.google.firebase.analytics-13.6.0.tgz", "destination": "Assets/__RSUnityFramework__/Plugins/Firebase/com.google.firebase.analytics-13.6.0.tgz"},
    {"url": "https://dl.google.com/games/registry/unity/com.google.firebase.auth/com.google.firebase.auth-13.6.0.tgz", "destination": "Assets/__RSUnityFramework__/Plugins/Firebase/com.google.firebase.auth-13.6.0.tgz"},
    {"url": "https://dl.google.com/games/registry/unity/com.google.firebase.messaging/com.google.firebase.messaging-13.6.0.tgz", "destination": "Assets/__RSUnityFramework__/Plugins/Firebase/com.google.firebase.messaging-13.6.0.tgz"},
    {"url": "https://dl.google.com/games/registry/unity/com.google.firebase.crashlytics/com.google.firebase.crashlytics-13.6.0.tgz", "destination": "Assets/__RSUnityFramework__/Plugins/Firebase/com.google.firebase.crashlytics-13.6.0.tgz"},
]

# List package bạn muốn đảm bảo có trong project
REQUIRED_PACKAGES = {
    "com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask#2.1.0",
    "com.google.firebase.app": "file:../Assets/__RSUnityFramework__/Plugins/Firebase/com.google.firebase.app-13.6.0.tgz",
    "com.google.firebase.analytics": "file:../Assets/__RSUnityFramework__/Plugins/Firebase/com.google.firebase.analytics-13.6.0.tgz",
    "com.google.firebase.auth": "file:../Assets/__RSUnityFramework__/Plugins/Firebase/com.google.firebase.auth-13.6.0.tgz",
    "com.google.firebase.crashlytics": "file:../Assets/__RSUnityFramework__/Plugins/Firebase/com.google.firebase.crashlytics-13.6.0.tgz",
    "com.google.firebase.messaging": "file:../Assets/__RSUnityFramework__/Plugins/Firebase/com.google.firebase.messaging-13.6.0.tgz",
    "com.google.ads.mobile.mediation.applovin": "8.5.0",
    "com.applovin.mediation.ads": "8.5.1",
    "com.unity.localization": "1.5.9"
}

# List Scoped Registry bạn muốn đảm bảo có trong project
REQUIRED_SCOPED_REGISTRIES = [
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.google"
      ]
    },
    {
        "name": "AppLovin MAX Unity",
        "url": "https://unity.packages.applovin.com/",
        "scopes": [
            "com.applovin.mediation.ads"
        ]
    }
]


# --- SCRIPT ---
def download_packages(packages_to_download):
    """Tải các package từ URL nếu chúng chưa tồn tại."""
    downloaded_count = 0
    print("Bắt đầu kiểm tra và tải các local package...")

    for package in packages_to_download:
        url = package["url"]
        destination_rel_path = package["destination"]
        destination_abs_path = os.path.join(PROJECT_ROOT, destination_rel_path)

        # Kiểm tra file đã tồn tại chưa
        if os.path.exists(destination_abs_path):
            print(f"  - Đã tồn tại: {destination_rel_path}")
            continue

        print(f"  - 📥 Đang tải: {os.path.basename(destination_rel_path)} từ {url}")

        # Tạo thư mục nếu chưa có
        destination_folder = os.path.dirname(destination_abs_path)
        os.makedirs(destination_folder, exist_ok=True)

        # Tải file
        try:
            urllib.request.urlretrieve(url, destination_abs_path)
            print(f"  - ✅ Tải thành công: {destination_rel_path}")
            downloaded_count += 1
        except Exception as e:
            print(f"  - ❌ Lỗi khi tải {url}: {e}")
    return downloaded_count

def load_manifest(path):
    if not os.path.exists(path):
        raise FileNotFoundError(f"Không tìm thấy manifest: {path}")
    with open(path, "r") as f:
        return json.load(f)


def save_manifest(path, data):
    with open(path, "w") as f:
        json.dump(data, f, indent=4)
    print(f"Đã cập nhật manifest thành công: {path}")


def update_packages(manifest, required_packages):
    deps = manifest.get("dependencies", {})

    added = []
    updated = []

    for pkg, new_version in required_packages.items():
        if pkg not in deps:
            deps[pkg] = new_version
            added.append((pkg, new_version))
        else:
            old_version = deps[pkg]
            # Nếu muốn update version mới hơn
            if old_version != new_version:
                deps[pkg] = new_version
                updated.append((pkg, old_version, new_version))

    manifest["dependencies"] = deps

    return added, updated

def update_scoped_registries(manifest, required_registries):
    """Kiểm tra và thêm các scoped registry còn thiếu."""
    registries = manifest.get("scopedRegistries", [])
    changed = False

    # Lấy danh sách tên của các registry đã có để kiểm tra
    existing_registry_names = {reg.get("name") for reg in registries}

    for required_reg in required_registries:
        # Nếu registry chưa tồn tại, thêm vào
        if required_reg.get("name") not in existing_registry_names:
            registries.append(required_reg)
            print(f"➕ Thêm Scoped Registry: {required_reg.get('name')}")
            changed = True

    if changed:
        manifest["scopedRegistries"] = registries
    
    return changed


def main():
    downloaded_count = download_packages(DOWNLOADABLE_PACKAGES)
    if downloaded_count > 0:
        print(f"Tổng cộng đã tải {downloaded_count} package mới.")
        print("──────────────────────────────")

    manifest = load_manifest(MANIFEST_PATH)

    packages_added, packages_updated = update_packages(manifest, REQUIRED_PACKAGES)
    registries_changed = update_scoped_registries(manifest, REQUIRED_SCOPED_REGISTRIES)
    
    if packages_added or packages_updated or registries_changed:
        save_manifest(MANIFEST_PATH, manifest)

    print("──────────────────────────────")
    print("Kết quả cập nhật package:")
    print("──────────────────────────────")

    if packages_added:
        print("➕ Package thêm vào:")
        for pkg, version in packages_added:
            print(f"   • {pkg} (version {version})")

    if packages_updated:
        print("♻ Package update version:")
        for pkg, old_version, new_version in packages_updated:
            print(f"   • {pkg} (từ {old_version} sang {new_version})")

    if not packages_added and not packages_updated and not registries_changed and downloaded_count == 0:
        print("✔ Không có package nào cần cập nhật.")

    input("\nNhấn Enter để kết thúc...")


if __name__ == "__main__":
    main()
