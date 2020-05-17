using Telegram.Bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;
using System.IO.Compression;
using System.Reflection;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace ConsoleApp1
{
    class Program
    {
        static bool newMsg = false;
        static WebClient webClient = new WebClient();
        static string source;
        static char[] invalidСharacters = { '~', '@', '#', '$', '%', '^', '-', '_', '(', ')', '{', '}', '`', '+', '=', '[', ']', ':',
            ',', ';',',', '.', '/', '?','/','\\',':','*','?','«','»','<','>','|','&','—'};
        static List<string> text_tags = new List<string>() { "#habr", "#it" };
        static string botToken = "1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy"; //some your token //example value
        static int userAndChatID = 1234567890;//some your id//example value
        static TelegramBotClient bot = new TelegramBotClient(botToken);
        static string LinuxPath = "//media//pi//f//books//"; //f - usb flash
        static string WindowsPath = Directory.GetCurrentDirectory() + "\\" + "books\\";
        static bool IsBook = false;



        static string Log(string name)
        {
            string LogFile;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LogFile = "//media//pi//f//URLLogs.txt";
            }
            else
            {
                LogFile = Directory.GetCurrentDirectory() + "\\URLLogs.txt";
            }
            if (!File.Exists(LogFile))
            {
                using (File.Create(LogFile)) { };
            }
            if (name != "")
            {
                byte[] WebPageDataArr;
                try
                {
                    WebPageDataArr = Encoding.Default.GetBytes(name + "\n");
                    using (StreamWriter logFile = File.AppendText(LogFile))
                    {
                        logFile.WriteLine(name + "\n");
                    }
                    Console.WriteLine("add to log");
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }

            return LogFile;
        }
        static async void RenameAndSave()
        {
            string PageTitle = System.Text.RegularExpressions.Regex.Match(source, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase).Groups["Title"].Value;


            byte[] WebPageTitleArr = Encoding.Default.GetBytes(PageTitle);
            PageTitle = Encoding.UTF8.GetString(WebPageTitleArr);

            if (PageTitle == "")
            {
                PageTitle = "article " + DateTime.Now;
            }
            if (PageTitle.Length > 70)
            {
                Console.WriteLine("too long title");
                PageTitle = PageTitle.Remove(70, PageTitle.Length - 71);
                await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), PageTitle + "... has too long name. The name was cropped.File save.");
            }
            for (int i = 0; i < invalidСharacters.Length; i++)
            {
                PageTitle = PageTitle.Replace(invalidСharacters[i], ' ');
            }
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    System.IO.File.Move(LinuxPath + "111.html", LinuxPath + PageTitle + ".html");
                    File.Delete(LinuxPath + "111.html");
                }
                else
                {
                    System.IO.File.Move(WindowsPath + "111.html", WindowsPath + PageTitle + ".html");
                    File.Delete(WindowsPath + "111.html");
                }
                await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), PageTitle + " is saved");
                Log(PageTitle);
                //  var t = await bot.SendTextMessageAsync("@MalinkaLibraryBot", PageTitle + "is saved");
            }
            catch (Exception ex)
            {
                string oldTitle = PageTitle;

                Console.WriteLine(ex.Message);
                // await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "error: " + ex.Message);
                PageTitle = "article " + DateTime.Now;

                for (int i = 0; i < invalidСharacters.Length; i++)
                {
                    PageTitle = PageTitle.Replace(invalidСharacters[i], ' ');
                }
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    System.IO.File.Move(LinuxPath + "111.html", LinuxPath + PageTitle + ".html");
                    await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "file was rename and saved");
                }
                else
                {
                    System.IO.File.Move(WindowsPath + "111.html", WindowsPath + PageTitle + ".html");
                    await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "file was rename and saved");
                }
                Log(oldTitle + " There was an attempt to save the article with the same name - the file was saved again with the new name(" + PageTitle + ")");
            }
        }
        static async Task Main(string[] args)
        {

            TcpListener clientListener = new TcpListener(8024);
            clientListener.Start();


            bot.StartReceiving();//начать принимать запросы  

            bot.OnMessage += SavePageQuery;// сохранение - напрямую - через бота
                                           //start messege
            await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "server started");
            HelloMessageAsync();
            TcpClient client = clientListener.AcceptTcpClient(); // сохранение через приложение сервера
            NetworkStream readerStream = client.GetStream();
            var BotInformation = bot.GetMeAsync().Result;


            BinaryFormatter outformat;
            FileStream fs;
            BinaryWriter bw;
            outformat = new BinaryFormatter();

            await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "connection with remote client established.");
            while (true)
            {
                // bot.GetUpdatesAsync().ToString();
                //linux
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    if (!Directory.Exists(LinuxPath))
                    {
                        Directory.CreateDirectory(LinuxPath);
                    }
                    fs = new FileStream(LinuxPath + "111.html", FileMode.OpenOrCreate);
                }
                else
                {
                    if (!Directory.Exists(WindowsPath))
                    {
                        Directory.CreateDirectory(WindowsPath);
                    }
                    fs = new FileStream(WindowsPath + "111.html", FileMode.OpenOrCreate);
                }
                bw = new BinaryWriter(fs);
                readerStream = client.GetStream();

                try
                {


                    byte[] buf = (byte[])(outformat.Deserialize(readerStream));
                    bw.Write(buf);
                }
                catch (Exception ex)
                {
                    await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "article is not saved " + ex.Message);
                    clientListener.Stop();

                    clientListener.Start();
                    client = clientListener.AcceptTcpClient();
                    //readerStream = client.GetStream();
                    //outformat = new BinaryFormatter();
                }
                bw.Close();
                fs.Close();



                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    source = webClient.DownloadString(LinuxPath + "111.html");
                }
                else
                {
                    source = webClient.DownloadString(WindowsPath + "111.html");
                }
                /////
                RenameAndSave();
                ///////


                //Console.ReadLine();
                //bw.Close();
                //fs.Close();
                client = clientListener.AcceptTcpClient();
            }
        }


        static string GetBooksList()
        {
            string path = "";
            string slash = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                path = "//media//pi//f//books//";
                slash = "//";
            }
            else
            {
                path = Directory.GetCurrentDirectory() + "\\" + "books\\";
                slash = "\\";
            }
            string[] dir = Directory.GetDirectories(path);
            string[] files;
            int dirCount = 1;
            int FileCount = 100;
            string dirList = "";
            for (int i = 0; i < dir.Length; i++)
            {

                dirList += "@" + dirCount + " " + dir[i] + slash + "\n" + "\n";
                dirCount++;
                //get files list

                files = Directory.GetFiles(dir[i]);
                for (int j = 0; j < files.Length; j++)
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        dirList += FileCount + " " + files[j].Substring(files[j].LastIndexOf('/')) + "\n";
                        //  dirList += FileCount + " " + files[j].Substring(files[j].LastIndexOf(dir[i])) + "\n";

                    }
                    else
                    {
                        dirList += FileCount + " " + files[j].Substring(files[j].LastIndexOf(slash)) + "\n";
                    }
                    FileCount++;
                }
                dirList += "\n";
            }
            //in root directory
            //0 - root directory

            dirList += "@" + 0 + " " + path + "\n" + "\n";

            files = Directory.GetFiles(path);


            for (int j = 0; j < files.Length; j++)
            {

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    dirList += FileCount + " " + files[j].Substring(files[j].LastIndexOf("//")) + "\n";
                }
                else
                {
                    dirList += FileCount + " " + files[j].Substring(files[j].LastIndexOf("\\")) + "\n";
                }
                FileCount++;
            }
            dirList += "\n";

            bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), dirList);
            return dirList;
        }

        static async Task<bool> MessageIsCommandAsync(MessageEventArgs e)
        {
            string[] directories;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                directories = Directory.GetDirectories(LinuxPath);
            }
            else
            {
                directories = Directory.GetDirectories(WindowsPath);
            }
            if (directories.Length > 0)
            {
                for (int i = 0; i < directories.Length; i++)
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        text_tags.Add(directories[i].Substring(directories[i].LastIndexOf("//")).Replace("//", "#"));
                    }
                    else
                    {
                        text_tags.Add(directories[i].Substring(directories[i].LastIndexOf("\\")).Replace("\\", "#"));
                    }
                }
                for (int i = 0; i < text_tags.Count; i++)
                {
                    if (e.Message.Text.StartsWith(text_tags[i])) //one of the existing directories is indicated
                    {

                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                        {
                            LinuxPath = LinuxPath + text_tags[i].Remove(0, 1) + "//";
                            if (!Directory.Exists(LinuxPath))
                            {
                                Directory.CreateDirectory(LinuxPath);
                            }
                        }
                        else
                        {
                            WindowsPath = WindowsPath + text_tags[i].Remove(0, 1) + "\\"; //Remove(0, 1) - delete #
                            if (!Directory.Exists(WindowsPath))
                            {
                                Directory.CreateDirectory(WindowsPath);
                            }
                        }


                        return true;
                    }
                }
            }
            if (e.Message.Text.StartsWith("#"))//create new direcrory
            {

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    LinuxPath = "//media//pi//f//books//" + e.Message.Text.Substring(0, e.Message.Text.IndexOf(" ")).Remove(0, 1);
                    if (!Directory.Exists(LinuxPath))
                    {
                        Directory.CreateDirectory(LinuxPath);
                        Console.WriteLine("new directory was create");
                    }
                    LinuxPath += "//";
                }
                else
                {
                    WindowsPath = Directory.GetCurrentDirectory() + "\\" + "books\\" + e.Message.Text.Substring(0, e.Message.Text.IndexOf(" ")).Remove(0, 1);
                    if (!Directory.Exists(WindowsPath))
                    {
                        Directory.CreateDirectory(WindowsPath);
                        Console.WriteLine("new directory was create");
                    }
                    WindowsPath += "\\";
                }


                return true;
            }
            if (e.Message.Text.StartsWith("$"))//command
            {
                switch (e.Message.Text)
                {
                    case "$help":
                        await HelloMessageAsync();
                        break;
                    case "$books_log":


                        string log = File.ReadAllText(Log(""));
                        if (log.Length > 0)
                        {
                            await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "log file:\n\n" + log);
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "log is empty.Searches for files in the storage. Records about found files will be added to the log file.");
                            log = File.ReadAllText(Log(GetBooksList()));
                            await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "log file:\n\n" + log);
                        }
                        break;
                    case "$books_list":
                        GetBooksList();
                        break;

                    case "$zip":
                        await CreateZipArchiveAsync();
                        break;
                    case "$backup":
                        e.Message.Text = "$";
                        await CreateBackUpAsync();
                        break;
                    case "$erase":
                        await CreateBackUpAsync();
                        await DeleteAllFAsync(e);
                        break;
                    case "$erase-w":
                        await DeleteAllFAsync(e);
                        break;
                    //case "$reset":
                    //    e.Message.Text = "$";
                    //    RestartApp(e);
                    //    break;
                    default:
                        if (e.Message.Text.StartsWith("$download"))
                        {
                            string[] fileFormats = { "pdf", "fb2", "mobi", "epub", "txt", "docx", "doc", "zip", "rar" };
                            string pathToFile = GetPathToSpecificF(e.Message.Text.Substring(e.Message.Text.IndexOf(" ") + 1), e);
                            try
                            {
                                if (File.Exists(pathToFile))
                                {
                                    // Console.WriteLine("file exist"); //debug
                                    using (var stream = File.OpenRead(pathToFile))
                                    {
                                        InputOnlineFile iof = new InputOnlineFile(stream);
                                        for (int i = 0; i < fileFormats.Length; i++)
                                        {
                                            if (IsBook || bookPath.Substring(bookPath.LastIndexOf(".")).Contains(fileFormats[i]))
                                            {
                                                iof.FileName = bookPath;
                                                break;
                                            }
                                            else
                                            {
                                                iof.FileName = bookPath + ".html";
                                            }
                                        }


                                        await bot.SendDocumentAsync(new Telegram.Bot.Types.ChatId(userAndChatID), iof, bookPath);
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("file NOT exist");
                                    await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "file not exist");

                                }

                                //bot.SendDocumentAsync(new Telegram.Bot.Types.ChatId(userAndChatID), new Telegram.Bot.Types.InputFiles.InputOnlineFile(pathToFile));

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        if (e.Message.Text.StartsWith("$delete"))
                        {
                            string pathToFile = GetPathToSpecificF(e.Message.Text.Substring(e.Message.Text.IndexOf(" ") + 1), e);
                            if (File.Exists(pathToFile))
                            {
                                e.Message.Text = "$";
                                File.Delete(pathToFile);
                                await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "file was delete");

                            }
                            else
                            {
                                Console.WriteLine("file NOT exist");
                                await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "file not exist");

                            }
                        }
                        break;
                }

                return false;
            }
            if (e.Message.Text == "/start")
            {
                await HelloMessageAsync();
                return true;
            }
            return false;

        }

        private static async Task CreateBackUpAsync()
        {

            await CreateZipArchiveAsync();
            Random random1 = new Random(DateTime.UtcNow.Minute);
            Random random2 = new Random(DateTime.UtcNow.Millisecond);
            string hash = System.Convert.ToString(Math.Abs(DateTime.UtcNow.GetHashCode() ^ random1.Next() ^ random2.Next()));
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                if (!Directory.Exists("//media//pi//f//backup"))
                {
                    Directory.CreateDirectory("//media//pi//f//backup");
                }
                File.Move("//media//pi//f//books.zip", "//media//pi//f//backup//books " + hash + ".zip");
            }
            else
            {
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\" + "backup"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\" + "backup");
                }

                File.Move(Directory.GetCurrentDirectory() + "\\" + "books.zip", Directory.GetCurrentDirectory() + "\\" + "backup" + "\\" + "books" + hash + ".zip");
            }
            await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "backup created - backup name: books " + hash + ".zip");

        }

        //private static void RestartApp(MessageEventArgs e)
        //{
        //    if (e.Message.Text == "$reset")
        //    {
        //        e.Message.Text = "";
        //        //  Process.Start(Assembly.GetExecutingAssembly().Location);
        //        Process p = Process.GetCurrentProcess();
        //        p.Kill();
        //        Environment.Exit(0);
        //    }
        //}

        private static async Task DeleteAllFAsync(MessageEventArgs e)
        {
            string path;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                path = "//media//pi//f//books//";
                if (Directory.Exists(path))
                {
                    e.Message.Text = "$";
                    Console.WriteLine("erase all start");
                    try
                    { // Directory.Delete(path); - not working in mono // mono bug
                        //https://github.com/mono/mono/issues/7117
                        //https://github.com/mono/mono/issues/6660
                        //http://community.monogame.net/t/string-conversion-error-illegal-byte-sequence-encounted-in-the-input/11034 !!!
                        string[] files = Directory.GetFiles(path);
                        string[] subDir = Directory.GetDirectories(path);
                        //in root directory
                        for (int i = 0; i < files.Length; i++)
                        {
                            File.Delete(files[i]);
                        }
                        //in subdirictories
                        for (int i = 0; i < subDir.Length; i++)
                        {
                            files = Directory.GetFiles(subDir[i]);
                            for (int j = 0; j < files.Length; j++)
                            {
                                File.Delete(files[i]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("directory not delete " + ex.Message);
                    }

                    Console.WriteLine("erase all end");
                    // Directory.CreateDirectory("//media//pi//f//books//");
                    // Console.WriteLine("new dirictory was create");
                    await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "all files deleted");

                }
            }
            else
            {
                path = Directory.GetCurrentDirectory() + "\\" + "books";
                if (Directory.Exists(path))
                {
                    e.Message.Text = "$";
                    Console.WriteLine("erase all start");
                    string[] files = Directory.GetFiles(path);
                    string[] subDir = Directory.GetDirectories(path);
                    for (int i = 0; i < files.Length; i++)
                    {
                        File.Delete(files[i]);
                    }
                    for (int i = 0; i < subDir.Length; i++)
                    {
                        files = Directory.GetFiles(subDir[i]);
                        for (int j = 0; j < files.Length; j++)
                        {
                            File.Delete(files[i]);
                        }
                    }


                    Console.WriteLine("erase all end");
                    await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "all files deleted");
                }
            }


        }

        private static async Task CreateZipArchiveAsync()
        {
            //create zip
            string zipFile = "";
            string[] files;
            string[] dirictories;
            string slash = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                files = Directory.GetFiles(LinuxPath);
                dirictories = Directory.GetDirectories(LinuxPath);
                zipFile = "//media//pi//f//books.zip";
                slash = "//";
            }
            else
            {
                files = Directory.GetFiles(WindowsPath);
                dirictories = Directory.GetDirectories(WindowsPath);
                zipFile = Directory.GetCurrentDirectory() + "\\" + "books.zip";
                slash = "\\";
            }
            if (File.Exists(zipFile))
            {
                File.Delete(zipFile);
            }

            using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
            {
                if (files.Length > 0)
                {
                    foreach (var fPath in files)
                    {
                        archive.CreateEntryFromFile(fPath, Path.GetFileName(fPath));
                    }

                }
                if (dirictories.Length > 0)
                {
                    string SubDirName;
                    for (int i = 0; i < dirictories.Length; i++)
                    {
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                        {
                            SubDirName = dirictories[i].Substring(dirictories[i].LastIndexOf(slash) + 2);
                        }
                        else
                        {
                            SubDirName = dirictories[i].Substring(dirictories[i].LastIndexOf(slash) + 1);
                        }

                        //Console.WriteLine(dirictories[i]);
                        //Console.WriteLine();
                        Console.WriteLine(SubDirName);

                        if (File.Exists(SubDirName + ".zip"))
                        {
                            File.Delete(SubDirName + ".zip");
                        }

                        ZipFile.CreateFromDirectory(dirictories[i] + slash, SubDirName + ".zip");
                        archive.CreateEntryFromFile(SubDirName + ".zip", SubDirName + ".zip");
                    }

                }
            }

            //send zip

            using (var stream = File.OpenRead(zipFile))
            {
                InputOnlineFile iof = new InputOnlineFile(stream);
                iof.FileName = "all articles" + ".zip";
                await bot.SendDocumentAsync(new Telegram.Bot.Types.ChatId(userAndChatID), iof, "all articles");
            }
        }

        static string bookPath = "";
        private static string GetPathToSpecificF(string v, MessageEventArgs e)
        {
            string dirList = GetBooksList();
            // Console.WriteLine(v.Substring(v.LastIndexOf(" ") + 1));

            // Console.WriteLine(dirList.Substring(dirList.IndexOf(v[0]), dirList.IndexOf("\n") - dirList.IndexOf(v[0])).Remove(0, 2)); //ok
            //  Console.WriteLine(dirList);
            string bookNumber = v.Substring(v.LastIndexOf(" ") + 1);
            string dirNumber = v.Remove(v.IndexOf(" "));
            string dirPath = "";

            // Console.WriteLine(bookNumber);
            var arr = dirList.Split('\n');
            string d = "";
            string slash = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) //linux os
            {
                slash = "//";
            }
            else
            {
                slash = "\\";
            }
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != "")
                {


                    Console.WriteLine(arr[i]);
                    if (arr[i].StartsWith(bookNumber))
                    {
                        bookPath = (arr[i].Remove(0, arr[i].LastIndexOf(slash) + 1)).Replace(bookNumber + " ", "");
                        Console.WriteLine("book path: " + bookPath);
                    }

                    if (arr[i].StartsWith("@"))
                    {
                        d = arr[i].Remove(arr[i].IndexOf('@') + arr[i].IndexOf(" "));
                        Console.WriteLine("dL " + d);
                        if (d == dirNumber)
                        {
                            dirPath = (arr[i]);

                        }
                    }


                }

            }
            //  Console.WriteLine(dirList.Substring(dirList.IndexOf(bookNumber),).Remove(0, 5)); //ok
            Console.WriteLine("--------------------------");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) //linux os
            {
                Console.WriteLine(dirPath.Remove(0, 3) + "//" + bookPath);
                return dirPath.Remove(0, 3) + "//" + bookPath;
            }
            else
            {

                Console.WriteLine(dirPath.Remove(0, 3) + "\\" + bookPath);
                return dirPath.Remove(0, 3) + "\\" + bookPath;
            }
        }
        private static async Task HelloMessageAsync()
        {
            await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "send me some commands:\n $help - to see commands list\n\n" +
                                              "$books_log - to see list of saved books and articles (from log file)\n\n" +
                                              " $books_list - to see list of saved books and articles(get files name from storage)\n\n " +
                                              "$download - to download some books and articles from storage\n" + "example:\n$download<space>@<folder number><space><file number>\n\n" +
                                              "$zip - Get (download) a copy of all files from storage. You will receive a zip archive with all the files.\n\n" +
                                             "$delete - delete specific file at storage\n" + "example:\n$delete<space>@<folder number><space><file number>\n\n" +
                                              "$erase - delete all files at storge(a backup will be automatically created).\n\n" +
                                              "$erase-w - delete all files at storge(without backup).\n\n" +
                                              "$backup - create a backup copy in the special backup folder at storage, and send you a copy.\n\n" +
                                              "or send me a link to a web page that you want to save in the storage as an HTML file." +
                                              "You can also send me next file formats file:\n" + "pdf,djvu,fb2,epub,mobi,txt,docx,doc,zip,rar,jpeg,gif,png\n" + "less 20 mb size."
                                              + "\n when you send image - it can have small size after saving. Type 'resize' at image caption (without quotes) for resize image."
                                            //  "$reset - restart aplication"
                                            );
        }
        private static async void SavePageQuery(object sender, MessageEventArgs e)
        {
            string filePath = "";
            string url = "";
            try
            {
                var user = e.Message.From;

                if (user.Id == userAndChatID)
                {

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) //linux os
                    {

                        if (!Directory.Exists(LinuxPath))
                        {
                            Directory.CreateDirectory("//media//pi//f//books");
                        }
                        bool x = await MessegeIsBookAsync(e);
                        if (x)
                        {
                            IsBook = true;

                            Console.WriteLine("some book");
                            return;
                        }
                        else if (e.Message.Text == null)
                        {
                            IsBook = false;
                            await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), " file have too big size or wrong book format. You can send me book file less 20 mb in next format:\n pdf\ndjvu\nfb2\nepub\nmobi\ntxt\ndocx\ndoc\nzip\nrar");
                            return;
                        }
                        else if (MessageIsCommandAsync(e).Result)
                        {
                            filePath = LinuxPath + "111.html";
                        }
                        else // message - isnt command
                        {
                            LinuxPath = "//media//pi//f//books//"; //restore original path
                            filePath = LinuxPath + "111.html";
                        }

                    }
                    else //windows os
                    {
                        if (!Directory.Exists(WindowsPath))
                        {
                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\" + "books");
                        }
                        bool x = await MessegeIsBookAsync(e);
                        if (x)
                        {
                            IsBook = true;

                            Console.WriteLine("some book");
                            return;
                        }
                        else if (e.Message.Text == null)
                        {
                            IsBook = false;
                            await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), " file have too big size or wrong book format. You can send me book file less 20 mb in next format:\n pdf\ndjvu\nfb2\nepub\nmobi\ntxt\ndocx\ndoc\nzip\nrar");
                            return;
                        }
                        else if (MessageIsCommandAsync(e).Result)
                        {
                            filePath = WindowsPath + "111.html";

                        }
                        else // message - isnt command
                        {
                            WindowsPath = Directory.GetCurrentDirectory() + "\\" + "books\\"; //restore original path
                            filePath = WindowsPath + "111.html";
                        }
                    }


                    if (e.Message.Text.Contains("https://"))
                    {
                        IsBook = false;
                        //if (File.Exists(filePath))//last session or aborted session wrong file - "111.html"
                        //{
                        //    File.Delete(filePath);
                        //}
                        url = e.Message.Text.Substring(e.Message.Text.IndexOf("https://"));
                        await Task.Delay(123);
                        webClient.DownloadFile(url, filePath);
                        source = webClient.DownloadString(filePath);
                        RenameAndSave();
                    }
                    else if (e.Message.Text.Contains("http://"))
                    {
                        IsBook = false;
                        //if (File.Exists(filePath))//last session or aborted session wrong file - "111.html"
                        //{
                        //    File.Delete(filePath);
                        //}
                        url = e.Message.Text.Substring(e.Message.Text.IndexOf("http://"));
                        await Task.Delay(123);
                        webClient.DownloadFile(url, filePath);
                        source = webClient.DownloadString(filePath);
                        RenameAndSave();
                    }
                    else if (e.Message.Text.Contains("$") || e.Message.Text == "/start")
                    {
                    }
                    else
                    {
                        await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "article is not saved - wrong URL");

                    }

                }
                else
                {
                    await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(e.Message.Chat.Id), "you cant send message to this bot");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "article is not saved " + ex.Message + "\n" + ex.StackTrace);
            }
            //Console.WriteLine(msg);
        }

        private static async Task<bool> MessegeIsBookAsync(MessageEventArgs e)
        {
            if (e.Message.Document != null)
            {
                if (e.Message.Document.FileSize > 20971520) //20971520 == 20 mb - 20 mb in byte
                {
                    await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "book is not saved - too large file. Send file less 20 mb.");

                    return false;
                }

                if (e.Message.Document.MimeType == "application/pdf"
                    || e.Message.Document.MimeType == "image/vnd.djvu"
                    || e.Message.Document.MimeType == "application/x-fictionbook+xml"
                    || e.Message.Document.MimeType == "application/epub+zip"
                    || e.Message.Document.MimeType == "application/x-mobipocket-ebook"
                    || e.Message.Document.MimeType == "text/plain"
                    || e.Message.Document.MimeType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                    || e.Message.Document.MimeType == "application/msword"
                    || e.Message.Document.MimeType == "application/zip"
                    || e.Message.Document.MimeType == "application/vnd.rar")
                {
                    //"application/pdf" - pdf
                    // "image/vnd.djvu" - djvu
                    // "application/x-fictionbook+xml" - fb2
                    //"application/epub+zip" - epub
                    //"application/x-mobipocket-ebook" - mobi
                    //"text/plain" - txt
                    //"application/vnd.openxmlformats-officedocument.wordprocessingml.document" - docx
                    //"application/msword" - doc
                    //"application/zip" - zip
                    //"application/vnd.rar" - rar
                    //"image/jpeg" - jpeg
                    //"image/gif" - gif
                    //"image/png" - png


                    return await WriteBookOrImgFileAsync(e);
                }

            }
            else if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                if (e.Message.Photo[0].FileSize > 20971520) //20971520 == 20 mb - 20 mb in byte
                {
                    await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "book is not saved - too large file. Send file less 20 mb.");

                    return false;
                }
                else
                {
                    return await WriteBookOrImgFileAsync(e);
                }


            }
            WindowsPath = Directory.GetCurrentDirectory() + "\\" + "books\\";
            LinuxPath = "//media//pi//f//books//";
            return false;

        }

        static async Task<bool> WriteBookOrImgFileAsync(MessageEventArgs e)
        {
            Telegram.Bot.Types.File book = null;
            FileStream fs = null;
            string imgPath = "";
            int imgW = 1;
            int imgH = 1;
            try
            {

                if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Document)
                {
                    book = await bot.GetFileAsync(e.Message.Document.FileId);
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        fs = new FileStream(LinuxPath + e.Message.Document.FileName, FileMode.Create);
                    }
                    else
                    {
                        fs = new FileStream(WindowsPath + e.Message.Document.FileName, FileMode.Create);
                    }
                    Log("book or file: " + e.Message.Document.FileName);
                }

                if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
                {
                    book = await bot.GetFileAsync(e.Message.Photo[0].FileId);
                    imgW = e.Message.Photo[0].Width;
                    imgH = e.Message.Photo[0].Height;
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        fs = new FileStream(LinuxPath + "image" + e.Message.Photo[0].FileId + ".jpeg", FileMode.Create);
                        imgPath = LinuxPath + "image" + e.Message.Photo[0].FileId + ".jpeg";
                    }
                    else
                    {
                        fs = new FileStream(WindowsPath + "image" + e.Message.Photo[0].FileId + ".jpeg", FileMode.Create);
                        imgPath = WindowsPath + "image" + e.Message.Photo[0].FileId + ".jpeg";

                    }
                    Log("book or file: " + "image" + e.Message.Photo[0].FileId);
                }
            }


            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "book or file is not saved " + ex.Message + "\n" + ex.StackTrace);

                return false;
            }



            await bot.DownloadFileAsync(book.FilePath, fs);
            fs.Close();
            fs.Dispose();
            if (e.Message.Caption=="resize")
            {
                await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "wait a few minutes resizying image. You give a notification when will done.");
                ResizeImage(imgPath, imgW, imgH);
            }
        
            Console.WriteLine("book or file name add to log");
            await bot.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(userAndChatID), "book or file was save");
            
            return true;
        }
        public static void ResizeImage(string fileName, int width, int height)
        {
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(fileName)) {


                var res = new Bitmap(image, width * 10, height * 10);
                using (var gr = Graphics.FromImage(res))
                {
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    for (int i = 0; i < 200; i++)
                    {
                        gr.DrawImage(image, 0, 0, res.Width, res.Height);
                    }
                 

                }
                res.Save(fileName.Insert(fileName.LastIndexOf(".") - 1, "d"));
            }
            File.Delete(fileName);


        }
    }
}


