# rivals-unity

Unity (2022.3 LTS) starter scaffold for "Rivals" — a 1v1 fighting prototype using Unity Netcode for GameObjects (Netcode v2).

This repository contains a minimal scaffold: a .gitignore, Packages/manifest.json referencing the New Input System and Netcode, and starter C# scripts for player movement, fighting, health, and game management.

What I pushed
- .gitignore — Unity template
- README.md — instructions to open the project and next steps
- Packages/manifest.json — package dependencies (Input System, Unity Transport, Netcode)
- Assets/Scripts/PlayerController.cs — placeholder movement (PC controls: Z/Q/D/S, Space jump, LeftCtrl slide)
- Assets/Scripts/Fighter.cs — health and damage hooks (NetworkVariable) and light attack placeholder
- Assets/Scripts/GameManager.cs — basic round/respawn manager (singleton)
- Assets/Scripts/HealthUI.cs — simple UI script to bind to Fighter health

Notes and next steps
- I used Unity 2022.3 LTS in README recommendations. Open Unity Hub, create a new 3D project with 2022.3 LTS, then copy these files into the project folder or pull this repo into the project root.
- In the Unity Editor: install the Input System and Netcode for GameObjects + Unity Transport via the Package Manager (see README for package names).
- I intentionally kept scenes out of the initial commit to avoid large binary files; once you confirm, I can add a small scene (.unity) and prefabs, or I can guide you to create the scene locally and I'll push the scene next.

If you want, I can now:
- Push a simple Main.unity scene and prefab player with placeholder sprites (adds binary files), or
- Add Netcode lobby/spawn scripts immediately.

Tell me which and I will continue.
