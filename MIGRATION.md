# Migration Guide: v1.0.3 to v4.0.0

> **📌 Version Note:** SmartPasswordGeneratorCsharpCli jumps from v1.0.3 directly to v4.0.0 to align with smartpasslib-csharp v4.0.0. All smartpasslib implementations (Python, C#, JS, Go, Kotlin) now share the same version number and algorithm.

## ⚠️ Breaking Change Notice

**SmartPasswordGeneratorCsharpCli v4.0.0 is NOT backward compatible with v1.0.3**

| Version    | Status      | Why                                                                    |
|------------|-------------|------------------------------------------------------------------------|
| v1.0.3     | Deprecated  | Used old smartpasslib-csharp (fixed iterations, limited charset)       |
| **v4.0.0** | **Current** | Uses smartpasslib-csharp v4.0.0 (dynamic iterations, expanded charset) |

Smart passwords generated with v1.0.3 cannot be regenerated using v4.0.0 due to fundamental changes in the deterministic generation algorithm.

---

## Why the change?

- **Dynamic iteration counts** — deterministic steps vary per secret (15-30 for private, 45-60 for public)
- **Expanded character set** — Google-compatible symbols (`!@#$%^&*()_+-=[]{};:,.<>?/` + letters + digits)
- **Stricter validation** — secret phrases must be at least 12 characters; max password length changed from 1000 to 100; max code length changed from 20 to 100.
- **Enhanced security** — salt separation for keys, no secret exposure in iterations.

---

## What changed in the CLI?

| Aspect                  | v1.0.3       | v4.0.0                |
|-------------------------|--------------|-----------------------|
| smart/strong max length | 1000         | **100**               |
| code max length         | 20           | **100**               |
| Secret validation       | none (min 4) | **min 12 characters** |
| Version displayed       | 1.0.3        | **4.0.0**             |

---

## Migration Steps

### Step 1: Retrieve existing passwords

Before upgrading, use the **old version** (v1.0.3) to generate all passwords you need. Copy them to a safe place.

### Step 2: Upgrade to v4.0.0

Download the new binary or build from source. Replace the old executable.

### Step 3: Generate new passwords

Using the **same secret phrases and lengths**, generate new passwords.

### Step 4: Update your services

Replace the old passwords stored on websites/services with the newly generated ones.

### Step 5: Verify

Log in using the new passwords. Confirm that regeneration works (same secret → same password).

---

## Important Notes

- **No automatic migration** — manual regeneration of each smart password is required.
- **Your secret phrases remain the same** — only the generated passwords change.
- **Secret phrases shorter than 12 characters will now be rejected**.
- **Passwords generated with `strong` or `code` commands are affected (length limits changed), but you can generate new random ones anytime.**
- Test with non-essential accounts first.

---

## Need Help?

- **Issues**: [GitHub Issues](https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli/issues)
- **Core Library**: [smartpasslib-csharp](https://github.com/smartlegionlab/smartpasslib-csharp)

---

