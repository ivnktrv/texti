/*

    _________ ______ _    _ _________  o
    |___ ___| |  . | \\  // |___ ___| ___
       | |    | ___|  \\//     | |    | |
       | |    | |__   //\\     | |    | |
       |_|    |____| //  \\    |_|    |_|

*/
namespace textiMain;

class Texti
{
    static void Main()
    {
        displayLogo();

        Console.Write("Укажите название файла или его путь: ");
        string path = Console.ReadLine();

        while (true)
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine("\x1b[3J");

            try
            {
                using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    byte[] readBuffer = new byte[stream.Length];
                    stream.Read(readBuffer, 0, readBuffer.Length);
                    string getText = System.Text.Encoding.UTF8.GetString(readBuffer);

                    List<char> symbols = new List<char>(getText.ToCharArray());

                    FileInfo fileInfo = new FileInfo(path);

                    Console.WriteLine(//$"\t\t\t  {fileInfo.DirectoryName}\\{fileInfo.Name}\n\n"
                        getText +
                    "\n──────────────────────────────────────────────────────────────────────────────────\n" +
                    $"   Delete - очистить всё     Home - путь до файла     Esc - сохранить и выйти"
                    );

                    ConsoleKeyInfo key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Delete)
                    {
                        stream.Close();
                        File.WriteAllText(path, string.Empty);
                    }

                    else if (key.Key == ConsoleKey.Home)
                    {
                        Console.WriteLine($"\nПуть до файла: {fileInfo.DirectoryName}\\{fileInfo.Name}");
                        Console.ReadKey();
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        byte[] writeBuffer = System.Text.Encoding.UTF8.GetBytes("\n");
                        stream.Write(writeBuffer, 0, writeBuffer.Length);
                    }

                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        stream.Close();

                        File.WriteAllText(path, string.Empty);

                        try
                        {
                            symbols.RemoveAt(symbols.Count - 1);
                        }
                        catch (ArgumentOutOfRangeException) {}

                        string tmp = new string(symbols.ToArray());

                        using (FileStream s = new FileStream(path, FileMode.Open, FileAccess.Write))
                        {
                            byte[] writeBuffer = System.Text.Encoding.UTF8.GetBytes(tmp);
                            s.Write(writeBuffer, 0, writeBuffer.Length);
                        }
                    }

                    else if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }

                    else
                    {
                        byte[] writeBuffer = System.Text.Encoding.UTF8.GetBytes(key.KeyChar.ToString());
                        stream.Write(writeBuffer, 0, writeBuffer.Length);
                    }
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Не указан путь!");
                Console.ReadLine();

                break;
            }         
        }
    }

    static void displayLogo()
    {
        string logo =
 """
     _________ ______ _    _ _________  o
     |___ ___| |  . | \\  // |___ ___| ___
        | |    | ___|  \\//     | |    | |
        | |    | |__   //\\     | |    | |
        |_|    |____| //  \\    |_|    |_|

               текстовый редактор
         
               
     """;
        Console.WriteLine(logo);
    }
}