# Tetris WPF

![License](https://img.shields.io/github/license/wojciechszymanski-dev/tetris-wpf)
![.NET](https://img.shields.io/badge/.NET-6.0%2B-blue)
![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey)

A classic, polished Tetris clone built using **C#** and **Windows Presentation Foundation (WPF)**. This project implements smooth animations, block logic, and a responsive UI, showcasing MVVM patterns and game development basics in the .NET ecosystem.

---

## 🎮 Features

* **Classic Gameplay:** All 7 standard Tetrominoes (I, J, L, O, S, T, Z) with accurate rotation logic.
* **Next Piece Preview:** Plan your strategy by seeing the upcoming block.
* **Ghost Piece:** A subtle shadow showing exactly where your block will land.
* **Leveling System:** The game speeds up as you clear more lines.
* **Score Tracking:** Points awarded based on lines cleared (Single, Double, Triple, or the elusive Tetris).
* **Held Piece:** Swap and save a Tetromino for later use.
* **Game Over States:** Visual feedback when the stack hits the top.

## ⌨️ Controls

| Key | Action |
| :--- | :--- |
| **Left / Right Arrow** | Move Tetromino |
| **Up Arrow** | Rotate Clockwise |
| **Down Arrow** | Soft Drop (Faster fall) |
| **Space** | Hard Drop (Instant land) |
| **C / Left Shift** | Hold / Swap Piece |
| **P** | Pause Game |
| **Esc** | Quit |

## 🚀 Getting Started

### Prerequisites
* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (with .NET desktop development workload)
* [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or higher.

### Installation
1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/wojciechszymanski-dev/tetris-wpf.git](https://github.com/wojciechszymanski-dev/tetris-wpf.git)
    ```
2.  **Open the project:**
    Open `Tetris.sln` in Visual Studio.
3.  **Build and Run:**
    Press `F5` to compile and launch the application.

## 🛠️ Built With
* **C#** - Programming language.
* **WPF (Windows Presentation Foundation)** - UI Framework.
* **XAML** - UI Design and layout.

## 📝 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
