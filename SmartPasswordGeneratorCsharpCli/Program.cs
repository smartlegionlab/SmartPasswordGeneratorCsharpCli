using SmartLegionLab.SmartPasswordLib;

namespace SmartPasswordGeneratorCsharpCli;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            InteractiveMode();
        }
        else
        {
            CommandMode(args);
        }
    }

    static void CommandMode(string[] args)
    {
        switch (args[0].ToLower())
        {
            case "smart":
                if (args.Length < 3)
                {
                    Console.WriteLine("ERROR: Usage: passgen smart <secret> <length>");
                    return;
                }
                string secret = args[1];
                if (secret.Length < 12)
                {
                    Console.WriteLine("ERROR: Secret must be at least 12 characters");
                    return;
                }
                if (!int.TryParse(args[2], out int length))
                {
                    Console.WriteLine("ERROR: Invalid length");
                    return;
                }
                if (length < 12 || length > 1000)
                {
                    Console.WriteLine("ERROR: Length must be between 12 and 1000");
                    return;
                }
                try
                {
                    Console.WriteLine(SmartPasswordGenerator.GenerateSmartPassword(secret, length));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
                break;

            case "strong":
                if (args.Length < 2)
                {
                    Console.WriteLine("ERROR: Usage: passgen strong <length>");
                    return;
                }
                if (!int.TryParse(args[1], out int strongLength))
                {
                    Console.WriteLine("ERROR: Invalid length");
                    return;
                }
                if (strongLength < 12 || strongLength > 1000)
                {
                    Console.WriteLine("ERROR: Length must be between 12 and 1000");
                    return;
                }
                try
                {
                    Console.WriteLine(SmartPasswordGenerator.GenerateStrongPassword(strongLength));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
                break;

            case "code":
                if (args.Length < 2)
                {
                    Console.WriteLine("ERROR: Usage: passgen code <length>");
                    return;
                }
                if (!int.TryParse(args[1], out int codeLength))
                {
                    Console.WriteLine("ERROR: Invalid length");
                    return;
                }
                if (codeLength < 4 || codeLength > 20)
                {
                    Console.WriteLine("ERROR: Length must be between 4 and 20");
                    return;
                }
                try
                {
                    Console.WriteLine(SmartPasswordGenerator.GenerateCode(codeLength));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
                break;

            case "public":
                if (args.Length < 2)
                {
                    Console.WriteLine("ERROR: Usage: passgen public <secret>");
                    return;
                }
                if (args[1].Length < 12)
                {
                    Console.WriteLine("ERROR: Secret must be at least 12 characters");
                    return;
                }
                try
                {
                    Console.WriteLine(SmartPasswordGenerator.GeneratePublicKey(args[1]));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
                break;

            case "verify":
                if (args.Length < 3)
                {
                    Console.WriteLine("ERROR: Usage: passgen verify <secret> <public_key>");
                    return;
                }
                if (args[1].Length < 12)
                {
                    Console.WriteLine("ERROR: Secret must be at least 12 characters");
                    return;
                }
                bool isValid = SmartPasswordGenerator.VerifySecret(args[1], args[2]);
                Console.WriteLine(isValid ? "VALID" : "INVALID");
                break;

            case "help":
                ShowHelp();
                break;

            default:
                Console.WriteLine("ERROR: Unknown command. Available: smart, strong, code, public, verify, help");
                break;
        }
    }

    static void InteractiveMode()
    {
        while (true)
        {
            Console.Clear();
            DrawHeader();
            DrawMenu();

            Console.Write("\nSelect option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GenerateSmartPassword();
                    break;
                case "2":
                    GenerateStrongPassword();
                    break;
                case "3":
                    GenerateCode();
                    break;
                case "4":
                    ShowPublicKey();
                    break;
                case "5":
                    VerifySecret();
                    break;
                case "6":
                    ShowHelp();
                    Console.ReadKey();
                    break;
                case "0":
                    Exit();
                    return;
                default:
                    Console.WriteLine("\nInvalid option! Press any key...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static string ReadSecret()
    {
        string secret = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                secret += key.KeyChar;
            }
            else if (key.Key == ConsoleKey.Backspace && secret.Length > 0)
            {
                secret = secret.Substring(0, secret.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return secret;
    }

    static void ShowHelp()
    {
        Console.Clear();
        int width = Console.WindowWidth;
        Console.WriteLine(new string('=', width));
        CenterText("SMART PASSWORD GENERATOR (C#) CLI");
        CenterText("Version v1.0.1");
        Console.WriteLine(new string('=', width));
        Console.WriteLine();
        Console.WriteLine("DESCRIPTION:");
        Console.WriteLine("Deterministic password generator using SmartPasswordLib");
        Console.WriteLine("Same secret + same length = same password across all platforms");
        Console.WriteLine();
        Console.WriteLine("HOW IT WORKS:");
        Console.WriteLine("1. Smart password: deterministic from secret phrase (30 iterations SHA-256)");
        Console.WriteLine("2. Strong password: cryptographically secure random");
        Console.WriteLine("3. Auth code: short random code for 2FA (4-20 chars)");
        Console.WriteLine("4. Public key: derived from secret (60 iterations SHA-256)");
        Console.WriteLine("5. Verify: check if secret matches public key");
        Console.WriteLine();
        Console.WriteLine("COMMANDS:");
        Console.WriteLine("  passgen smart <secret> <length>");
        Console.WriteLine("  passgen strong <length>");
        Console.WriteLine("  passgen code <length>");
        Console.WriteLine("  passgen public <secret>");
        Console.WriteLine("  passgen verify <secret> <public_key>");
        Console.WriteLine("  passgen help");
        Console.WriteLine();
        Console.WriteLine("REQUIREMENTS:");
        Console.WriteLine("- Secret phrase: minimum 12 characters");
        Console.WriteLine("- Smart/Strong password length: 12-1000 characters");
        Console.WriteLine("- Auth code length: 4-20 characters");
        Console.WriteLine();
        Console.WriteLine("EXAMPLES:");
        Console.WriteLine("  passgen smart \"MySecretPhrase123\" 16");
        Console.WriteLine("  passgen strong 20");
        Console.WriteLine("  passgen code 8");
        Console.WriteLine("  passgen public \"MySecretPhrase123\"");
        Console.WriteLine("  passgen verify \"MySecretPhrase123\" a1b2c3d4...");
        Console.WriteLine();
        Console.WriteLine("LINKS:");
        Console.WriteLine("Repo: https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli");
        Console.WriteLine("Core Lib: https://github.com/smartlegionlab/smartpasslib-csharp");
        Console.WriteLine("License: BSD 3-Clause");
        Console.WriteLine("Author: Alexander Suvorov");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void DrawHeader()
    {
        int width = Console.WindowWidth;
        Console.WriteLine(new string('=', width));
        CenterText("SMART PASSWORD GENERATOR (C#) CLI");
        CenterText($"Version: v1.0.1");
        Console.WriteLine(new string('=', width));
        Console.WriteLine();
    }

    static void DrawMenu()
    {
        Console.WriteLine(" MAIN MENU");
        Console.WriteLine(" 1. Generate Smart Password");
        Console.WriteLine(" 2. Generate Strong Random Password");
        Console.WriteLine(" 3. Generate Auth Code");
        Console.WriteLine(" 4. Show Public Key");
        Console.WriteLine(" 5. Verify Secret");
        Console.WriteLine(" 6. Help");
        Console.WriteLine(" 0. Exit");
    }

    static void Exit()
    {
        Console.Clear();
        int width = Console.WindowWidth;
        Console.WriteLine(new string('=', width));
        CenterText("SMART PASSWORD GENERATOR (C#) CLI");
        CenterText($"Version: v1.0.1");
        Console.WriteLine(new string('=', width));
        Console.WriteLine();
        CenterText("https://github.com/smartlegionlab/SmartPasswordGeneratorCsharpCli");
        CenterText("Alexander Suvorov | BSD 3-Clause");
        Console.WriteLine(new string('=', width));
        Console.WriteLine();
        CenterText("Press any key to exit...");
        Console.ReadKey();
    }

    static void GenerateSmartPassword()
    {
        Console.Clear();
        DrawHeader();

        Console.WriteLine(" GENERATE SMART PASSWORD");
        Console.WriteLine(" (Secret must be at least 12 characters)");
        Console.Write(" Enter secret phrase (input hidden): ");
        string secret = ReadSecret();

        if (secret.Length < 12)
        {
            Console.WriteLine("\n ERROR: Secret must be at least 12 characters! Press any key...");
            Console.ReadKey();
            return;
        }

        Console.Write(" Enter password length (12-1000): ");
        if (!int.TryParse(Console.ReadLine(), out int length))
        {
            Console.WriteLine("\n ERROR: Invalid length! Press any key...");
            Console.ReadKey();
            return;
        }

        if (length < 12 || length > 1000)
        {
            Console.WriteLine("\n ERROR: Length must be between 12 and 1000! Press any key...");
            Console.ReadKey();
            return;
        }

        try
        {
            string password = SmartPasswordGenerator.GenerateSmartPassword(secret, length);
            Console.WriteLine($"\n Generated Password:\n {password}\n");
            Console.WriteLine(" Press any key to continue...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n ERROR: {ex.Message}");
            Console.ReadKey();
        }
    }

    static void GenerateStrongPassword()
    {
        Console.Clear();
        DrawHeader();

        Console.WriteLine(" GENERATE STRONG RANDOM PASSWORD");
        Console.WriteLine(" (Password length must be between 12 and 1000)");
        Console.Write(" Enter password length: ");

        if (!int.TryParse(Console.ReadLine(), out int length))
        {
            Console.WriteLine("\n ERROR: Invalid length! Press any key...");
            Console.ReadKey();
            return;
        }

        if (length < 12 || length > 1000)
        {
            Console.WriteLine("\n ERROR: Length must be between 12 and 1000! Press any key...");
            Console.ReadKey();
            return;
        }

        try
        {
            string password = SmartPasswordGenerator.GenerateStrongPassword(length);
            Console.WriteLine($"\n Generated Password:\n {password}\n");
            Console.WriteLine(" Press any key to continue...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n ERROR: {ex.Message}");
            Console.ReadKey();
        }
    }

    static void GenerateCode()
    {
        Console.Clear();
        DrawHeader();

        Console.WriteLine(" GENERATE AUTH CODE");
        Console.WriteLine(" (Code length must be between 4 and 20)");
        Console.Write(" Enter code length: ");

        if (!int.TryParse(Console.ReadLine(), out int length))
        {
            Console.WriteLine("\n ERROR: Invalid length! Press any key...");
            Console.ReadKey();
            return;
        }

        if (length < 4 || length > 20)
        {
            Console.WriteLine("\n ERROR: Length must be between 4 and 20! Press any key...");
            Console.ReadKey();
            return;
        }

        try
        {
            string code = SmartPasswordGenerator.GenerateCode(length);
            Console.WriteLine($"\n Generated Code:\n {code}\n");
            Console.WriteLine(" Press any key to continue...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n ERROR: {ex.Message}");
            Console.ReadKey();
        }
    }

    static void ShowPublicKey()
    {
        Console.Clear();
        DrawHeader();

        Console.WriteLine(" SHOW PUBLIC KEY");
        Console.WriteLine(" (Secret must be at least 12 characters)");
        Console.Write(" Enter secret phrase (input hidden): ");
        string secret = ReadSecret();

        if (secret.Length < 12)
        {
            Console.WriteLine("\n ERROR: Secret must be at least 12 characters! Press any key...");
            Console.ReadKey();
            return;
        }

        try
        {
            string publicKey = SmartPasswordGenerator.GeneratePublicKey(secret);
            Console.WriteLine($"\n Public Key:\n {publicKey}\n");
            Console.WriteLine(" Press any key to continue...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n ERROR: {ex.Message}");
            Console.ReadKey();
        }
    }

    static void VerifySecret()
    {
        Console.Clear();
        DrawHeader();

        Console.WriteLine(" VERIFY SECRET");
        Console.WriteLine(" (Secret must be at least 12 characters)");
        Console.Write(" Enter secret phrase (input hidden): ");
        string secret = ReadSecret();

        if (secret.Length < 12)
        {
            Console.WriteLine("\n ERROR: Secret must be at least 12 characters! Press any key...");
            Console.ReadKey();
            return;
        }

        Console.Write(" Enter public key: ");
        string publicKey = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(publicKey))
        {
            Console.WriteLine("\n ERROR: Public key cannot be empty! Press any key...");
            Console.ReadKey();
            return;
        }

        try
        {
            bool isValid = SmartPasswordGenerator.VerifySecret(secret, publicKey);
            Console.WriteLine($"\n Result: {(isValid ? "VALID" : "INVALID")}\n");
            Console.WriteLine(" Press any key to continue...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n ERROR: {ex.Message}");
            Console.ReadKey();
        }
    }

    static void CenterText(string text)
    {
        int windowWidth = Console.WindowWidth;
        int padding = (windowWidth - text.Length) / 2;

        if (padding > 0)
            Console.WriteLine(text.PadLeft(padding + text.Length));
        else
            Console.WriteLine(text);
    }
}