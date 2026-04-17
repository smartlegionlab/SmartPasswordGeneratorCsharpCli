# Smart Password Generator CLI (C#) <sup>v1.0.0</sup>

**Terminal-based smart password generator with deterministic password generation. Generate passwords from secret phrases - same secret always produces the same password, no storage required. All from your command line.**

---

[![GitHub top language](https://img.shields.io/github/languages/top/smartlegionlab/SmartPasswordGeneratorCsharpCli)](https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli)
[![GitHub license](https://img.shields.io/github/license/smartlegionlab/SmartPasswordGeneratorCsharpCli)](https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli/blob/master/LICENSE)
[![GitHub release](https://img.shields.io/github/v/release/smartlegionlab/SmartPasswordGeneratorCsharpCli)](https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli/)
[![GitHub stars](https://img.shields.io/github/stars/smartlegionlab/SmartPasswordGeneratorCsharpCli?style=social)](https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/smartlegionlab/SmartPasswordGeneratorCsharpCli?style=social)](https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli/network/members)

---

## ⚠️ Disclaimer

**By using this software, you agree to the full disclaimer terms.**

**Summary:** Software provided "AS IS" without warranty. You assume all risks.

**Full legal disclaimer:** See [DISCLAIMER.md](https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli/blob/master/DISCLAIMER.md)

---

## Core Principles

- **Deterministic Generation**: Same secret + same length = same password, every time
- **Zero Storage**: Passwords exist only when generated, never stored
- **Memory-Based Security**: Your brain is the only password database
- **Cross-Platform**: Windows, Linux support
- **Multi-Mode Generation**: Smart passwords, strong random, and auth codes

## Key Features

- **Smart Password Generation**: Deterministic from secret phrase
- **Public Key Generation**: Generate verification key from secret
- **Secret Verification**: Verify secret against public key
- **Strong Random Passwords**: Cryptographically secure random passwords
- **Authentication Codes**: Short codes for 2FA/MFA (4-20 chars)
- **Interactive Mode**: Menu-driven interface for easy use
- **Command-Line Mode**: Scriptable generation for automation
- **Hidden Input**: Secret phrase entry with asterisks masking

## Security Model

- **Proof of Knowledge**: Public keys verify secrets without exposing them
- **Deterministic Certainty**: Mathematical certainty in password regeneration
- **Ephemeral Passwords**: Passwords exist only in memory during generation
- **Local Computation**: No data leaves your device
- **No Recovery Backdoors**: Lost secret = permanently lost passwords (by design)

---

## Research Paradigms & Publications

- **[Pointer-Based Security Paradigm](https://doi.org/10.5281/zenodo.17204738)** - Architectural Shift from Data Protection to Data Non-Existence
- **[Local Data Regeneration Paradigm](https://doi.org/10.5281/zenodo.17264327)** - Ontological Shift from Data Transmission to Synchronous State Discovery

---

## Technical Foundation

Powered by **[smartpasslib-csharp](https://github.com/smartlegionlab/smartpasslib-csharp)** — C# implementation of deterministic password generation.

**Key derivation (same as Python/JS/Kotlin/Go versions):**

| Key Type    | Iterations | Purpose                                               |
|-------------|------------|-------------------------------------------------------|
| Private Key | 30         | Password generation (never stored, never transmitted) |
| Public Key  | 60         | Verification (stored on server)                       |

**Character Set:** `abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$&*-_`

## Download

### Pre-built Binaries (no .NET required)

| Platform        | Download                                                                                                                         |
|-----------------|----------------------------------------------------------------------------------------------------------------------------------|
| **Windows x64** | [SmartPasswordGeneratorCsharpCli-win-x64.exe](https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli/releases/latest) |
| **Linux x64**   | [SmartPasswordGeneratorCsharpCli-linux-x64](https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli/releases/latest)   |

> Just download, run in terminal, and start using.

### Build from Source

```bash
# Clone repository
git clone https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli.git
cd SmartPasswordGeneratorCsharpCli

# Run directly (requires .NET SDK)
dotnet run --project SmartPasswordGeneratorCsharpCli/

# Publish single file executable
dotnet publish -c Release -o ./publish
```

## Quick Start

### Run the Application

```bash
# Windows
SmartPasswordGeneratorCsharpCli-win-x64.exe

# Linux
./SmartPasswordGeneratorCsharpCli-linux-x64
```

### Interactive Menu

```
================================================================================
                       SMART PASSWORD GENERATOR (C#) CLI
                                 Version: 1.0.0
================================================================================

 MAIN MENU
 1. Generate Smart Password
 2. Generate Strong Random Password
 3. Generate Auth Code
 4. Show Public Key
 5. Verify Secret
 6. Help
 0. Exit

Select option: 

```

### Command-Line Mode

```bash
# Generate smart password (deterministic)
SmartPasswordGeneratorCsharpCli smart "MySecretPhrase123" 16

# Generate strong random password
SmartPasswordGeneratorCsharpCli strong 20

# Generate auth code (2FA)
SmartPasswordGeneratorCsharpCli code 8

# Generate public key from secret
SmartPasswordGeneratorCsharpCli public "MySecretPhrase123"

# Verify secret against public key
SmartPasswordGeneratorCsharpCli verify "MySecretPhrase123" "a1b2c3d4..."

# Show help
SmartPasswordGeneratorCsharpCli help
```

## Commands Reference

| Command                        | Description                       |
|--------------------------------|-----------------------------------|
| `smart <secret> <length>`      | Generate deterministic password   |
| `strong <length>`              | Generate cryptographically random |
| `code <length>`                | Generate auth code (4-20 chars)   |
| `public <secret>`              | Generate public key from secret   |
| `verify <secret> <public_key>` | Verify secret against public key  |
| `help`                         | Show help information             |

## Security Requirements

### Secret Phrase
- **Minimum 12 characters** (enforced)
- Case-sensitive
- Use mix of: uppercase, lowercase, numbers, symbols, emoji, or Cyrillic
- Never store digitally

### Strong Secret Examples
```
✅ "MyCatHippo2026"          — mixed case + numbers
✅ "P@ssw0rd!LongSecret"     — special chars + numbers + length
✅ "КотБегемот2026НаДиете"   — Cyrillic + numbers
```

### Weak Secret Examples (avoid)
```
❌ "password"                — dictionary word, too short
❌ "1234567890"              — only digits, too short
❌ "qwerty123"               — keyboard pattern
```

## Input Validation

| Parameter                    | Minimum  | Maximum    |
|------------------------------|----------|------------|
| Secret phrase                | 12 chars | unlimited  |
| Smart/Strong password length | 12 chars | 1000 chars |
| Auth code length             | 4 chars  | 20 chars   |

## Cross-Platform Compatibility

Smart Password Generator (C#) CLI produces **identical passwords** to:

| Platform       | Application                                                                                 |
|----------------|---------------------------------------------------------------------------------------------|
| Python CLI     | [CLI PassGen](https://github.com/smartlegionlab/clipassgen)                                 |
| Python CLI Man | [CLI PassMan](https://github.com/smartlegionlab/clipassman)                                 |
| Desktop Python | [Desktop Manager](https://github.com/smartlegionlab/smart-password-manager-desktop)         |
| Desktop C#     | [Desktop C# Manager](https://github.com/smartlegionlab/SmartPasswordManagerCsharpDesktop)   |
| Web            | [Web Manager](https://github.com/smartlegionlab/smart-password-manager-web)                 |
| Android        | [Android Manager](https://github.com/smartlegionlab/smart-password-manager-android)         |

## For Developers

### Prerequisites
- .NET 10.0 SDK or later

### Build Commands

```bash
# Arch Linux
sudo pacman -S dotnet-sdk

# Run
dotnet run --project SmartPasswordGeneratorCsharpCli/

# Build
dotnet build SmartPasswordGeneratorCsharpCli/

# Publish single file
# Windows
dotnet publish -c Release -o C:\publish-win -p:AssemblyName=SmartPasswordGeneratorCsharpCli-win-x64 -r win-x64 --self-contained true

# Linux
dotnet publish -c Release -o ~/.publish-linux/SmartPasswordGeneratorCsharpCli -p:AssemblyName=SmartPasswordGeneratorCsharpCli-linux-x64 -r linux-x64 --self-contained true
```

## Ecosystem

**Core Libraries:**
- **[smartpasslib](https://github.com/smartlegionlab/smartpasslib)** - Python
- **[smartpasslib-js](https://github.com/smartlegionlab/smartpasslib-js)** - JavaScript
- **[smartpasslib-kotlin](https://github.com/smartlegionlab/smartpasslib-kotlin)** - Kotlin
- **[smartpasslib-go](https://github.com/smartlegionlab/smartpasslib-go)** - Go
- **[smartpasslib-csharp](https://github.com/smartlegionlab/smartpasslib-csharp)** - C#

**CLI Applications:**
- **[CLI PassMan (Python)](https://github.com/smartlegionlab/clipassman)**
- **[CLI PassGen (Python)](https://github.com/smartlegionlab/clipassgen)**
- **[CLI Manager (C#)](https://github.com/smartlegionlab/SmartPasswordManagerCsharpCli)**
- **[CLI Generator (C#)](https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli)** (this)

**Desktop Applications:**
- **[Desktop Manager (Python)](https://github.com/smartlegionlab/smart-password-manager-desktop)**
- **[Desktop Manager (C#)](https://github.com/smartlegionlab/SmartPasswordManagerCsharpDesktop)**

**Other:**
- **[Web Manager](https://github.com/smartlegionlab/smart-password-manager-web)**
- **[Android Manager](https://github.com/smartlegionlab/smart-password-manager-android)**

## License

**[BSD 3-Clause License](https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli/blob/master/LICENSE)**

Copyright (©) 2026, [Alexander Suvorov](https://github.com/smartlegionlab)

## Author

**Alexander Suvorov** - [GitHub](https://github.com/smartlegionlab)

---

## Support

- **Issues**: [GitHub Issues](https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli/issues)
- **Documentation**: This [README](https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli/blob/master/README.md)

---

